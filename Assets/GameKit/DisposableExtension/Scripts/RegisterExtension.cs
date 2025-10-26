using R3;

namespace GameKit.DisposableExtension
{
    public static class RegisterExtension
    {
        public static ReadOnlyReactiveProperty<T> Register<T>(this ReadOnlyReactiveProperty<T> rp, Disposer disposer)
        {
            disposer.Register(rp);
            return rp;
        }
    }
}