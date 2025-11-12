namespace GameKit.Exception
{
    public abstract class BaseException : System.Exception
    {
        protected BaseException(
            string message,
            System.Exception? innerException = null
        ) : base(message, innerException)
        {
        }
    }
}