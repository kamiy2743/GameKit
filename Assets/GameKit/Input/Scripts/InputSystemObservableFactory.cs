using System;
using GameKit.DisposableExtension;
using R3;
using UnityEngine.InputSystem;

namespace GameKit.Input
{
    public sealed class InputSystemObservableFactory
    {
        readonly InputModeHolder inputModeHolder;

        public InputSystemObservableFactory(InputModeHolder inputModeHolder)
        {
            this.inputModeHolder = inputModeHolder;
        }
        
        public ReadOnlyReactiveProperty<T> MakeReactiveProperty<T>(
            InputMode enableMode,
            InputAction action,
            Disposer disposer
        )  where T : struct
        {
            return Observable.EveryUpdate()
                .Select(_ => ReadValue<T>(enableMode, action))
                .ToReadOnlyReactiveProperty(ReadValue<T>(enableMode, action))
                .RegisterAndReturn(disposer);
        }

        public Observable<Unit> MakeObservable(
            InputMode enableMode,
            InputAction action,
            ButtonInputMode mode
        )
        {
            return Observable.EveryUpdate()
                .Where(_ => inputModeHolder.Get().CurrentValue.Allows(enableMode))
                .Where(_ =>
                {
                    return mode switch
                    {
                        ButtonInputMode.Triggered => action.triggered,
                        ButtonInputMode.Pressed => action.IsPressed(),
                        _ => throw new NotImplementedException(),
                    };
                });
        }
        
        T ReadValue<T>(InputMode enableMode, InputAction action) where T : struct
        {
            return inputModeHolder.Get().CurrentValue.Allows(enableMode) ? action.ReadValue<T>() : default;
        }
    }
}