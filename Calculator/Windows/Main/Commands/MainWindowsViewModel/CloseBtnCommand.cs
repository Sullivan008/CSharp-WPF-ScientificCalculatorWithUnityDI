using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Calculator.Core.Commands;

namespace Calculator.Windows.Main.ViewModels
{
    public partial class MainWindowViewModel
    {
        private ICommand _closeBtnCommand;
        public ICommand CloseBtnCommand =>
            _closeBtnCommand ?? (_closeBtnCommand = new RelayCommandAsync(CloseBtnCommandExecute));

        public Task CloseBtnCommandExecute()
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
