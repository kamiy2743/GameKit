using System;
using GameKit.Application;
using R3;
using VContainer.Unity;

namespace GameKit.Input
{
    public sealed class CursorChanger : IInitializable, IDisposable
    {
        readonly InputModeContainer inputModeContainer;

        readonly CompositeDisposable disposable = new();

        public CursorChanger(InputModeContainer inputModeContainer)
        {
            this.inputModeContainer = inputModeContainer;
        }

        void IInitializable.Initialize()
        {
            inputModeContainer.GetActiveModeAsReactiveProperty(disposable)
                .Subscribe(x => Cursor.SetVisible(x.isVisibleCursor))
                .AddTo(disposable);
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}