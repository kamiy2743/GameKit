using GameKit.Exception;

namespace GameKit.VRM
{
    public sealed class LocalVRMFailedException : BaseException
    {
        new const string Message = "VRMの読み込みに失敗しました。";
        
        public LocalVRMFailedException(System.Exception? innerException = null) : base(Message, innerException)
        {
        }
    }
}