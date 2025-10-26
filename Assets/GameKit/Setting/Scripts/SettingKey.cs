namespace GameKit.Setting
{
    public sealed record SettingKey(string? Category, string? SubCategory, string Name)
    {
        public string? Category { get; } = Category;
        public string? SubCategory { get; } = SubCategory;
        public string Name { get; } = Name;
    }
}