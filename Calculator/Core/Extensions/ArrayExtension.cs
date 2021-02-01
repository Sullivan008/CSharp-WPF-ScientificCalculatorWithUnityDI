using System;

namespace Calculator.Core.Extensions
{
    public static class ArrayExtension
    {
        public static bool IsNullOrEmpty(this Array array)
        {
            return array == null || array.Length == 0;
        }

        public static bool TryIndex<TInputArrayType, TOutputElementType>(this TInputArrayType[] array, int index, out TOutputElementType result)
        {
            index = Math.Abs(index);

            result = default;
            bool success = false;

            if (array != null && index < array.Length)
            {
                result = (TOutputElementType)array.GetValue(index);

                success = true;
            }

            return success;
        }
    }
}
