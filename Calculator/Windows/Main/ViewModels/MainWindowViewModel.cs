using Calculator.Core.ViewModels;
using Calculator.Views.Calculator.ViewModels.Interfaces;
using Calculator.Windows.Main.ViewModels.Interfaces;

namespace Calculator.Windows.Main.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public ICalculatorViewModel CalculatorViewModel { get; }

        public MainWindowViewModel(ICalculatorViewModel calculatorViewModel)
        {
            CalculatorViewModel = calculatorViewModel;
        }
    }
}
