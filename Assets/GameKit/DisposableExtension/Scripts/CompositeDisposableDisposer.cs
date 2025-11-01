using System;
using System.Collections.Generic;

namespace GameKit.DisposableExtension
{
    sealed class DisposableCollectionDisposer : IDisposer
    {
        readonly ICollection<IDisposable> disposables;

        public DisposableCollectionDisposer(ICollection<IDisposable> disposables)
        {
            this.disposables = disposables;
        }

        void IDisposer.Register(IDisposable disposable)
        {
            disposables.Add(disposable);
        }
    }
}