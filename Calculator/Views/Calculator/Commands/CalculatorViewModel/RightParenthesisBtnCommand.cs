using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _rightParenthesisBtnCommand;
        public ICommand RightParenthesisBtnCommand =>
            _rightParenthesisBtnCommand ?? (_rightParenthesisBtnCommand = new RelayCommandAsync<OperatorCommandParameter>(RightParenthesisBtnCommandExecute));

        private void RightParenthesisBtnCommandExecute(OperatorCommandParameter commandParameter)
        {
            if (_calculatorStorageService.LeftParenthesisNumber > 0)
            {
                if (_calculatorStorageService.LastUserInteractionType != UserInteractionType.AnsBtnPressed &&
                    _calculatorStorageService.LastUserInteractionType != UserInteractionType.EFunctionBtnPressed &&
                    _calculatorStorageService.LastUserInteractionType != UserInteractionType.ModOperatorBtnPressed &&
                    _calculatorStorageService.LastUserInteractionType != UserInteractionType.FactOperatorBtnPressed &&
                    _calculatorStorageService.LastUserInteractionType != UserInteractionType.SquareOfXNumberBtnPressed &&
                    _calculatorStorageService.LastUserInteractionType != UserInteractionType.RightParenthesisBtnPressed)
                {
                    _calculatorStorageService.AddValueToQueue(NumberTextBoxValue);

                    SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {NumberTextBoxValue}";
                }

                _calculatorStorageService.AddRightParenthesisOperatorToStack(commandParameter.OperatorText);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {commandParameter.OperatorText}";
                NumberTextBoxValue = string.Empty;

                _calculatorStorageService.SetLastUserInteractionType(commandParameter.UserInteractionType);
            }
        }
    }
}
