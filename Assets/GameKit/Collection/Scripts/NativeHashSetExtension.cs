using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;

namespace GameKit.Collection
{
    public static class NativeHashSetExtension
    {
        public static IEnumerable<T> AsEnumerable<T>(this NativeHashSet<T> source) where T : unmanaged, IEquatable<T>
        {
            return new NativeHashSetEnumerable<T>(source);
        }
        
        readonly struct NativeHashSetEnumerable<T> : IEnumerable<T> where T : unmanaged, IEquatable<T>
        {
            readonly NativeHashSet<T> source;

            public NativeHashSetEnumerable(NativeHashSet<T> source)
            {
                this.source = source;
            }


            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return source.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return source.GetEnumerator();
            }
        }
    }
}