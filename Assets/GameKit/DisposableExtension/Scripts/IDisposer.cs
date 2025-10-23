using System;

namespace GameKit.DisposableExtension
{
    public interface IDisposer
    {
        void Register(IDisposable disposable);
    }
}