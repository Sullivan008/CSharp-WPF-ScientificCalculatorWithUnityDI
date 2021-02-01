using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Calculator.Core.Commands;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _offBtnCommand;
        public ICommand OffBtnCommand =>
            _offBtnCommand ?? (_offBtnCommand = new RelayCommandAsync(OffBtnCommandExecute));

        public Task OffBtnCommandExecute()
        {
            Window mainWindow = Application.Current.MainWindow;

            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow), @"The value cannot be null!");
            }

            mainWindow.Close();

            return Task.FromResult(true);
        }
    }
}
