using Calculator.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Calculator.Modules.Calculator.Interfaces;

namespace Calculator.Modules.Calculator.Handlers
{
    public class CalculateHandler : CommonCalculationHandler, ICalculateHandler
    {
        public string Calculate(Stack operatorStack, Queue<string> postfixQueue, double functionMultiplier, ref double ansValue)
        {
            Stack prefixStack = new Stack();

            string firstOperator = string.Empty;
            string secondOperator = string.Empty;

            while (operatorStack.Count != 0)
            {
                postfixQueue.Enqueue((string)operatorStack.Pop());
            }

            try
            {
                while (postfixQueue.Count != 0)
                {
                    string currentPostfixQueueItem = postfixQueue.Dequeue();

                    if (PostfixQueueItemIsNumeric(currentPostfixQueueItem))
                    {
                        prefixStack.Push(currentPostfixQueueItem);
                    }
                    else if (PostfixQueueItemIsAlphanumeric(currentPostfixQueueItem))
                    {
                        firstOperator = (string)prefixStack.Pop();

                        switch (currentPostfixQueueItem)
                        {
                            case CalculatorViewModelTexts.SinTextValue:
                                ansValue = Math.Sin(double.Parse(firstOperator) * functionMultiplier);
                                break;
                            case CalculatorViewModelTexts.InvSinTextValue:
                                ansValue = Math.Asin(double.Parse(firstOperator) / functionMultiplier);
                                break;
                            case CalculatorViewModelTexts.CosTextValue:
                                ansValue = Math.Cos(double.Parse(firstOperator) * functionMultiplier);
                                break;
                            case CalculatorViewModelTexts.InvCosTextValue:
                                ansValue = Math.Acos(double.Parse(firstOperator) / functionMultiplier);
                                break;
                            case CalculatorViewModelTexts.TanTextValue:
                                ansValue = Math.Tan(double.Parse(firstOperator) * functionMultiplier);
                                break;
                            case CalculatorViewModelTexts.InvTanTextValue:
                                ansValue = Math.Atan(double.Parse(firstOperator) / functionMultiplier);
                                break;
                            case CalculatorViewModelTexts.SqrtTextValue:
                                ansValue = Math.Sqrt(double.Parse(firstOperator));
                                break;
                            case CalculatorViewModelTexts.LnTextValue:
                                ansValue = Math.Log(double.Parse(firstOperator));
                                break;
                            case CalculatorViewModelTexts.LogTextValue:
                                ansValue = Math.Log10(double.Parse(firstOperator));
                                break;
                            case CalculatorViewModelTexts.RootTextValue:
                                secondOperator = (string)prefixStack.Pop();
                                ansValue = Math.Pow(double.Parse(firstOperator), (1 / double.Parse(secondOperator)));
                                break;
                            case CalculatorViewModelTexts.FactTextValue:
                                double number = double.Parse(firstOperator);
                                ansValue = Factorial(number);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException($@"Switching Type is not exists this method: {nameof(Calculate)}!");
                        }

                        prefixStack.Push(ansValue.ToString(CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        if (prefixStack.Count != 0)
                        {
                            firstOperator = (string)prefixStack.Pop();
                        }

                        if (prefixStack.Count != 0)
                        {
                            secondOperator = (string)prefixStack.Pop();
                        }

                        switch (currentPostfixQueueItem)
                        {
                            case CalculatorViewModelTexts.AddOperatorTextValue:
                                ansValue = double.Parse(firstOperator) + double.Parse(secondOperator);
                                break;
                            case CalculatorViewModelTexts.SubOperatorTextValue:
                                ansValue = double.Parse(secondOperator) - double.Parse(firstOperator);
                                break;
                            case CalculatorViewModelTexts.MulOperatorTextValue:
                                ansValue = double.Parse(firstOperator) * double.Parse(secondOperator);
                                break;
                            case CalculatorViewModelTexts.DivOperatorTextValue:
                                if (firstOperator.Equals("0"))
                                {
                                    throw new Exception(CalculatorViewModelTexts.MaErrorTextValue);
                                }
                                else
                                {
                                    ansValue = double.Parse(secondOperator) / double.Parse(firstOperator);
                                }
                                break;
                            case "^":
                                if (double.Parse(firstOperator) <= 0 && secondOperator.Equals("0"))
                                {
                                    throw new Exception(CalculatorViewModelTexts.MaErrorTextValue);
                                }
                                else
                                {
                                    ansValue = Math.Pow(double.Parse(secondOperator), double.Parse(firstOperator));
                                }
                                break;
                            case "%":
                                ansValue = double.Parse(firstOperator) / 100;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException($@"Switching Type is not exists this method: {nameof(Calculate)}!");
                        }

                        prefixStack.Push(ansValue.ToString(CultureInfo.CurrentCulture));
                    }
                }

                if (prefixStack.Count == 0)
                {
                    throw new FormatException(CalculatorViewModelTexts.SyntaxErrorTextValue);
                }

                return (string)prefixStack.Pop();
            }
            catch (FormatException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region PRIVATE Helper Methods

        private static double Factorial(double number)
        {
            if ((int)number == 1)
            {
                return 1;
            }

            return number * Factorial(number - 1);
        }

        protected bool PostfixQueueItemIsNumeric(string postfixQueueItem) =>
            double.TryParse(postfixQueueItem, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, null, out _);

        #endregion
    }
}
