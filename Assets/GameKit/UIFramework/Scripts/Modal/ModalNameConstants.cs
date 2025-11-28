namespace GameKit.UIFramework.Modal
{
    public partial record ModalName
    {
        public static readonly ModalName Confirm = new(nameof(Confirm));
        public static readonly ModalName Notice = new(nameof(Notice));
        public static readonly ModalName License = new(nameof(License));
    }
}