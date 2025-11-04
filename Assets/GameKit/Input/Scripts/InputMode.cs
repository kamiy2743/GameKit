namespace GameKit.Input
{
    public sealed record InputMode
    {
        public static readonly InputMode All = new(nameof(All));
        
        readonly string value;
        
        public InputMode(string value)
        {
            this.value = value;
        }
        
        public bool Allows(InputMode mode)
        {
            return Equals(All) || Equals(mode);
        }
    }
}