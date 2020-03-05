using System.Collections;
using System.Collections.Generic;

namespace Calculator.Modules.Calculator.Interfaces
{
    public interface IBasicArithmeticOperatorHandler
    {
        /// <summary>
        ///     Az alapvető aritmetikai jelet, hozzáadjuk az operátorokat tartalmazó veremhez.
        /// </summary>
        void AddBasicArithmeticOperatorOperatorStack(Stack operatorStack, Queue<string> postfixQueue, string arithmeticOperator);
    }
}
