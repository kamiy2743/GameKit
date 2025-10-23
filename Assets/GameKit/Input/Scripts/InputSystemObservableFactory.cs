using R3;
using UnityEngine.InputSystem;

namespace GameKit.Input.GameKit.Input
{
    public static class InputSystemObservableFactory
    {
        public static ReadOnlyReactiveProperty<T> MakeReactiveProperty<T>(InputAction action)  where T : struct
        {
            return Observable.EveryUpdate()
                .Select(_ => action.ReadValue<T>())
                .Prepend(action.ReadValue<T>())
                .ToReadOnlyReactiveProperty();
        }

        public static Observable<Unit> MakeObservable(InputAction action)
        {
            return Observable.EveryUpdate()
                .Where(_ => action.IsPressed());
        }
    }
}