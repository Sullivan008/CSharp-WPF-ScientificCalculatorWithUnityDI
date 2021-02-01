using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Core.Extensions;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _delBtnCommand;
        public ICommand DelBtnCommand =>
            _delBtnCommand ?? (_delBtnCommand = new RelayCommandAsync<OperationCommandParameter>(DelBtnCommandExecute));

        public void DelBtnCommandExecute(OperationCommandParameter commandParameter)
        {
            if (_calculatorStorageService.LastUserInteractionType != UserInteractionType.AnsBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.EqualBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.EFunctionBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.ModOperatorBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.FactOperatorBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.SquareOfXNumberBtnPressed &&
                _calculatorStorageService.LastUserInteractionType != UserInteractionType.RightParenthesisBtnPressed)
            {
                if (!NumberTextBoxValue.IsOnlyZeroValue())
                {
                    NumberTextBoxValue = NumberTextBoxValue.RemoveLast();

                    if (NumberTextBoxValue.Length == 0)
                    {
                        NumberTextBoxValue = default(int).ToString();
                    }
                }

                _calculatorStorageService.SetLastUserInteractionType(commandParameter.UserInteractionType);
            }
        }
    }
}
