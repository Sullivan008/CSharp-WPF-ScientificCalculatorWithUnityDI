using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.StaticValues
{
    public static class Functions
    {
        private static readonly ReadOnlyDictionary<FunctionType, string> FunctionTexts;

        private static readonly ReadOnlyDictionary<TrigonometricFunctionType, string> TrigonometricFunctionTexts;

        static Functions()
        {
            FunctionTexts = new ReadOnlyDictionary<FunctionType, string>(new Dictionary<FunctionType, string>
            {
                { FunctionType.E, "e" },
                { FunctionType.Ln, "ln" },
                { FunctionType.Log, "log" },
                { FunctionType.Sqrt, "sqrt" },
                { FunctionType.YRoot, "yroot" },
            });

            TrigonometricFunctionTexts = new ReadOnlyDictionary<TrigonometricFunctionType, string>(new Dictionary<TrigonometricFunctionType, string>
            {
                { TrigonometricFunctionType.Sin, "sin" },
                { TrigonometricFunctionType.ArcSin, "arcsin" },
                { TrigonometricFunctionType.Cos, "cos" },
                { TrigonometricFunctionType.ArcCos, "arccos" },
                { TrigonometricFunctionType.Tan, "tan" },
                { TrigonometricFunctionType.ArcTan, "arctan" },
            });
        }

        public static string GetFunctionText(FunctionType key)
        {
            if (!FunctionTexts.TryGetValue(key, out string result))
            {
                throw new InvalidEnumArgumentException(nameof(key), (int)key, typeof(FunctionType));
            }

            return result;
        }

        public static FunctionType GetFunctionTypeByText(string value)
        {
            return FunctionTexts.First(x => x.Value == value).Key;
        }

        public static string GetTrigonometricFunctionText(TrigonometricFunctionType key)
        {
            if (!TrigonometricFunctionTexts.TryGetValue(key, out string result))
            {
                throw new InvalidEnumArgumentException(nameof(key), (int)key, typeof(TrigonometricFunctionType));
            }

            return result;
        }

        public static TrigonometricFunctionType GetTrigonometricFunctionTypeByText(string value)
        {
            return TrigonometricFunctionTexts.First(x => x.Value == value).Key;
        }

        public static bool IsFunction(string value)
        {
            return FunctionTexts.Any(x => x.Value == value);
        }

        public static bool IsTrigonometricFunction(string value)
        {
            return TrigonometricFunctionTexts.Any(x => x.Value == value);
        }
    }
}