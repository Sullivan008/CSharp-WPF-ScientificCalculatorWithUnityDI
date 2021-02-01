using System.Globalization;
using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _ansBtnCommand;
        public ICommand AnsBtnCommand =>
            _ansBtnCommand ?? (_ansBtnCommand = new RelayCommandAsync<OperationCommandParameter>(AnsBtnCommandExecute));

        public void AnsBtnCommandExecute(OperationCommandParameter commandParameter)
        {
            if (_calculatorStorageService.LastUserInteractionType == UserInteractionType.EqualBtnPressed)
            {
                SeriesOfComputerTextBoxValue = string.Empty;
            }

            if (_calculatorStorageService.LastUserInteractionType == UserInteractionType.AnsBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.EFunctionBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.ModOperatorBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.FactOperatorBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.SquareOfXNumberBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.RightParenthesisBtnPressed)
            {
                string mulOperatorText = Operators.GetOperatorText(OperatorType.Multiplication);

                _calculatorStorageService.AddOperatorToStack(mulOperatorText, OperatorType.Multiplication);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {mulOperatorText}";
            }

            _calculatorStorageService.AddValueToQueue(_calculatorStorageService.Ans.ToString(CultureInfo.CurrentCulture));

            SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {commandParameter.OperationText}";
            NumberTextBoxValue = string.Empty;

            _calculatorStorageService.SetLastUserInteractionType(commandParameter.UserInteractionType);
        }
    }
}
