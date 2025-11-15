using GameKit.Exception;

namespace GameKit.File
{
    public sealed class SelectFileCanceledException : BaseException
    {
        new const string Message = "ファイル選択がキャンセルされました。";
        
        public SelectFileCanceledException(System.Exception? innerException = null) : base(Message, innerException)
        {
        }
    }
}