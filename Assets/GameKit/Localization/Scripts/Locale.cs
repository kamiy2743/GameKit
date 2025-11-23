namespace GameKit.Localization
{
    public sealed record Locale(string Name, string Code)
    {
        public string Name { get; } = Name;
        public string Code { get; } = Code;
    }
}