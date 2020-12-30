using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Calculator.Constants;
using Calculator.Enums;
using Calculator.Modules.Calculator.Interfaces;
using Calculator.ViewModels.Views;

namespace Calculator.Modules.Calculator.Handlers
{
    public class BasicCalculatorHandler : CommonCalculationHandler, IBasicCalculatorHandler
    {
        private readonly IBasicArithmeticOperatorHandler _basicArithmeticOperatorHandler;
        private readonly IRightParenthesisHandler _rightParenthesisHandler;
        private readonly ICalculateHandler _calculateHandler;

        public CalculatorViewModel Model { get; set; }

        private readonly Queue<string> _postfixQueue;
        private readonly Stack _operatorStack;

        private int _leftParenthesisNumber;
        private int _functionsNumber;

        private double _ansValue;
        private double _functionMultiplier = 1;

        private bool _lastOperationIsEqual;

        public BasicCalculatorHandler(IBasicArithmeticOperatorHandler basicArithmeticOperatorHandler, IRightParenthesisHandler rightParenthesisHandler,
            ICalculateHandler calculateHandler)
        {
            _postfixQueue = new Queue<string>();
            _operatorStack = new Stack();

            _basicArithmeticOperatorHandler = basicArithmeticOperatorHandler ?? throw new ArgumentNullException(nameof(basicArithmeticOperatorHandler));
            _rightParenthesisHandler = rightParenthesisHandler ?? throw new ArgumentNullException(nameof(rightParenthesisHandler));
            _calculateHandler = calculateHandler ?? throw new ArgumentNullException(nameof(calculateHandler));
        }

        public void NumberButtonOperation(object numberText)
        {
            if (_lastOperationIsEqual)
            {
                SetModelPropertiesDefaultValue();
                _lastOperationIsEqual = false;
            }

            if (numberText.Equals(CalculatorViewModelTexts.CommaTextValue))
            {
                if (!Model.NumberTextBoxValue.Contains(CalculatorViewModelTexts.CommaTextValue))
                {
                    Model.NumberTextBoxValue += CalculatorViewModelTexts.CommaTextValue;
                }
            }
            else
            {
                if (!Model.NumberTextBoxValue.Equals(string.Empty))
                {
                    if (Model.NumberTextBoxValue.Equals(CalculatorViewModelTexts.NumberTextBoxDefaultValue))
                    {
                        Model.NumberTextBoxValue = string.Empty;
                        Model.NumberTextBoxValue = numberText.ToString();
                    }
                    else
                    {
                        Model.NumberTextBoxValue += numberText.ToString();
                    }
                }
            }
        }

        public void BasicArithmeticOperatorButtonOperation(object arithmeticOperatorText)
        {
            if (_lastOperationIsEqual || Model.NumberTextBoxValue.Equals(CalculatorViewModelTexts.AnsTextValue))
            {
                SetAnsValue();
            }
            else
            {
                if (Model.NumberTextBoxValue.Length != 0)
                {
                    if (!Model.SeriesOfComputerTextBoxValue.EndsWith($"{CalculatorViewModelTexts.PercentageTextValue} "))
                    {
                        Model.SeriesOfComputerTextBoxValue += Model.NumberTextBoxValue;
                        AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);
                    }
                }
            }

            Model.SeriesOfComputerTextBoxValue += $" {arithmeticOperatorText} ";
            Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;

            _basicArithmeticOperatorHandler.AddBasicArithmeticOperatorOperatorStack(_operatorStack, _postfixQueue, arithmeticOperatorText.ToString());
        }

        public void EqualButtonOperation(object equalText)
        {
            // Minden NYITÓ Zárójelhez hozzá kell rendelni a ZÁRÓ Zárójelet.
            while (_leftParenthesisNumber > 0)
            {
                RightParButtonOperation(CalculatorViewModelTexts.RightParenthesisTextValue);
            }

            if (!_lastOperationIsEqual)
            {
                if (Model.NumberTextBoxValue.Length != 0)
                {
                    if (!Model.SeriesOfComputerTextBoxValue.EndsWith(CalculatorViewModelTexts.RightParenthesisTextValue) &&
                        !Model.SeriesOfComputerTextBoxValue.EndsWith($"{CalculatorViewModelTexts.PercentageTextValue} "))
                    {
                        Model.SeriesOfComputerTextBoxValue += Model.NumberTextBoxValue;
                        AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);
                    }
                }

                Model.SeriesOfComputerTextBoxValue += $" {equalText}";

                if (Model.SelectedMode == ModeEnum.Radian)
                {
                    _functionMultiplier = 1;
                }
                else
                {
                    _functionMultiplier = Math.PI / 180;
                }

                Model.NumberTextBoxValue =
                    _calculateHandler.Calculate(_operatorStack, _postfixQueue, _functionMultiplier, ref _ansValue);

                ClearBasicData();

                _lastOperationIsEqual = true;
            }
        }

        public void AnsButtonOperation(object ansText)
        {
            if (_lastOperationIsEqual)
            {
                Model.SeriesOfComputerTextBoxValue = string.Empty;
            }

            Model.NumberTextBoxValue = string.Empty;

            if (!Model.SeriesOfComputerTextBoxValue.EndsWith((string)ansText))
            {
                AddValueForPostfixQueue(_postfixQueue, _ansValue.ToString(CultureInfo.CurrentCulture));

                Model.SeriesOfComputerTextBoxValue += (string)ansText;
            }
        }

        public Task DelButtonOperation()
        {
            Model.NumberTextBoxValue = Model.NumberTextBoxValue.Remove(Model.NumberTextBoxValue.Length - 1);

            if (Model.NumberTextBoxValue.Length == 0)
            {
                Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;
            }

            return Task.FromResult(true);
        }

        public Task AcButtonOperation()
        {
            ClearBasicData();

            SetModelPropertiesDefaultValue();

            return Task.FromResult(true);
        }

        public void LeftParButtonOperation(object leftParenthesisText)
        {
            if (_lastOperationIsEqual)
            {
                SetModelPropertiesDefaultValue();
            }

            Model.SeriesOfComputerTextBoxValue += leftParenthesisText;

            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, leftParenthesisText.ToString());
        }

        public void RightParButtonOperation(object rightParenthesisText)
        {
            try
            {
                if (_leftParenthesisNumber != 0)
                {
                    if (Model.NumberTextBoxValue.Length != 0)
                    {
                        Model.SeriesOfComputerTextBoxValue += Model.NumberTextBoxValue;
                        AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);

                        Model.NumberTextBoxValue = string.Empty;
                    }

                    // Ha nem hajtottunk végre bármilyen FÜGGVÉNY műveletet (Sin, Cos, stb...), akkor...
                    if (_functionsNumber == 0)
                    {
                        int oldLeftPairNumber = _leftParenthesisNumber;

                        _rightParenthesisHandler.AddItemsPostfixQueueRightParOperationWithoutFunction(_operatorStack, _postfixQueue,
                            ref _leftParenthesisNumber);

                        // Annyi ZÁRÓ zárójelet hozzáadunk, ahány OPERÁTORT hozzáadunk a POSTFIX SOR-hoz. 
                        for (int i = 0; i < oldLeftPairNumber - _leftParenthesisNumber; i++)
                        {
                            Model.SeriesOfComputerTextBoxValue += rightParenthesisText;
                        }
                    }
                    else
                    {
                        int oldLeftPairNumber = _leftParenthesisNumber;

                        _rightParenthesisHandler.AddItemsPostfixQueueRightParOperationWithFunction(_operatorStack, _postfixQueue,
                            ref _functionsNumber, ref _leftParenthesisNumber);

                        // Annyi ZÁRÓ zárójelet hozzáadunk, ahány OPERÁTORT hozzáadunk a POSTFIX SOR-hoz.
                        for (int i = 0; i < oldLeftPairNumber - _leftParenthesisNumber; i++)
                        {
                            Model.SeriesOfComputerTextBoxValue += rightParenthesisText;
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                Model.NumberTextBoxValue = CalculatorViewModelTexts.SyntaxErrorTextValue;
            }
        }

        public void TrigonometricFunctionsButtonOperation(object trigonometricFunctionText)
        {
            if (_lastOperationIsEqual)
            {
                Model.SeriesOfComputerTextBoxValue = string.Empty;
                _lastOperationIsEqual = false;
            }

            Model.SeriesOfComputerTextBoxValue += (trigonometricFunctionText.ToString() == CalculatorViewModelTexts.SqrtTextValue ?
                CalculatorViewModelTexts.SquareRootTextValue :
                trigonometricFunctionText.ToString()) + CalculatorViewModelTexts.LeftParenthesisTextValue;

            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, trigonometricFunctionText.ToString());
            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, CalculatorViewModelTexts.LeftParenthesisTextValue);

            Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;
        }

        public void PowButtonOperation(object powFunctionText)
        {
            if (_lastOperationIsEqual)
            {
                SetAnsValue();
            }
            else
            {
                Model.SeriesOfComputerTextBoxValue += Model.NumberTextBoxValue;

                AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);
            }

            Model.SeriesOfComputerTextBoxValue += powFunctionText.ToString();
            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, powFunctionText.ToString());

            Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;
        }

        public Task SquareButtonOperation()
        {
            const int exponent = 2;

            if (_lastOperationIsEqual)
            {
                Model.SeriesOfComputerTextBoxValue = $"{CalculatorViewModelTexts.AnsTextValue}{CalculatorViewModelTexts.SquareTextValue}{exponent}";

                AddValueForPostfixQueue(_postfixQueue, _ansValue.ToString(CultureInfo.CurrentCulture));

                _lastOperationIsEqual = false;
            }
            else
            {
                Model.SeriesOfComputerTextBoxValue += $"{Model.NumberTextBoxValue}{CalculatorViewModelTexts.SquareTextValue}{exponent}";

                AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);
            }

            AddValueForPostfixQueue(_postfixQueue, exponent.ToString());
            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, CalculatorViewModelTexts.SquareTextValue);

            Model.NumberTextBoxValue = string.Empty;

            return Task.FromResult(true);
        }

        public void ModButtonOperation(object modFunctionText)
        {
            if (_lastOperationIsEqual)
            {
                SetAnsValue();
            }
            else
            {
                Model.SeriesOfComputerTextBoxValue += Model.NumberTextBoxValue;

                AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);
            }

            Model.SeriesOfComputerTextBoxValue += $" {modFunctionText} ";
            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, modFunctionText.ToString());

            Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;
        }

        public void LnLogButtonOperation(object paramData)
        {
            const string powerValue = CalculatorViewModelTexts.SquareTextValue;

            if (paramData.Equals(CalculatorViewModelTexts.LnTextValue) || paramData.Equals(CalculatorViewModelTexts.LogTextValue))
            {
                if (_lastOperationIsEqual)
                {
                    Model.SeriesOfComputerTextBoxValue = paramData + CalculatorViewModelTexts.LeftParenthesisTextValue;

                    _lastOperationIsEqual = false;
                }
                else
                {
                    Model.SeriesOfComputerTextBoxValue += paramData + CalculatorViewModelTexts.LeftParenthesisTextValue;
                }

                AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, paramData.ToString());
                AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, CalculatorViewModelTexts.LeftParenthesisTextValue);
            }
            else
            {
                paramData = $"{paramData.ToString().Remove(paramData.ToString().Length - 1)}{powerValue}";

                if (_lastOperationIsEqual)
                {
                    Model.SeriesOfComputerTextBoxValue = paramData.ToString();

                    _lastOperationIsEqual = false;
                }
                else
                {
                    Model.SeriesOfComputerTextBoxValue += paramData.ToString();
                }

                AddValueForPostfixQueue(_postfixQueue, paramData.Equals($"{CalculatorViewModelTexts.ETextValue}{powerValue}") ?
                    Math.E.ToString(CultureInfo.CurrentCulture) :
                    10.ToString());

                AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, $"{powerValue}");

                Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;
            }
        }

        public void RootButtonOperation(object paramData)
        {
            if (_lastOperationIsEqual)
            {
                Model.SeriesOfComputerTextBoxValue = $"{CalculatorViewModelTexts.AnsTextValue} {paramData} {CalculatorViewModelTexts.LeftParenthesisTextValue}";

                AddValueForPostfixQueue(_postfixQueue, _ansValue.ToString(CultureInfo.CurrentCulture));

                _lastOperationIsEqual = false;
            }
            else
            {
                Model.SeriesOfComputerTextBoxValue += $"{Model.NumberTextBoxValue} {paramData} {CalculatorViewModelTexts.LeftParenthesisTextValue}";

                AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);
            }

            // Az y karaktert eltávolítva adjuk hozzá az OPERÁTOR VEREM-hez a bejövő függvény paramétert.
            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, paramData.ToString().Remove(0, 1));
            AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, CalculatorViewModelTexts.LeftParenthesisTextValue);

            Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;
        }

        public void EButtonOperation(object eFunctionText)
        {
            if (_lastOperationIsEqual)
            {
                Model.SeriesOfComputerTextBoxValue = eFunctionText.ToString();

                _lastOperationIsEqual = false;
            }
            else
            {
                Model.SeriesOfComputerTextBoxValue += eFunctionText.ToString();
            }

            AddValueForPostfixQueue(_postfixQueue, Math.E.ToString(CultureInfo.CurrentCulture));

            Model.NumberTextBoxValue = string.Empty;
        }

        public void FactButtonOperation(object factFunctionText)
        {
            if (!Model.SeriesOfComputerTextBoxValue.EndsWith(CalculatorViewModelTexts.FactOperatorTextValue))
            {
                if (_lastOperationIsEqual)
                {
                    Model.SeriesOfComputerTextBoxValue = $"{CalculatorViewModelTexts.AnsTextValue}{factFunctionText}";

                    AddValueForPostfixQueue(_postfixQueue, _ansValue.ToString(CultureInfo.CurrentCulture));

                    _lastOperationIsEqual = false;
                }
                else
                {
                    Model.SeriesOfComputerTextBoxValue += $"{Model.NumberTextBoxValue}{factFunctionText}";

                    AddValueForPostfixQueue(_postfixQueue, Model.NumberTextBoxValue);
                }

                AddOtherOperatorsOperatorStack(_operatorStack, ref _leftParenthesisNumber, ref _functionsNumber, CalculatorViewModelTexts.FactTextValue);

                Model.NumberTextBoxValue = string.Empty;
            }
        }

        public Task PlusMinusButtonOperation()
        {
            const string minusValue = "-";

            if (!Model.NumberTextBoxValue.Equals(CalculatorViewModelTexts.NumberTextBoxDefaultValue) && Model.NumberTextBoxValue.Length != 0)
            {
                Model.NumberTextBoxValue = !Model.NumberTextBoxValue.StartsWith(minusValue) ?
                    Model.NumberTextBoxValue.Insert(0, minusValue) :
                    Model.NumberTextBoxValue.Remove(0, 1);
            }

            return Task.FromResult(true);
        }

        #region PRIVATE Helper Methods

        private void SetAnsValue()
        {
            Model.SeriesOfComputerTextBoxValue = CalculatorViewModelTexts.AnsTextValue;

            AddValueForPostfixQueue(_postfixQueue, _ansValue.ToString(CultureInfo.CurrentCulture));

            _lastOperationIsEqual = false;
        }

        private void SetModelPropertiesDefaultValue()
        {
            Model.SelectedMode = ModeEnum.Radian;

            Model.NumberTextBoxValue = CalculatorViewModelTexts.NumberTextBoxDefaultValue;

            Model.SeriesOfComputerTextBoxValue = string.Empty;
        }

        private void ClearBasicData()
        {
            _postfixQueue.Clear();
            _operatorStack.Clear();

            _leftParenthesisNumber = 0;
        }

        #endregion
    }
}
