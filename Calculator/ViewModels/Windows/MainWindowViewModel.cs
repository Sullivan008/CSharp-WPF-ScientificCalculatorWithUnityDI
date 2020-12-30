using Calculator.ViewModels.Views.Interfaces;

namespace Calculator.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICalculatorViewModel CalculatorViewModel { get; }

        public MainWindowViewModel(ICalculatorViewModel calculatorViewModel)
        {
            CalculatorViewModel = calculatorViewModel;
        }
    }
}
