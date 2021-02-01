using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.StaticValues
{
    public static class Operators
    {
        private static readonly ReadOnlyDictionary<OperatorType, string> OperatorTexts;

        private static readonly ReadOnlyDictionary<OperatorType, int> OperatorPriorities;

        static Operators()
        {
            OperatorTexts = new ReadOnlyDictionary<OperatorType, string>(new Dictionary<OperatorType, string>
            {
                { OperatorType.Addition, "+" },
                { OperatorType.Subtraction, "-" },
                { OperatorType.Multiplication, "*" },
                { OperatorType.Division, "/" },
                { OperatorType.Modulus, "%" },
                { OperatorType.Factorial, "!" },
                { OperatorType.Pow, "^" },
                { OperatorType.LeftParenthesis, "(" },
                { OperatorType.RightParenthesis, ")" },
                { OperatorType.Equal, "=" }
            });

            OperatorPriorities = new ReadOnlyDictionary<OperatorType, int>(new Dictionary<OperatorType, int>
            {
                { OperatorType.Pow, 3 },
                { OperatorType.Modulus, 3 },
                { OperatorType.Factorial, 3 },
                { OperatorType.Division, 2 },
                { OperatorType.Multiplication, 2 },
                { OperatorType.Addition, 1 },
                { OperatorType.Subtraction, 1 },
            });
        }

        public static bool IsOperator(string value)
        {
            return OperatorTexts.Any(x => x.Value == value);
        }

        public static bool IsLeftParenthesisOperator(string operatorText)
        {
            if (!IsOperator(operatorText))
            {
                return false;
            }

            OperatorType operatorType = GetOperatorTypeByText(operatorText);

            return operatorType == OperatorType.LeftParenthesis;
        }

        public static bool IsParenthesisOperator(string operatorText)
        {
            if (!IsOperator(operatorText))
            {
                return false;
            }

            OperatorType operatorType = GetOperatorTypeByText(operatorText);

            return operatorType == OperatorType.LeftParenthesis || operatorType == OperatorType.RightParenthesis;
        }

        public static int GetOperatorPriority(OperatorType operatorType)
        {
            return OperatorPriorities.First(x => x.Key == operatorType).Value;
        }

        public static string GetOperatorText(OperatorType key)
        {
            if (!OperatorTexts.TryGetValue(key, out string result))
            {
                throw new InvalidEnumArgumentException(nameof(key), (int)key, typeof(OperatorType));
            }

            return result;
        }

        public static OperatorType GetOperatorTypeByText(string operatorText)
        {
            return OperatorTexts.First(x => x.Value == operatorText).Key;
        }
    }
}
