using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace GameKit.Collection
{
    public readonly unsafe struct NestableNativeReference<T> where T : unmanaged
    {
        readonly T* ptr;
        
        public T Value
        {
            get => *ptr;
            set => *ptr = value;
        }

        public NestableNativeReference(NativeReference<T> source)
        {
            ptr = source.GetUnsafePtr();
        }
    }
}
