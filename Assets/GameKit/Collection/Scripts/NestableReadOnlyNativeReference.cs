using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace GameKit.Collection
{
    public readonly unsafe struct NestableReadOnlyNativeReference<T> where T : unmanaged
    {
        readonly T* ptr;
        
        public T Value => *ptr;

        public NestableReadOnlyNativeReference(NativeReference<T> source)
        {
            ptr = source.GetUnsafeReadOnlyPtr();
        }
    }
}