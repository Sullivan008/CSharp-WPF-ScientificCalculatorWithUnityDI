using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _commaBtnCommand;
        public ICommand CommaBtnCommand =>
            _commaBtnCommand ?? (_commaBtnCommand = new RelayCommandAsync<OperationCommandParameter>(CommaBtnCommandExecute));

        private void CommaBtnCommandExecute(OperationCommandParameter commandParameter)
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

            if (!NumberTextBoxValue.Contains(commandParameter.OperationText))
            {
                NumberTextBoxValue += commandParameter.OperationText;
            }

            _calculatorStorageService.SetLastUserInteractionType(commandParameter.UserInteractionType);
        }
    }
}
