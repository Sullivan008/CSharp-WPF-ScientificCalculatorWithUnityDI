using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _plusMinusBtnCommand;
        public ICommand PlusMinusBtnCommand =>
            _plusMinusBtnCommand ?? (_plusMinusBtnCommand = new RelayCommandAsync(PlusMinusBtnCommandExecute));

        private Task PlusMinusBtnCommandExecute()
        {
            if (_calculatorStorageService.LastUserInteractionType == UserInteractionType.AnsBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.EqualBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.EFunctionBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.ModOperatorBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.FactOperatorBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.SquareOfXNumberBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.RightParenthesisBtnPressed)
            {
                OperandBtnCommand.Execute(new OperandCommandParameter(UserInteractionType.OperandBtnPressed, default));
            }

            if (!double.TryParse(NumberTextBoxValue, out double number))
            {
                throw new FormatException("The input filed contains non-numeric characters!");
            }

            NumberTextBoxValue = (number * -1).ToString(CultureInfo.CurrentCulture);

            _calculatorStorageService.SetLastUserInteractionType(UserInteractionType.PlusMinusBtnPressed);

            return Task.FromResult(true);
        }
    }
}
