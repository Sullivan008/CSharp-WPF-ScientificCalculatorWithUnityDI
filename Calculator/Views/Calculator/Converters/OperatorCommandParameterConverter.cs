using System;
using System.Globalization;
using System.Windows.Data;
using Calculator.Core.Extensions;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.Converters
{
    public class OperatorCommandParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(values), @"The value cannot be null!");
            }

            if (!values.TryIndex(0, out UserInteractionType userInteractionType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(UserInteractionType)} - {nameof(userInteractionType)}");
            }

            if (!values.TryIndex(1, out OperatorType operatorType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(OperationType)} - {nameof(operatorType)}");
            }

            return new OperatorCommandParameter(userInteractionType, operatorType);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
