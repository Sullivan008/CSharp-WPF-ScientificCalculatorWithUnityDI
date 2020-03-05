using Calculator.Constants;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.Modules.Calculator
{
    public abstract class CommonCalculationHandler
    {
        protected void AddValueForPostfixQueue(Queue<string> postfixQueue, string value)
        {
            postfixQueue.Enqueue(value);
        }

        protected static bool PostfixQueueItemIsAlphanumeric(string postfixQueueItem) =>
            Regex.Match(postfixQueueItem, @"([A-Za-z]+)").Success;


        protected void AddOtherOperatorsOperatorStack(Stack operatorStack, ref int leftParenthesisNumber, ref int functionsNumber, string operatorValue)
        {
            operatorStack.Push(operatorValue);

            if (NeedToIncTheNumberOfLeftPar(operatorValue))
            {
                leftParenthesisNumber++;
            }

            if (NeedToIncNumberOfFunctionNumber(operatorValue))
            {
                functionsNumber++;
            }
        }

        #region PRIVATE Helper Methods

        private static bool NeedToIncTheNumberOfLeftPar(string operatorValue) => operatorValue.Equals(CalculatorViewModelTexts.LeftParenthesisTextValue);

        private static bool NeedToIncNumberOfFunctionNumber(string operatorValue) => operatorValue.Equals(CalculatorViewModelTexts.SinTextValue) || 
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.InvSinTextValue) ||
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.CosTextValue) || 
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.InvCosTextValue) ||
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.InvTanTextValue) ||
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.InvTanTextValue) ||
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.SquareRootTextValue) ||
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.LnTextValue) ||
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.LogTextValue) ||
                                                                                     operatorValue.Equals(CalculatorViewModelTexts.RootTextValue);
        #endregion
    }
}
