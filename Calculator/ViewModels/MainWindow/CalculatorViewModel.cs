using Calculator.Commands;
using Calculator.Constants;
using Calculator.Enums;
using Calculator.Modules.Calculator.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModels.MainWindow
{
    public class CalculatorViewModel : ViewModelBase
    {
        private readonly IBasicCalculatorHandler _basicCalculatorHandler;

        private bool _shiftButtonIsEnabled;

        public CalculatorViewModel(IBasicCalculatorHandler basicCalculationHandler)
        {
            _basicCalculatorHandler = basicCalculationHandler ?? throw new ArgumentNullException(nameof(basicCalculationHandler));
            _basicCalculatorHandler.Model = this;

            SetShiftButtonContents();
        }

        #region PROPERTIES Getters/ Setters

        public static int NumberTextBoxMaxLines { get; set; } = 1;

        public static int NumberTextBoxHeight { get; set; } = 40;

        public static double NumberTextBoxFontSize { get; set; } = (double)NumberTextBoxHeight / NumberTextBoxMaxLines * 0.6;

        private string _numberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;
        public string NumberTextBoxValue
        {
            get => _numberTextBoxValue;
            set
            {
                _numberTextBoxValue = value;
                OnPropertyChanged();
            }
        }

        private ModeEnum _selectedMode = ModeEnum.Radian;
        public ModeEnum SelectedMode
        {
            get => _selectedMode;
            set
            {
                _selectedMode = value;
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

        private string _sinButtonTextBlockValue;
        public string SinButtonTextBlockValue
        {
            get => _sinButtonTextBlockValue;
            set
            {
                _sinButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _cosButtonTextBlockValue;
        public string CosButtonTextBlockValue
        {
            get => _cosButtonTextBlockValue;
            set
            {
                _cosButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _tanButtonTextBlockValue;
        public string TanButtonTextBlockValue
        {
            get => _tanButtonTextBlockValue;
            set
            {
                _tanButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _lnButtonTextBlockValue;
        public string LnButtonTextBlockValue
        {
            get => _lnButtonTextBlockValue;
            set
            {
                _lnButtonTextBlockValue = value;
                OnPropertyChanged();
            }
        }

        private string _logButtonTextBlockValue;
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

        #region COMMANDS

        #region RIGHT SIDE Commands

        private ICommand _numberButtonCommand;
        public ICommand NumberButtonCommand =>
            _numberButtonCommand ?? (_numberButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.NumberButtonOperation));

        private ICommand _basicArithmeticOperatorButtonCommand;
        public ICommand BasicArithmeticOperatorButtonCommand =>
            _basicArithmeticOperatorButtonCommand ?? (_basicArithmeticOperatorButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.BasicArithmeticOperatorButtonOperation));

        private ICommand _ansButtonCommand;
        public ICommand AnsButtonCommand =>
            _ansButtonCommand ?? (_ansButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.AnsButtonOperation));

        private ICommand _equalButtonCommand;
        public ICommand EqualButtonCommand =>
            _equalButtonCommand ?? (_equalButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.EqualButtonOperation));

        private ICommand _delButtonCommand;
        public ICommand DelButtonCommand =>
            _delButtonCommand ?? (_delButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.DelButtonOperation));

        private ICommand _acButtonCommand;
        public ICommand AcButtonCommand =>
            _acButtonCommand ?? (_acButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.AcButtonOperation));

        #endregion

        #region LEFT SIDE Commands

        private ICommand _leftParButtonCommand;
        public ICommand LeftParButtonCommand =>
            _leftParButtonCommand ?? (_leftParButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.LeftParButtonOperation));

        private ICommand _rightParButtonCommand;
        public ICommand RightParButtonCommand =>
            _rightParButtonCommand ?? (_rightParButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.RightParButtonOperation));

        private ICommand _powButtonCommand;
        public ICommand PowButtonCommand =>
            _powButtonCommand ?? (_powButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.PowButtonOperation));

        private ICommand _squareButtonCommand;
        public ICommand SquareButtonCommand =>
            _squareButtonCommand ?? (_squareButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.SquareButtonOperation));

        private ICommand _trigonometricFunctionsCommand;
        public ICommand TrigonometricFunctionsCommand =>
            _trigonometricFunctionsCommand ?? (_trigonometricFunctionsCommand = new RelayCommandAsync(_basicCalculatorHandler.TrigonometricFunctionsButtonOperation));

        private ICommand _modButtonCommand;
        public ICommand ModButtonCommand =>
            _modButtonCommand ?? (_modButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.ModButtonOperation));

        private ICommand _lnLogButtonCommand;
        public ICommand LnLogButtonCommand =>
            _lnLogButtonCommand ?? (_lnLogButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.LnLogButtonOperation));

        private ICommand _rootButtonCommand;
        public ICommand RootButtonCommand =>
            _rootButtonCommand ?? (_rootButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.RootButtonOperation));

        private ICommand _shiftButtonCommand;
        public ICommand ShiftButtonCommand =>
            _shiftButtonCommand ?? (_shiftButtonCommand = new RelayCommandAsync(ShiftButton));

        private ICommand _eButtonCommand;
        public ICommand EButtonCommand =>
            _eButtonCommand ?? (_eButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.EButtonOperation));

        private ICommand _factButtonCommand;
        public ICommand FactButtonCommand =>
            _factButtonCommand ?? (_factButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.FactButtonOperation));

        private ICommand _plusMinusButtonCommand;
        public ICommand PlusMinusButtonCommand =>
            _plusMinusButtonCommand ?? (_plusMinusButtonCommand = new RelayCommandAsync(_basicCalculatorHandler.PlusMinusButtonOperation));

        #endregion
        #endregion

        #region PRIVATE COMMAND Helper Methods

        private Task ShiftButton()
        {
            _shiftButtonIsEnabled = !_shiftButtonIsEnabled;

            SetShiftButtonContents();

            return Task.FromResult(true);
        }

        #endregion

        #region PRIVATE HELPER Methods

        private void SetShiftButtonContents()
        {
            if (_shiftButtonIsEnabled)
            {
                SinButtonTextBlockValue = "InvSin";
                CosButtonTextBlockValue = "InvCos";
                TanButtonTextBlockValue = "InvTan";
                LnButtonTextBlockValue = "eⁿ";
                LogButtonTextBlockValue = "10ⁿ";
            }
            else
            {
                SinButtonTextBlockValue = "sin";
                CosButtonTextBlockValue = "cos";
                TanButtonTextBlockValue = "tan";
                LnButtonTextBlockValue = "ln";
                LogButtonTextBlockValue = "log";
            }
        }

        #endregion
    }
}