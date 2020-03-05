using Calculator.Constants;
using System.Collections;
using System.Collections.Generic;
using Calculator.Modules.Calculator.Interfaces;

namespace Calculator.Modules.Calculator.Handlers
{
    public class RightParenthesisHandler : CommonCalculationHandler, IRightParenthesisHandler
    {
        public void AddItemsPostfixQueueRightParOperationWithoutFunction(Stack operatorStack,
            Queue<string> postfixQueue, ref int leftParenthesisNumber)
        {
            while (leftParenthesisNumber > 0)
            {
                string operatorStackTopValue = (string)operatorStack.Pop();

                if (!operatorStackTopValue.Equals(CalculatorViewModelTexts.LeftParenthesisTextValue))
                {
                    postfixQueue.Enqueue(operatorStackTopValue);
                }
                else
                {
                    leftParenthesisNumber--;

                    break;
                }
            }
        }

        public void AddItemsPostfixQueueRightParOperationWithFunction(Stack operatorStack, Queue<string> postfixQueue,
            ref int functionsNumber, ref int leftParenthesisNumber)
        {
            while (functionsNumber > 0)
            {
                string operatorStackTopValue = (string)operatorStack.Pop();

                if (!operatorStackTopValue.Equals(CalculatorViewModelTexts.LeftParenthesisTextValue))
                {
                    postfixQueue.Enqueue(operatorStackTopValue);

                    if (PostfixQueueItemIsAlphanumeric(operatorStackTopValue))
                    {
                        functionsNumber--;
                    }
                }
                else
                {
                    leftParenthesisNumber--;

                    if (leftParenthesisNumber != 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}
