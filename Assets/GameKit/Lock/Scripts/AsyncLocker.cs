using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameKit.Lock
{
    public sealed record AsyncLocker
    {
        readonly SemaphoreSlim locker = new(1, 1);

        public async UniTask<IDisposable> LockAsync(CancellationToken ct)
        {
            await locker.WaitAsync(ct);
            return new LockToken(locker);
        }

        sealed record LockToken : IDisposable
        {
            readonly SemaphoreSlim locker;

            bool disposed;

            public LockToken(SemaphoreSlim locker)
            {
                this.locker = locker;
            }

            void IDisposable.Dispose()
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(nameof(LockToken));
                }
                disposed = true;
                locker.Release();
            }
        }
    }
}