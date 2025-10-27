using System;
using R3;

namespace GameKit.DisposableExtension
{
    public static class RegisterExtension
    {
        public static void AddTo(this IDisposable disposable, Disposer disposer)
        {
            disposer.Register(disposable);
        }
        
        public static ReadOnlyReactiveProperty<T> RegisterAndReturn<T>(this ReadOnlyReactiveProperty<T> rp, Disposer disposer)
        {
            disposer.Register(rp);
            return rp;
        }
    }
}