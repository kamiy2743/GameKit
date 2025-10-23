using System;
using System.Threading;

namespace GameKit.DisposableExtension
{
    sealed class CancellationTokenDisposer : IDisposer
    {
        readonly CancellationToken ct;

        public CancellationTokenDisposer(CancellationToken ct)
        {
            this.ct = ct;
        }

        void IDisposer.Register(IDisposable disposable)
        {
            ct.Register(disposable.Dispose);
        }
    }
}