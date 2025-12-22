using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace GameKit.Collection
{
    public readonly unsafe struct NestableReadOnlyNativeArray<T> where T : unmanaged
    {
        readonly T* ptr;

        public int Length { get; }
        
        public ref T this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return ref ptr[index];
            }
        }

        public NestableReadOnlyNativeArray(NativeArray<T> source)
        {
            ptr = (T*)source.GetUnsafeReadOnlyPtr();
            Length = source.Length;
        }
    }
}