using System;
using System.Globalization;
using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Views.Calculator.CommandParameters;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.Exceptions;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _equalBtnCommand;
        public ICommand EqualBtnCommand =>
            _equalBtnCommand ?? (_equalBtnCommand = new RelayCommandAsync<OperatorCommandParameter>(EqualBtnCommandExecute));

        private void EqualBtnCommandExecute(OperatorCommandParameter commandParameter)
        {
            if (_calculatorStorageService.LastUserInteractionType != UserInteractionType.EqualBtnPressed)
            {
                while (_calculatorStorageService.LeftParenthesisNumber > 0)
                {
                    RightParenthesisBtnCommand.Execute(new OperatorCommandParameter(UserInteractionType.RightParenthesisBtnPressed, OperatorType.RightParenthesis));
                }

                try
                {
                    if (_calculatorStorageService.LastUserInteractionType != UserInteractionType.AnsBtnPressed &&
                        _calculatorStorageService.LastUserInteractionType != UserInteractionType.EFunctionBtnPressed &&
                        _calculatorStorageService.LastUserInteractionType != UserInteractionType.ModOperatorBtnPressed &&
                        _calculatorStorageService.LastUserInteractionType != UserInteractionType.FactOperatorBtnPressed &&
                        _calculatorStorageService.LastUserInteractionType != UserInteractionType.SquareOfXNumberBtnPressed &&
                        _calculatorStorageService.LastUserInteractionType != UserInteractionType.RightParenthesisBtnPressed)
                    {
                        _calculatorStorageService.AddValueToQueue(NumberTextBoxValue);

                        SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {NumberTextBoxValue}";
                    }
                    
                    SeriesOfComputerTextBoxValue = $"{SeriesOfComputerTextBoxValue.Trim()} {commandParameter.OperatorText}";
                    
                    _calculatorStorageService.AddAllItemsInTheStackToTheQueue();
                    
                    double calculateResult = _calculateService.Calculate(_calculatorStorageService.Queue, _selectedAngleUnit);

                    NumberTextBoxValue = calculateResult.ToString(CultureInfo.CurrentCulture);

                    _calculatorStorageService.SetAns(calculateResult);
                }
                catch (DivideByZeroException)
                {
                    NumberTextBoxValue = "Infinity";
                }
                catch (NotFiniteNumberException)
                {
                    NumberTextBoxValue = "Infinity";
                }
                catch (SyntaxErrorException)
                {
                    NumberTextBoxValue = "Syntax ERROR";
                }
                finally
                {
                    _calculatorStorageService.ClearQueue();
                    _calculatorStorageService.ClearStack();
                    _calculatorStorageService.ClearLeftParenthesisNumber();

                    _calculatorStorageService.SetLastUserInteractionType(UserInteractionType.EqualBtnPressed);
                }
            }
        }
    }
}
