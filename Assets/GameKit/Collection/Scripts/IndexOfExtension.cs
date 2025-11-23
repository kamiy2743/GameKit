using System;
using System.Collections.Generic;

namespace GameKit.Collection
{
    public static class IndexOfExtension
    {
        public static int? IndexOf<T>(this IEnumerable<T> collection, T target) where T : IEquatable<T>
        {
            var index = 0;

            foreach (var value in collection)
            {
                if (value.Equals(target))
                {
                    return index;
                }
                index++;
            }

            return null;
        }
    }
}