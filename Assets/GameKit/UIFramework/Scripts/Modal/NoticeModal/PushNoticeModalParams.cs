using GameKit.Localization;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed record PushNoticeModalParams(LocalizedString Message) : IPushModalParams
    {
        public LocalizedString Message { get; } = Message;
    }
}