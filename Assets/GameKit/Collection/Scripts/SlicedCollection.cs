using System;
using System.Collections;
using System.Collections.Generic;

namespace GameKit.Collection
{
    public readonly struct SlicedCollection<T> : IEnumerable<T>
    {
        readonly IEnumerable<T> source;
        readonly int start;
        readonly int length;

        public SlicedCollection(IEnumerable<T> source, int start, int length)
        {
            this.source = source;
            this.start = start;
            this.length = length;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(source, start, length);
        }
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>
        {
            readonly IEnumerator<T> enumerator;
            readonly int start;
            readonly int length;
            int index;
            bool initialized;

            public Enumerator(IEnumerable<T> source, int start, int length)
            {
                enumerator = source.GetEnumerator();
                this.start = start;
                this.length = length;
                index = 0;
                initialized = false;
            }

            public T Current => enumerator.Current;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (!initialized)
                {
                    initialized = true;
                    for (var i = 0; i < start; i++)
                    {
                        if (!enumerator.MoveNext())
                        {
                            return false;
                        }
                    }
                }

                if (index >= length)
                {
                    return false;
                }

                if (!enumerator.MoveNext())
                {
                    index = length;
                    return false;
                }

                index++;
                return true;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
                enumerator.Dispose();
            }
        }
    }
}
