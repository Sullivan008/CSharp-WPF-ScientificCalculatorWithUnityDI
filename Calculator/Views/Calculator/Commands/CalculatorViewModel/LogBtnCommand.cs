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
        private ICommand _logBtnCommand;
        public ICommand LogBtnCommand =>
            _logBtnCommand ?? (_logBtnCommand = new RelayCommandAsync<FunctionAndOperatorCommandParameter>(LogBtnCommandExecute));

        private void LogBtnCommandExecute(FunctionAndOperatorCommandParameter commandParameter)
        {
            if (!_isShiftEnabled)
            {
                string leftParenthesisOperatorText = Operators.GetOperatorText(OperatorType.LeftParenthesis);

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

                _calculatorStorageService.AddLeftParenthesisOperatorToStack(leftParenthesisOperatorText);
                _calculatorStorageService.AddFunctionToStack(commandParameter.FunctionText);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {commandParameter.FunctionText} {leftParenthesisOperatorText}";
                NumberTextBoxValue = default(int).ToString();

                _calculatorStorageService.SetLastUserInteractionType(commandParameter.FunctionUserInteractionType);
            }
            else
            {
                const int TEN_NUMBER_VALUE = default(int) + 10;

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

                _calculatorStorageService.AddValueToQueue(TEN_NUMBER_VALUE.ToString());
                _calculatorStorageService.AddOperatorToStack(commandParameter.OperatorText, commandParameter.OperatorType);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {TEN_NUMBER_VALUE} {commandParameter.OperatorText}";
                NumberTextBoxValue = default(int).ToString();

                _calculatorStorageService.SetLastUserInteractionType(commandParameter.OperatorUserInteractionType);
            }
        }
    }
}
