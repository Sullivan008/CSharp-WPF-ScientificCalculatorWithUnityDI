using Calculator.Commands;
using Calculator.Enums;
using Calculator.Logic;
using Calculator.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel : ViewModelBase
    {
        private const string NUMBER_TEXTBOX_DEFAULT_VALUE = "0";
        private const string ANS_VALUE = "Ans";
        private const string LEFT_PAR_VALUE = "(";

        private bool ShiftButtonEnabled { get; set; } = false;

        private CalculatorViewModelLogic modelLogic;

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public CalculatorViewModel()
        {
            modelLogic = new CalculatorViewModelLogic();

            /// Beállítjuk az össze Property értékét az alapértelmezett értékre.
            SetAllPropertyToDefaultValue();

            /// Beállítjuk azoknak a gomboknak az értékét, amelyre a SHIFT gomb hatással van.
            SetShiftButtonContents();
        }

        #region Properties
        private ModeEnum _selectedMode;

        /// <summary>
        ///     Property, amely a kiválasztott módot tartalmazza.
        /// </summary>
        public ModeEnum SelectedMode
        {
            get { return _selectedMode; }
            set { _selectedMode = value; OnPropertyChanged(); modelLogic.FunctionMultipler = Math.PI / 180; }
        }

        private string _numberTextBoxValue;

        /// <summary>
        ///     Property, amely a beviteli mező értékét tartalmazza.
        /// </summary>
        public string NumberTextBoxValue
        {
            get { return _numberTextBoxValue; }
            set { _numberTextBoxValue = value; OnPropertyChanged(); }
        }

        private string _seriesOfCumputerTextBoxValue;

        /// <summary>
        ///     Felületen a beviteli mező fölött megjelnő szövegrész.
        /// </summary>
        public string SeriesOfCumputerTextBoxValue
        {
            get { return _seriesOfCumputerTextBoxValue; }
            set { _seriesOfCumputerTextBoxValue = value; OnPropertyChanged(); }
        }

        /// <summary>
        ///     Felületen a SIN mező fölött megjelnő szövegrész.
        /// </summary>
        private string _sinButtonTextBlockValue;

        public string SinButtonTextBlockValue
        {
            get { return _sinButtonTextBlockValue; }
            set { _sinButtonTextBlockValue = value; OnPropertyChanged(); }
        }

        /// <summary>
        ///     Felületen a COS mező fölött megjelnő szövegrész.
        /// </summary>
        private string _cosButtonTextBlockValue;

        public string CosButtonTextBlockValue
        {
            get { return _cosButtonTextBlockValue; }
            set { _cosButtonTextBlockValue = value; OnPropertyChanged(); }
        }

        /// <summary>
        ///     Felületen a TAN mező fölött megjelnő szövegrész.
        /// </summary>
        private string _tanButtonTextBlockValue;

        public string TanButtonTextBlockValue
        {
            get { return _tanButtonTextBlockValue; }
            set { _tanButtonTextBlockValue = value; OnPropertyChanged(); }
        }

        /// <summary>
        ///     Felületen a LN mező fölött megjelnő szövegrész.
        /// </summary>
        private string _lnButtonTextBlockValue;

        public string LnButtonTextBlockValue
        {
            get { return _lnButtonTextBlockValue; }
            set { _lnButtonTextBlockValue = value; OnPropertyChanged(); }
        }

        /// <summary>
        ///     Felületen a LOG mező fölött megjelnő szövegrész.
        /// </summary>
        private string _logButtonTextBlockValue;

        public string LogButtonTextBlockValue
        {
            get { return _logButtonTextBlockValue; }
            set { _logButtonTextBlockValue = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands

        private ICommand _numberButtonCommand;

        public ICommand NumberButtonCommand
        {
            get
            {
                if (_numberButtonCommand == null)
                {
                    _numberButtonCommand = new RelayCommandAsync((x) => NumberButton(x));
                }

                return _numberButtonCommand;
            }
        }

        private ICommand _powButtonCommand;

        public ICommand PowButtonCommand
        {
            get
            {
                if (_powButtonCommand == null)
                {
                    _powButtonCommand = new RelayCommandAsync((x) => PowButton(x));
                }

                return _powButtonCommand;
            }
        }

        private ICommand _squareButtonCommand;

        public ICommand SquareButtonCommand
        {
            get
            {
                if (_squareButtonCommand == null)
                {
                    _squareButtonCommand = new RelayCommandAsync(() => SquareButton());
                }

                return _squareButtonCommand;
            }
        }

        private ICommand _leftParButtonCommand;

        public ICommand LeftParButtonCommand
        {
            get
            {
                if (_leftParButtonCommand == null)
                {
                    _leftParButtonCommand = new RelayCommandAsync((x) => LeftParButton(x));
                }

                return _leftParButtonCommand;
            }
        }

        private ICommand _rightParButtonCommand;

        public ICommand RightParButtonCommand
        {
            get
            {
                if (_rightParButtonCommand == null)
                {
                    _rightParButtonCommand = new RelayCommandAsync((x) => RightParButton(x));
                }

                return _rightParButtonCommand;
            }
        }

        private ICommand _delButtonCommand;

        public ICommand DelButtonCommand
        {
            get
            {
                if (_delButtonCommand == null)
                {
                    _delButtonCommand = new RelayCommandAsync(() => DelButton());
                }

                return _delButtonCommand;
            }
        }

        private ICommand _acButtonCommand;

        public ICommand AcButtonCommand
        {
            get
            {
                if (_acButtonCommand == null)
                {
                    _acButtonCommand = new RelayCommandAsync(() => AcButton());
                }

                return _acButtonCommand;
            }
        }

        private ICommand _trigonometricFunctionsCommand;

        public ICommand TrigonometricFunctionsCommand
        {
            get
            {
                if (_trigonometricFunctionsCommand == null)
                {
                    _trigonometricFunctionsCommand = new RelayCommandAsync((x) => TrigonometricFunctionsButton(x));
                }

                return _trigonometricFunctionsCommand;
            }
        }

        private ICommand _modButtonCommand;

        public ICommand ModButtonCommand
        {
            get
            {
                if (_modButtonCommand == null)
                {
                    _modButtonCommand = new RelayCommandAsync((x) => ModButton(x));
                }

                return _modButtonCommand;
            }
        }

        private ICommand _lnLogButtonCommand;

        public ICommand LnLogButtonCommand
        {
            get
            {
                if (_lnLogButtonCommand == null)
                {
                    _lnLogButtonCommand = new RelayCommandAsync((x) => LnLogButton(x));
                }

                return _lnLogButtonCommand;
            }
        }

        private ICommand _rootButtonCommand;

        public ICommand RootButtonCommand
        {
            get
            {
                if (_rootButtonCommand == null)
                {
                    _rootButtonCommand = new RelayCommandAsync((x) => RootButton(x));
                }

                return _rootButtonCommand;
            }
        }

        private ICommand _basicArithmeticOperatorButtonCommand;

        public ICommand BasicArithmeticOperatorButtonCommand
        {
            get
            {
                if (_basicArithmeticOperatorButtonCommand == null)
                {
                    _basicArithmeticOperatorButtonCommand = new RelayCommandAsync((x) => BasicArithmeticOperatorButton(x));
                }

                return _basicArithmeticOperatorButtonCommand;
            }
        }

        private ICommand _shiftButtonCommand;

        public ICommand ShiftButtonCommand
        {
            get
            {
                if (_shiftButtonCommand == null)
                {
                    _shiftButtonCommand = new RelayCommandAsync(() => ShiftButton());
                }

                return _shiftButtonCommand;
            }
        }

        private ICommand _eButtonCommand;

        public ICommand EButtonCommand
        {
            get
            {
                if (_eButtonCommand == null)
                {
                    _eButtonCommand = new RelayCommandAsync((x) => EButton(x));
                }

                return _eButtonCommand;
            }
        }

        private ICommand _factButtonCommand;

        public ICommand FactButtonCommand
        {
            get
            {
                if (_factButtonCommand == null)
                {
                    _factButtonCommand = new RelayCommandAsync((x) => FactButton(x));
                }

                return _factButtonCommand;
            }
        }

        private ICommand _plusMinusButtonCommand;

        public ICommand PlusMinusButtonCommand
        {
            get
            {
                if (_plusMinusButtonCommand == null)
                {
                    _plusMinusButtonCommand = new RelayCommandAsync(() => PlusMinusButton());
                }

                return _plusMinusButtonCommand;
            }
        }

        private ICommand _ansButtonCommand;

        public ICommand AnsButtonCommand
        {
            get
            {
                if (_ansButtonCommand == null)
                {
                    _ansButtonCommand = new RelayCommandAsync((x) => AnsButton(x));
                }

                return _ansButtonCommand;
            }
        }

        private ICommand _equalButtonCommand;

        public ICommand EqualButtonCommand
        {
            get
            {
                if (_equalButtonCommand == null)
                {
                    _equalButtonCommand = new RelayCommandAsync((x) => EqualButton(x));
                }

                return _equalButtonCommand;
            }
        }

        private ICommand _offButtonCommand;

        public ICommand OffButtonCommand
        {
            get
            {
                if (_offButtonCommand == null)
                {
                    _offButtonCommand = new RelayCommandAsync(() => OffButton());
                }

                return _offButtonCommand;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        ///     Számjegy lenyomásához tartozó metódus. Ez a metódus hívódik meg akkor is, ha
        ///     a felhasználó tizedes vesszőt nyom le.
        /// </summary>
        /// <param name="numberData">
        ///     XAML felől kapott Command paraméter. 
        ///     A számok 0-9-ig. Vagy egy tizedesvessző (,).
        /// </param>
        private Task NumberButton(object numberData)
        {
            if (modelLogic.LastOperationIsEqual)
            {
                SetAllPropertyToDefaultValue();
            }

            if (numberData.Equals(","))
            {
                if (!NumberTextBoxValue.Contains(","))
                {
                    NumberTextBoxValue += ",";
                }
            }
            else
            {
                if(!NumberTextBoxValue.Equals(string.Empty))
                {
                    if (NumberTextBoxValue.Equals(NUMBER_TEXTBOX_DEFAULT_VALUE))
                    {
                        NumberTextBoxValue = string.Empty;
                        NumberTextBoxValue = numberData.ToString();
                    }
                    else
                    {
                        NumberTextBoxValue += numberData.ToString();
                    }
                }
            }


            return Task.FromResult(true);
        }

        /// <summary>
        ///     Az alapvető műveleti jel lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="paramData">
        ///     XAML felől kapott Command paraméter: +, -, *, / </param>
        private Task BasicArithmeticOperatorButton(object paramData)
        {
            /// Ha az utolsó művelet egy Kalkuláció volt, akkor mivel OPERÁTOR műveletet szeretnénk végrehajtani,
            /// így tovább az ANS értékkel fogunk számolni, tehát ez lesz az első OPERANDUS-unk.
            if (modelLogic.LastOperationIsEqual || NumberTextBoxValue.Equals(ANS_VALUE))
            {
                SetAnsValue();
            }
            else
            {
                if (NumberTextBoxValue.Length != 0)
                {
                    /// Csak akkor adjuk hozzá a számot a POSTFIX QUEUE-hoz, hogyha a SOROZATSZÁMÍTÁSI OUTPUT mező értéke
                    /// nem %-al végződik!
                    if (!SeriesOfCumputerTextBoxValue.EndsWith("% "))
                    {
                        SeriesOfCumputerTextBoxValue += NumberTextBoxValue;
                        modelLogic.AddValueForPostfixQueue(NumberTextBoxValue);
                    }
                }
            }

            SeriesOfCumputerTextBoxValue += $" {paramData} ";
            SetNumberTextBoxValueDefaultValue();

            modelLogic.AddBasicArithmeticOperatorOperatorStack(paramData.ToString());

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A számítás végrehajtását végrehajtó metódus.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: =</param>
        private Task EqualButton(object paramData)
        {
            /// Minden NYITÓ Zárójelhez hozzá kell rendelni a ZÁRÓ Zárójelet.
            /// Ezt csak akkor hajtjuk végre, ha még vannak nyitó zárójeleink.
            while (modelLogic.LeftParNumber > 0)
            {
                RightParButton(")");
            }

            if (!modelLogic.LastOperationIsEqual)
            {
                if (NumberTextBoxValue.Length != 0)
                {
                    if (!SeriesOfCumputerTextBoxValue.EndsWith(")") &&
                        !SeriesOfCumputerTextBoxValue.EndsWith("% "))
                    {
                        SeriesOfCumputerTextBoxValue += NumberTextBoxValue;
                        modelLogic.AddValueForPostfixQueue(NumberTextBoxValue);
                    }
                }

                SeriesOfCumputerTextBoxValue += $" {paramData}";

                /// A függvényszorzó beállítása, annak függvényében, hogy milyen módba van állítva a számológép.
                if(Convert.ToBoolean((int)_selectedMode))
                {
                    modelLogic.SetFunctionMultiplerValueToRadian();
                }
                else
                {
                    modelLogic.SetFunctionMultiplerValueToDegree();
                }

                NumberTextBoxValue = modelLogic.Calculate();
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Az ANS gomb lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter= Ans.</param>
        private Task AnsButton(object paramData)
        {
            if (modelLogic.LastOperationIsEqual)
            {
                SeriesOfCumputerTextBoxValue = string.Empty;
            }

            NumberTextBoxValue = string.Empty;

            if (!SeriesOfCumputerTextBoxValue.EndsWith((string)paramData))
            {
                /// Az ANS értéket hozzáadjuk a POSTFIX SOR-hoz.
                modelLogic.AddValueForPostfixQueue(modelLogic.AnsValue.ToString());

                SeriesOfCumputerTextBoxValue += (string)paramData;
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A DEL gomb lenyomásához tartozó metódus. Visszatörlünk egy karaktert,
        ///     az INPUT Beviteli mezőből.
        /// </summary>
        private Task DelButton()
        {
            NumberTextBoxValue = NumberTextBoxValue.Remove(NumberTextBoxValue.Length - 1);

            if (NumberTextBoxValue.Length == 0)
            {
                SetNumberTextBoxValueDefaultValue();
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Az AC gomb lenyomásához tartozó metódus. Minden Property-t az alapértelmezett értékre
        ///     állítunk.
        /// </summary>
        private Task AcButton()
        {
            modelLogic.ClearBasicData();

            SetAllPropertyToDefaultValue();

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A bal zárójel gomb lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: (.</param>
        private Task LeftParButton(object paramData)
        {
            if (modelLogic.LastOperationIsEqual)
            {
                SetAllPropertyToDefaultValue();
            }

            SeriesOfCumputerTextBoxValue += paramData;

            modelLogic.AddOtherOperatorsOperatorStack(paramData.ToString());

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A jobb zárójel gomb lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: ).</param>
        private Task RightParButton(object paramData)
        {
            try
            {
                /// Csak akkor hajtunk végre műveletet, ha már van BAL ZÁRÓJELÜNK.
                if (modelLogic.LeftParNumber != 0)
                {
                    if (NumberTextBoxValue.Length != 0)
                    {
                        SeriesOfCumputerTextBoxValue += NumberTextBoxValue;
                        modelLogic.AddValueForPostfixQueue(NumberTextBoxValue);

                        NumberTextBoxValue = string.Empty;
                    }

                    /// Ha nem hajtottunk végre bármilyen FÜGGVÉNY műveletet (Sin, Cos, stb...), akkor...
                    if (modelLogic.FuntionsNumber == 0)
                    {
                        int oldLeftPairNumber = modelLogic.LeftParNumber;

                        modelLogic.AddItemsPostfixQueueRightParOperationWithoutFunction();

                        /// Annyi ZÁRÓ zárójelet hozzáadunk, ahány OPERÁTORT hozzáadunk a POSTFIX SOR-hoz. 
                        for (int i = 0; i < oldLeftPairNumber - modelLogic.LeftParNumber; i++)
                        {
                            SeriesOfCumputerTextBoxValue += paramData;
                        }
                    }
                    else
                    {
                        int oldLeftPairNumber = modelLogic.LeftParNumber;

                        modelLogic.AddItemsPostfixQueueRightParOperationWithFunction();

                        /// Annyi ZÁRÓ zárójelet hozzáadunk, ahány OPERÁTORT hozzáadunk a POSTFIX SOR-hoz.
                        for (int i = 0; i < oldLeftPairNumber - modelLogic.LeftParNumber; i++)
                        {
                            SeriesOfCumputerTextBoxValue += paramData;
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                NumberTextBoxValue = "Syntaxt ERROR";
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A szögfüggvények lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">
        ///     XAML felől kapott Command paraméter: Sin, InvSin, Cos, InvCos, Tan, InvTan, sqrt.
        /// </param>
        private Task TrigonometricFunctionsButton(object paramData)
        {
            if (modelLogic.LastOperationIsEqual)
            {
                SeriesOfCumputerTextBoxValue = string.Empty;
                modelLogic.LastOperationIsEqual = false;
            }

            SeriesOfCumputerTextBoxValue += (paramData.ToString() == "sqrt" ? "√" : paramData.ToString()) + LEFT_PAR_VALUE;

            modelLogic.AddOtherOperatorsOperatorStack(paramData.ToString());
            modelLogic.AddOtherOperatorsOperatorStack(LEFT_PAR_VALUE);

            SetNumberTextBoxValueDefaultValue();

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A hatványozás lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: ^</param>
        private Task PowButton(object paramData)
        {
            /// Ha az utolsó művelet egy Kalkuláció volt, akkor mivel OPERÁTOR műveletet szeretnénk végrehajtani,
            /// így tovább az ANS értékkel fogunk számolni, tehát ez lesz az első OPERANDUS-unk.
            if (modelLogic.LastOperationIsEqual)
            {
                SetAnsValue();
            }
            else
            {
                SeriesOfCumputerTextBoxValue += NumberTextBoxValue;

                modelLogic.AddValueForPostfixQueue(NumberTextBoxValue);
            }

            SeriesOfCumputerTextBoxValue += paramData.ToString();
            modelLogic.AddOtherOperatorsOperatorStack(paramData.ToString());

            SetNumberTextBoxValueDefaultValue();

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A négyzetes hatványozás lenyomásához tartozó gomb.
        /// </summary>
        private Task SquareButton()
        {
            int exponent = 2;

            /// Ha az utolsó művelet egy Kalkuláció volt, akkor mivel OPERÁTOR műveletet szeretnénk végrehajtani,
            /// így tovább az ANS értékkel fogunk számolni, tehát ez lesz az első OPERANDUS-unk.
            if (modelLogic.LastOperationIsEqual)
            {
                SeriesOfCumputerTextBoxValue = $"{ANS_VALUE}^{exponent}";

                modelLogic.AddValueForPostfixQueue(modelLogic.AnsValue.ToString());

                modelLogic.LastOperationIsEqual = false;
            }
            else
            {
                SeriesOfCumputerTextBoxValue += $"{NumberTextBoxValue}^{exponent}";

                modelLogic.AddValueForPostfixQueue(NumberTextBoxValue);
            }

            modelLogic.AddValueForPostfixQueue(exponent.ToString());
            modelLogic.AddOtherOperatorsOperatorStack("^");

            NumberTextBoxValue = string.Empty;

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A maradékos osztás lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: %</param>
        private Task ModButton(object paramData)
        {
            /// Ha az utolsó művelet egy Kalkuláció volt, akkor mivel OPERÁTOR műveletet szeretnénk végrehajtani,
            /// így tovább az ANS értékkel fogunk számolni, tehát ez lesz az első OPERANDUS-unk.
            if (modelLogic.LastOperationIsEqual)
            {
                SetAnsValue();
            }
            else
            {
                SeriesOfCumputerTextBoxValue += NumberTextBoxValue;

                modelLogic.AddValueForPostfixQueue(NumberTextBoxValue);
            }

            SeriesOfCumputerTextBoxValue += $" {paramData} ";
            modelLogic.AddOtherOperatorsOperatorStack(paramData.ToString());

            SetNumberTextBoxValueDefaultValue();

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Az Ln és Log lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: ln, log, eⁿ, 10ⁿ</param>
        private Task LnLogButton(object paramData)
        {
            if (paramData.Equals("ln") || paramData.Equals("log"))
            {
                if (modelLogic.LastOperationIsEqual)
                {
                    SeriesOfCumputerTextBoxValue = paramData + LEFT_PAR_VALUE;

                    modelLogic.LastOperationIsEqual = false;
                }
                else
                {
                    SeriesOfCumputerTextBoxValue += paramData + LEFT_PAR_VALUE;
                }

                modelLogic.AddOtherOperatorsOperatorStack(paramData.ToString());
                modelLogic.AddOtherOperatorsOperatorStack(LEFT_PAR_VALUE);
            }
            else
            {
                string powerValue = "^";

                /// Az N karaktert levágjuk a paraméterben érkező függvény operátorról, majd kicseréljük hatvány karakterre..
                paramData = $"{paramData.ToString().Remove(paramData.ToString().Length - 1)}{powerValue}";

                if (modelLogic.LastOperationIsEqual)
                {
                    SeriesOfCumputerTextBoxValue = paramData.ToString();

                    modelLogic.LastOperationIsEqual = false;
                }
                else
                {
                    SeriesOfCumputerTextBoxValue += paramData.ToString();
                }

                /// Vizsgálat, hogy a bejövő paraméter egy "e" szám volt-e, ugyanis, akkor ez határozza meg, 
                /// hogy a POSTFIX SOR-hoz milyen értéket adunk hozzá.
                if (paramData.Equals($"e{powerValue}"))
                {
                    modelLogic.AddValueForPostfixQueue(Math.E.ToString());
                }
                else
                {
                    modelLogic.AddValueForPostfixQueue(10.ToString());
                }

                modelLogic.AddOtherOperatorsOperatorStack($"{powerValue}");

                SetNumberTextBoxValueDefaultValue();
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Az N. Gyök alatt az X lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: ln, log, eⁿ, 10ⁿ</param>
        private Task RootButton(object paramData)
        {
            if (modelLogic.LastOperationIsEqual)
            {
                SeriesOfCumputerTextBoxValue = $"{ANS_VALUE} {paramData} {LEFT_PAR_VALUE}";

                modelLogic.AddValueForPostfixQueue(modelLogic.AnsValue.ToString());

                modelLogic.LastOperationIsEqual = false;
            }
            else
            {
                SeriesOfCumputerTextBoxValue += $"{NumberTextBoxValue} {paramData} {LEFT_PAR_VALUE}";

                modelLogic.AddValueForPostfixQueue(NumberTextBoxValue);
            }

            /// Az y karaktert eltávolítva adjuk hozzá az OPERÁTOR VEREM-hez a bejövő függvény paramétert.
            modelLogic.AddOtherOperatorsOperatorStack(paramData.ToString().Remove(0,1));
            modelLogic.AddOtherOperatorsOperatorStack(LEFT_PAR_VALUE);

            SetNumberTextBoxValueDefaultValue();

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Az SHIFT lenyomásához tartozó gomb.
        /// </summary>
        private Task ShiftButton()
        {
            ShiftButtonEnabled = !ShiftButtonEnabled;

            SetShiftButtonContents();

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Az E lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: e</param>
        private Task EButton(object paramData)
        {
            if (modelLogic.LastOperationIsEqual)
            {
                SeriesOfCumputerTextBoxValue = paramData.ToString();

                modelLogic.LastOperationIsEqual = false;
            }
            else
            {
                SeriesOfCumputerTextBoxValue += paramData.ToString();
            }

            modelLogic.AddValueForPostfixQueue(Math.E.ToString());

            NumberTextBoxValue = string.Empty;

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A Fact lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: !</param>
        private Task FactButton(object paramData)
        {
            if(!SeriesOfCumputerTextBoxValue.EndsWith("!"))
            {
                if (modelLogic.LastOperationIsEqual)
                {
                    SeriesOfCumputerTextBoxValue = $"{ANS_VALUE}{paramData}";

                    modelLogic.AddValueForPostfixQueue(modelLogic.AnsValue.ToString());

                    modelLogic.LastOperationIsEqual = false;
                }
                else
                {
                    SeriesOfCumputerTextBoxValue += $"{NumberTextBoxValue}{paramData}";

                    modelLogic.AddValueForPostfixQueue(_numberTextBoxValue);
                }

                modelLogic.AddOtherOperatorsOperatorStack("fact");

                NumberTextBoxValue = string.Empty;
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     A ± lenyomásához tartozó gomb.
        /// </summary>
        private Task PlusMinusButton()
        {
            string minusValue = "-";

            if (!NumberTextBoxValue.Equals(NUMBER_TEXTBOX_DEFAULT_VALUE) && NumberTextBoxValue.Length != 0)
            {
                if (!NumberTextBoxValue.StartsWith(minusValue))
                {
                    NumberTextBoxValue = NumberTextBoxValue.Insert(0, minusValue);
                }
                else
                {
                    NumberTextBoxValue = NumberTextBoxValue.Remove(0, 1);
                }
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Metódus, amely a Globals-t használva meghívja a MainVieewModel CloseCommand-ot.
        /// </summary>
        private Task OffButton()
        {
            Globals.MainViewModelData?.CloseCommand?.Execute(null);

            return Task.FromResult(true);
        }
        #endregion

        #region PRIVATE HELPER Methods
        /// <summary>
        ///     Metódus, amely midnen a felületen megjelenő property értékét egy alapértelmezett értékre állítja be.
        /// </summary>
        private void SetAllPropertyToDefaultValue()
        {
            SelectedMode = ModeEnum.Radian;

            SetNumberTextBoxValueDefaultValue();

            SeriesOfCumputerTextBoxValue = string.Empty;

            modelLogic.LastOperationIsEqual = false;
        }

        /// <summary>
        ///     Az INPUT beviteli mezőnek beállítja a DEFAULT Value-t (Azaz 0-át)
        /// </summary>
        private void SetNumberTextBoxValueDefaultValue()
        {
            NumberTextBoxValue = NUMBER_TEXTBOX_DEFAULT_VALUE.ToString();
        }

        /// <summary>
        ///     Ha a SHIFT gomb engedélyezve van, akkor beállítjuk a gombban található megfelelő CONTENT-eket.
        /// </summary>
        private void SetShiftButtonContents()
        {
            if (ShiftButtonEnabled)
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

        /// <summary>
        ///     Metódus, amely beállítja az Ans (a legutolsó számítási eredmény) értékeket, ha azzal kezdünk el tovább számolni.
        /// </summary>
        private void SetAnsValue()
        {
            /// A SOROZATSZÁMÍTÁSI OUTPUT mezőhöz hozzáfűzzük az ANS értékét, majd az Ans értékét
            /// hozzáadjuk a POSTFIX SOR-hoz.
            SeriesOfCumputerTextBoxValue = ANS_VALUE;
            modelLogic.AddValueForPostfixQueue(modelLogic.AnsValue.ToString());

            /// Az utolsó aritmetikai művelet így már nem az egyenlőség jel lesz.
            modelLogic.LastOperationIsEqual = false;
        }
        #endregion
    }
}