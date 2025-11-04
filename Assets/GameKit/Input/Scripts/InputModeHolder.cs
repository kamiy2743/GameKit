using R3;

namespace GameKit.Input
{
    public sealed class InputModeHolder
    {
        readonly ReactiveProperty<InputMode> currentMode = new();

        public ReadOnlyReactiveProperty<InputMode> Get()
        {
            return currentMode;
        }

        public void Set(InputMode mode)
        {
            currentMode.Value = mode;
        }
    }
}