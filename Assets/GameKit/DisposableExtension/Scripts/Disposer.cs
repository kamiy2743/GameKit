using System;
using System.Threading;
using R3;

namespace GameKit.DisposableExtension
{
    public sealed class Disposer
    {
        readonly IDisposer disposer;

        Disposer(IDisposer disposer)
        {
            this.disposer = disposer;
        }
        
        public void Register(IDisposable disposable)
        {
            disposer.Register(disposable);
        }

        public static implicit operator Disposer(CancellationToken ct)
        {
            return new Disposer(new CancellationTokenDisposer(ct));
        }
        
        public static implicit operator Disposer(CompositeDisposable disposable)
        {
            return new Disposer(new DisposableCollectionDisposer(disposable));
        }
    }
}
