using System.Globalization;
using System.Linq;

namespace Calculator.Core.Extensions
{
    public static class StringExtension
    {
        public static bool IsOnlyZeroValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return value.All(x => x == '0');
        }

        public static bool IsDecimalNumber(this string value)
        {
            return double.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.CurrentCulture, out _);
        }

        public static string RemoveLast(this string value)
        {
            if (value.Length >= 1)
            {
                return value.Remove(value.Length - 1);
            }

            return value;
        }
    }
}
