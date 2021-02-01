using System;
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
        private ICommand _lnBtnCommand;
        public ICommand LnBtnCommand =>
            _lnBtnCommand ?? (_lnBtnCommand = new RelayCommandAsync<FunctionAndOperatorCommandParameter>(LnBtnCommandExecute));

        private void LnBtnCommandExecute(FunctionAndOperatorCommandParameter commandParameter)
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

                _calculatorStorageService.AddValueToQueue(Math.E.ToString(CultureInfo.CurrentCulture));
                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {Functions.GetFunctionText(FunctionType.E)}";

                _calculatorStorageService.AddOperatorToStack(commandParameter.OperatorText, commandParameter.OperatorType);

                SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {commandParameter.OperatorText}";
                NumberTextBoxValue = default(int).ToString();

                _calculatorStorageService.SetLastUserInteractionType(commandParameter.OperatorUserInteractionType);
            }
        }
    }
}
