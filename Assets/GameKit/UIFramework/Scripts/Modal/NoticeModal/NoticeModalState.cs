namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed record NoticeModalState(bool IsOk) : IModalState
    {
        public bool IsOk { get; } = IsOk;
    }
}