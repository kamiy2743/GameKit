using System;
using GameKit.DisposableExtension;
using ObservableCollections;
using R3;

namespace GameKit.Input
{
    public sealed class InputModeContainer
    {
        readonly ObservableStack<InputMode> stack = new();

        public InputModeContainer()
        {
            stack.Push(InputMode.Default);
        }
        
        public void Push(InputMode mode)
        {
            if (mode.Equals(InputMode.Default))
            {
                throw new InvalidOperationException("デフォルトモードはPushできません。");
            }
            stack.Push(mode);
        }
        
        public void Pop()
        {
            if (GetActiveMode().Equals(InputMode.Default))
            {
                throw new InvalidOperationException("デフォルトモードはPopできません。");
            }
            stack.Pop();
        }
        
        public InputMode GetActiveMode()
        {
            return stack.Peek();
        }
        
        public ReadOnlyReactiveProperty<InputMode> GetActiveModeAsReactiveProperty(Disposer disposer)
        {
            return stack.ObserveCountChanged()
                .Select(_ => GetActiveMode())
                .ToReadOnlyReactiveProperty(GetActiveMode())
                .RegisterAndReturn(disposer);
        }
        
        public bool ContainsInActiveMode(InputMode mode)
        {
            return GetActiveMode().Contains(mode);
        }
    }
}