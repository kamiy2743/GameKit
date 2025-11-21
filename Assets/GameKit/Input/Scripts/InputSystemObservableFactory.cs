using System;
using GameKit.DisposableExtension;
using R3;
using UnityEngine.InputSystem;

namespace GameKit.Input
{
    public sealed class InputSystemObservableFactory
    {
        readonly InputModeContainer inputModeContainer;

        public InputSystemObservableFactory(InputModeContainer inputModeContainer)
        {
            this.inputModeContainer = inputModeContainer;
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
                .Where(_ => inputModeContainer.ContainsInActiveMode(enableMode))
                .Where(_ =>
                {
                    return mode switch
                    {
                        ButtonInputMode.Triggered => action.triggered,
                        ButtonInputMode.Pressed => action.IsPressed(),
                        _ => throw new ArgumentException(),
                    };
                });
        }
        
        T ReadValue<T>(InputMode enableMode, InputAction action) where T : struct
        {
            return inputModeContainer.ContainsInActiveMode(enableMode) ? action.ReadValue<T>() : default;
        }
    }
}