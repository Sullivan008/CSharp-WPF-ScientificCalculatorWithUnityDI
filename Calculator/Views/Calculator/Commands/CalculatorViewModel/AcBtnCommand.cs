using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Views.Calculator.CommandParameters;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _acBtnCommand;
        public ICommand AcBtnCommand =>
            _acBtnCommand ?? (_acBtnCommand = new RelayCommandAsync<OperationCommandParameter>(AcBtnCommandExecute));

        public void AcBtnCommandExecute(OperationCommandParameter commandParameter)
        {
            _calculatorStorageService.ClearQueue();
            _calculatorStorageService.ClearStack();
            _calculatorStorageService.ClearLeftParenthesisNumber();

            NumberTextBoxValue = default(int).ToString();
            SeriesOfComputerTextBoxValue = string.Empty;

            _calculatorStorageService.SetLastUserInteractionType(commandParameter.UserInteractionType);
        }
    }
}
