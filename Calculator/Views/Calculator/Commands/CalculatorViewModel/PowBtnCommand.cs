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
        private ICommand _powBtnCommand;
        public ICommand PowBtnCommand =>
            _powBtnCommand ?? (_powBtnCommand = new RelayCommandAsync<OperatorCommandParameter>(PowBtnCommandExecute));

        private void PowBtnCommandExecute(OperatorCommandParameter commandParameter)
        {
            if (_calculatorStorageService.LastUserInteractionType == UserInteractionType.EqualBtnPressed)
            {
                _calculatorStorageService.AddValueToQueue(_calculatorStorageService.Ans.ToString(CultureInfo.CurrentCulture));

                SeriesOfComputerTextBoxValue = $"{Operations.GetOperationText(OperationType.Ans)}";
            }

            if (_calculatorStorageService.LastUserInteractionType == UserInteractionType.AnsBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.EqualBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.EFunctionBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.ModOperatorBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.FactOperatorBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.SquareOfXNumberBtnPressed ||
                _calculatorStorageService.LastUserInteractionType == UserInteractionType.RightParenthesisBtnPressed)

            {
                _calculatorStorageService.AddOperatorToStack(commandParameter.OperatorText, commandParameter.OperatorType);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {commandParameter.OperatorText}";
            }
            else
            {
                _calculatorStorageService.AddValueToQueue(NumberTextBoxValue);
                _calculatorStorageService.AddOperatorToStack(commandParameter.OperatorText, commandParameter.OperatorType);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {NumberTextBoxValue} {commandParameter.OperatorText}";
            }

            NumberTextBoxValue = default(int).ToString();

            _calculatorStorageService.SetLastUserInteractionType(commandParameter.UserInteractionType);
        }
    }
}
