using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.StaticValues
{
    public static class Operations
    {
        private static readonly ReadOnlyDictionary<OperationType, string> OperationTexts;

        static Operations()
        {
            OperationTexts = new ReadOnlyDictionary<OperationType, string>(new Dictionary<OperationType, string>
            {
                { OperationType.Ac, "Ac" },
                { OperationType.Del, "Del" },
                { OperationType.Ans, "Ans" },
                { OperationType.Comma, "," },
                { OperationType.PlusMinus, "±" },
                { OperationType.EToTheNthPow, "eⁿ" },
                { OperationType.TenToTheNthPow, "10ⁿ" },
            });
        }

        public static string GetOperationText(OperationType key)
        {
            if (!OperationTexts.TryGetValue(key, out string result))
            {
                throw new InvalidEnumArgumentException(nameof(key), (int)key, typeof(OperationType));
            }

            return result;
        }
    }
}
