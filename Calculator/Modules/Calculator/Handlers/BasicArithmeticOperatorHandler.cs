using Calculator.Constants;
using System.Collections;
using System.Collections.Generic;
using Calculator.Modules.Calculator.Interfaces;

namespace Calculator.Modules.Calculator.Handlers
{
    public class BasicArithmeticOperatorHandler : IBasicArithmeticOperatorHandler
    {
        public void AddBasicArithmeticOperatorOperatorStack(Stack operatorStack, Queue<string> postfixQueue, string arithmeticOperator)
        {
            while (operatorStack.Count != 0)
            {
                string operatorStackTopValue = (string)operatorStack.Peek();

                if (arithmeticOperator.Equals(CalculatorViewModelTexts.AddOperatorTextValue) || arithmeticOperator.Equals(CalculatorViewModelTexts.SubOperatorTextValue))
                {
                    if (AcceptedOperatorsForAddAndSubFromStackTop(operatorStackTopValue))
                    {
                        postfixQueue.Enqueue((string)operatorStack.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
                else if (arithmeticOperator.Equals(CalculatorViewModelTexts.MulOperatorTextValue) || arithmeticOperator.Equals(CalculatorViewModelTexts.DivOperatorTextValue))
                {
                    if (AcceptedOperatorsForMulAndDivFromStackTop(operatorStackTopValue))
                    {
                        postfixQueue.Enqueue((string)operatorStack.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
            }

            operatorStack.Push(arithmeticOperator);
        }

        #region PRIVATE Helper Methods

        private static bool AcceptedOperatorsForAddAndSubFromStackTop(string operatorStackTopValue) =>
            operatorStackTopValue.Equals(CalculatorViewModelTexts.AddOperatorTextValue) || operatorStackTopValue.Equals(CalculatorViewModelTexts.SubOperatorTextValue) ||
            operatorStackTopValue.Equals(CalculatorViewModelTexts.MulOperatorTextValue) || operatorStackTopValue.Equals(CalculatorViewModelTexts.DivOperatorTextValue) ||
            operatorStackTopValue.Equals(CalculatorViewModelTexts.SquareTextValue) || operatorStackTopValue.Equals(CalculatorViewModelTexts.PercentageTextValue) ||
            operatorStackTopValue.Equals(CalculatorViewModelTexts.FactTextValue);

        private static bool AcceptedOperatorsForMulAndDivFromStackTop(string operatorStackTopValue) =>
            operatorStackTopValue.Equals(CalculatorViewModelTexts.MulOperatorTextValue) || operatorStackTopValue.Equals(CalculatorViewModelTexts.DivOperatorTextValue) ||
            operatorStackTopValue.Equals(CalculatorViewModelTexts.SquareTextValue) || operatorStackTopValue.Equals(CalculatorViewModelTexts.FactTextValue);

        #endregion
    }
}
