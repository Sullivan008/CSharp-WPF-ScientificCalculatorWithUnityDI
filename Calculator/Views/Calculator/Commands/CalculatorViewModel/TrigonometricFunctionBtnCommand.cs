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
        private ICommand _trigonometricFunctionBtnCommand;
        public ICommand TrigonometricFunctionBtnCommand =>
            _trigonometricFunctionBtnCommand ?? (_trigonometricFunctionBtnCommand = new RelayCommandAsync<TrigonometricFunctionCommandParameter>(TrigonometricFunctionBtnCommandExecute));

        private void TrigonometricFunctionBtnCommandExecute(TrigonometricFunctionCommandParameter commandParameter)
        {
            UserInteractionType userOperationType = !_isShiftEnabled ? commandParameter.UserInteractionType : commandParameter.InverseUserInteractionType;
            
            string trigonometricFunctionText = !_isShiftEnabled ? commandParameter.TrigonometricFunctionText : commandParameter.InverseTrigonometricFunctionText;
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
            _calculatorStorageService.AddFunctionToStack(trigonometricFunctionText);

            SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {trigonometricFunctionText} {leftParenthesisOperatorText}";
            NumberTextBoxValue = default(int).ToString();

            _calculatorStorageService.SetLastUserInteractionType(userOperationType);
        }
    }
}
