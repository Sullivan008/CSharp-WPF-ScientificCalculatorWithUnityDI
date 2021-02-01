using Calculator.Core.ViewModels;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.Services.Interfaces;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;
using Calculator.Views.Calculator.ViewModels.Interfaces;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel : ViewModelBase, ICalculatorViewModel
    {
        private readonly ICalculatorStorageService _calculatorStorageService;

        private readonly ICalculateService _calculateService;

        private bool _isShiftEnabled;

        public CalculatorViewModel(ICalculatorStorageService calculatorStorageService, ICalculateService calculateService)
        {
            _calculatorStorageService = calculatorStorageService;
            _calculateService = calculateService;
        }

        #region PROPERTIES GETTERS/ SETTERS

        public static int NumberTextBoxMaxLines { get; } = 1;

        public static int NumberTextBoxHeight { get; } = 40;

        public static double NumberTextBoxFontSize { get; } = (double)NumberTextBoxHeight / NumberTextBoxMaxLines * 0.6;


        private string _numberTextBoxValue = default(int).ToString();
        public string NumberTextBoxValue
        {
            get => _numberTextBoxValue;
            set
            {
                _numberTextBoxValue = value;
                OnPropertyChanged();
            }
        }

        private AngleUnitType _selectedAngleUnit = AngleUnitType.Radian;
        public AngleUnitType SelectedAngleUnit
        {
            get => _selectedAngleUnit;
            set
            {
                _selectedAngleUnit = value;
                OnPropertyChanged();
            }
        }

        private string _seriesOfComputerTextBoxValue = string.Empty;
        public string SeriesOfComputerTextBoxValue
        {
            get => _seriesOfComputerTextBoxValue;
            set
            {
                _seriesOfComputerTextBoxValue = value;
                OnPropertyChanged();
            }
        }

        private string _sinButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.Sin);
        public string SinButtonTextBlockValue
        {
            get => _sinButtonTextBlockValue;
            set
            {
                _sinButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _cosButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.Cos);
        public string CosButtonTextBlockValue
        {
            get => _cosButtonTextBlockValue;
            set
            {
                _cosButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _tanButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.Tan);
        public string TanButtonTextBlockValue
        {
            get => _tanButtonTextBlockValue;
            set
            {
                _tanButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _lnButtonTextBlockValue = Functions.GetFunctionText(FunctionType.Ln);
        public string LnButtonTextBlockValue
        {
            get => _lnButtonTextBlockValue;
            set
            {
                _lnButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _logButtonTextBlockValue = Functions.GetFunctionText(FunctionType.Log);
        public string LogButtonTextBlockValue
        {
            get => _logButtonTextBlockValue;
            set
            {
                _logButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}