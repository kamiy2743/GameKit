using System.Collections;
using System.Collections.Generic;
using Unity.Collections;

namespace GameKit.Collection
{
    public static class NativeListExtension
    {
        public static IEnumerable<T> AsEnumerable<T>(this NativeList<T> list) where T : unmanaged
        {
            return new NativeListEnumerable<T>(list);
        }
        
        readonly struct NativeListEnumerable<T> : IEnumerable<T> where T : unmanaged
        {
            readonly NativeList<T> list;

            public NativeListEnumerable(NativeList<T> list)
            {
                this.list = list;
            }


            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return list.GetEnumerator();
            }
        }
    }
}