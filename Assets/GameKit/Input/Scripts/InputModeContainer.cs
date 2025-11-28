using System;
using System.Collections.Generic;

namespace GameKit.Input
{
    public sealed class InputModeContainer
    {
        readonly Stack<InputMode> stack = new();

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
        
        public bool ContainsInActiveMode(InputMode mode)
        {
            return GetActiveMode().Contains(mode);
        }
    }
}