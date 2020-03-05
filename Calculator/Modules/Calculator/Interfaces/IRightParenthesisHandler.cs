using System.Collections;
using System.Collections.Generic;

namespace Calculator.Modules.Calculator.Interfaces
{
    public interface IRightParenthesisHandler
    {
        /// <summary>
        ///     Az OPERATOR STACK-ből kiolvasva az elemeket addig jarjuk be, amíg nem találjuk meg a JOBB zárójelhez tartozó
        ///     BAL zárójel párját. Ez a metódus nem veszi figyelembe, azt, hogy függvényt hajtottunk-e végre a sázmológépen.
        /// </summary>
        void AddItemsPostfixQueueRightParOperationWithoutFunction(Stack operatorStack,
            Queue<string> postfixQueue, ref int leftParenthesisNumber);

        /// <summary>
        ///     Az OPERATOR STACK-ből kiolvasva az elemeket addig jarjuk be, amíg nem találjuk meg a JOBB zárójelhez tartozó
        ///     BAL zárójel párját. Ez a metódus nem veszi figyelembe veszi, hogy függvényt hajtottunk e végre a számológépen.
        /// </summary>
        void AddItemsPostfixQueueRightParOperationWithFunction(Stack operatorStack, Queue<string> postfixQueue,
            ref int functionsNumber, ref int leftParenthesisNumber);
    }
}
