using System;

namespace console
{
    public static class ArrayExtensions
    {
        public static TValue GetModuloValue<TValue>(this TValue[] array, int index)
        {
            return array[Math.Abs(index % array.Length)];
        }

        public static TValue SetModuloValue<TValue>(this TValue[] array, int index, TValue value)
        {
            return array[Math.Abs(index % array.Length)] = value;
        }
    }
}