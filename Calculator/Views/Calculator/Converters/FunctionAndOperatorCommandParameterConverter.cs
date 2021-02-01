using System;
using System.Globalization;
using System.Windows.Data;
using Calculator.Core.Extensions;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.Converters
{
    public class FunctionAndOperatorCommandParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(values), @"The value cannot be null!");
            }

            if (!values.TryIndex(0, out UserInteractionType functionUserInteractionType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(UserInteractionType)} - {nameof(functionUserInteractionType)}");
            }

            if (!values.TryIndex(1, out UserInteractionType operatorUserInteractionType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(UserInteractionType)} - {nameof(operatorUserInteractionType)}");
            }

            if (!values.TryIndex(2, out FunctionType functionType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(FunctionType)} - {nameof(functionType)}");
            }

            if (!values.TryIndex(3, out OperatorType operatorType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(OperatorType)} - {nameof(operatorType)}");
            }

            return new FunctionAndOperatorCommandParameter(functionUserInteractionType, operatorUserInteractionType, functionType, operatorType);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
