using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace GameKit.Collection
{
    public readonly unsafe struct NestableNativeArray<T> where T : unmanaged
    {
        readonly T* ptr;

        public int Length { get; }
        public bool IsEmpty { get; }
        
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

        public NestableNativeArray(NativeArray<T> source)
        {
            ptr = (T*)source.GetUnsafePtr();
            Length = source.Length;
            IsEmpty = source.Length == 0;
        }
        
        public T* GetUnsafePtr()
        {
            return ptr;
        }
    }
}