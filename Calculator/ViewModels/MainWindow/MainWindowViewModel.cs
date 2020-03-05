using Calculator.Modules.Calculator.Interfaces;
using System;

namespace Calculator.ViewModels.MainWindow
{
    public class MainWindowViewModel: ViewModelBase
    {
        private readonly IBasicCalculatorHandler _basicCalculatorHandler;

        public MainWindowViewModel(IBasicCalculatorHandler basicCalculatorHandler)
        {
            _basicCalculatorHandler = basicCalculatorHandler ?? throw new ArgumentNullException(nameof(basicCalculatorHandler));
        }

        #region PROPERTIES Getters/ Setters

        private CalculatorViewModel _calculatorViewModelData;
        public CalculatorViewModel CalculatorViewModelData => 
            _calculatorViewModelData ?? (_calculatorViewModelData = new CalculatorViewModel(_basicCalculatorHandler));

        #endregion
    }
}
