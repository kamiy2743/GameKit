namespace GameKit.Input
{
    public sealed class InputMode
    {
        public static readonly InputMode Default = new(nameof(Default));
        
        readonly string value;
        
        public InputMode(string value)
        {
            this.value = value;
        }
        
        public bool Contains(InputMode mode)
        {
            return Equals(Default) || Equals(mode);
        }
        
        bool Equals(InputMode other)
        {
            return value == other.value;
        }
    }
}