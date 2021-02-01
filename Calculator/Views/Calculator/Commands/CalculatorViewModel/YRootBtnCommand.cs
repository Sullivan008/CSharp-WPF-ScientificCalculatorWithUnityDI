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
        private ICommand _yRootBtnCommand;
        public ICommand YRootBtnCommand =>
            _yRootBtnCommand ?? (_yRootBtnCommand = new RelayCommandAsync<FunctionCommandParameter>(YRootBtnCommandExecute));

        private void YRootBtnCommandExecute(FunctionCommandParameter commandParameter)
        {
            if (_calculatorStorageService.LastUserInteractionType != UserInteractionType.AnsBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.EFunctionBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.ModOperatorBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.FactOperatorBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.SquareOfXNumberBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.RightParenthesisBtnPressed)
            {
                string leftParenthesisOperatorText = Operators.GetOperatorText(OperatorType.LeftParenthesis);

                if (_calculatorStorageService.LastUserInteractionType == UserInteractionType.EqualBtnPressed)
                {
                    _calculatorStorageService.AddValueToQueue(_calculatorStorageService.Ans.ToString(CultureInfo.CurrentCulture));

                    SeriesOfComputerTextBoxValue = $"{Operations.GetOperationText(OperationType.Ans)}";
                }
                else
                {
                    _calculatorStorageService.AddValueToQueue(NumberTextBoxValue);

                    SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {NumberTextBoxValue}";
                }

                _calculatorStorageService.AddLeftParenthesisOperatorToStack(leftParenthesisOperatorText);
                _calculatorStorageService.AddFunctionToStack(commandParameter.FunctionText);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {commandParameter.FunctionText} {leftParenthesisOperatorText}";
                NumberTextBoxValue = default(int).ToString();

                _calculatorStorageService.SetLastUserInteractionType(commandParameter.UserInteractionType);
            }
        }
    }
}
