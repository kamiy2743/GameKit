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
        
        public void SetActiveMode(InputMode mode)
        {
            if (!stack.Peek().Equals(InputMode.Default))
            {
                stack.Pop();
            }
            stack.Push(mode);
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