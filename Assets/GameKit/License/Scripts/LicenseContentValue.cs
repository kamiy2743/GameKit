namespace GameKit.License
{
    public sealed record LicenseContentValue(string Name, string Body)
    {
        public string Name { get; } = Name;
        public string Body { get; } = Body;
    }
}