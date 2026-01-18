using System.Runtime.CompilerServices;

namespace GameKit.Burst
{
    public static class BurstHashCode
    {
        const int Multiplier = 397;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(int value1)
        {
            return value1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(int value1, int value2)
        {
            unchecked
            {
                var hash = value1;
                hash = (hash * Multiplier) ^ value2;
                return hash;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(int value1, int value2, int value3)
        {
            unchecked
            {
                var hash = Combine(value1, value2);
                hash = (hash * Multiplier) ^ value3;
                return hash;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(int value1, int value2, int value3, int value4)
        {
            unchecked
            {
                var hash = Combine(value1, value2, value3);
                hash = (hash * Multiplier) ^ value4;
                return hash;
            }
        }
    }
}
