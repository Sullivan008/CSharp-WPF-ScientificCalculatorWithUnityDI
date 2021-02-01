using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calculator.Enums.CalculatorView;

namespace Calculator.Views.Calculator.StaticValues
{
    public static class SimpleOperations
    {
        private static readonly ReadOnlyDictionary<SimpleOperationType, string> SimpleOperationTexts;

        static SimpleOperations()
        {
            SimpleOperationTexts = new ReadOnlyDictionary<SimpleOperationType, string>(new Dictionary<SimpleOperationType, string>
            {
                { SimpleOperationType.Comma, "," },
                { SimpleOperationType.Ans, "Ans" },
                { SimpleOperationType.Ac, "Ac" },
                { SimpleOperationType.Del, "Del" }
            });
        }

        public static string GetSimpleOperationText(SimpleOperationType key)
        {
            if (!SimpleOperationTexts.TryGetValue(key, out string result))
            {
                throw new ArgumentNullException(nameof(result), $@"The following Simple Operation Text Type does not exist with this key. {nameof(key).ToUpper()}: {key}");
            }

            return result;
        }
    }
}
