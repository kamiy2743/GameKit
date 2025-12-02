using GameKit.Localization;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed record PushConfirmModalParams(LocalizedString Message) : IPushModalParams
    {
        public LocalizedString Message { get; } = Message;
    }
}