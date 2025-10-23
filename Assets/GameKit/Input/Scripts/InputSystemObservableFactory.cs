using GameKit.DisposableExtension;
using R3;
using UnityEngine.InputSystem;

namespace GameKit.Input.GameKit.Input
{
    public static class InputSystemObservableFactory
    {
        public static ReadOnlyReactiveProperty<T> MakeReactiveProperty<T>(InputAction action, Disposer disposer)  where T : struct
        {
            var rp = Observable.EveryUpdate()
                .Select(_ => action.ReadValue<T>())
                .Prepend(action.ReadValue<T>())
                .ToReadOnlyReactiveProperty();
            disposer.Register(rp);
            return rp;
        }

        public static Observable<Unit> MakeObservable(InputAction action)
        {
            return Observable.EveryUpdate()
                .Where(_ => action.IsPressed());
        }
    }
}