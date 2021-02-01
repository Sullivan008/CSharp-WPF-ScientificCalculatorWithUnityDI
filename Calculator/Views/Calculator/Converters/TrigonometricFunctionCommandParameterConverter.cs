using System;
using System.Globalization;
using System.Windows.Data;
using Calculator.Core.Extensions;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.Converters
{
    public class TrigonometricFunctionCommandParameterConverter : IMultiValueConverter
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

            if (!values.TryIndex(1, out UserInteractionType inverseUserInteractionType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(UserInteractionType)} - {nameof(inverseUserInteractionType)}");
            }

            if (!values.TryIndex(2, out TrigonometricFunctionType trigonometricFunctionType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(TrigonometricFunctionType)} - {nameof(trigonometricFunctionType)}");
            }

            if (!values.TryIndex(3, out TrigonometricFunctionType inverseTrigonometricFunctionType))
            {
                throw new IndexOutOfRangeException($@"The following type was not defined: {typeof(TrigonometricFunctionType)} - {nameof(inverseTrigonometricFunctionType)}");
            }

            return new TrigonometricFunctionCommandParameter(userInteractionType, inverseUserInteractionType, trigonometricFunctionType, inverseTrigonometricFunctionType);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
