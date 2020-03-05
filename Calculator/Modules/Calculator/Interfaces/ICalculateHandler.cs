using System.Collections;
using System.Collections.Generic;

namespace Calculator.Modules.Calculator.Interfaces
{
    public interface ICalculateHandler
    {
        /// <summary>
        ///     Kalkulálás. A felhasználó által megadott összes művelet végrehajtása.
        /// </summary>
        /// <returns>
        ///     NUMBER - Ha minden művelet végrehajtható, akkor a kalkulálás eredménye.
        ///     SYNTAX ERROR - Hibás bevitel esetén.
        ///     MA ERROR - Helytelen művelet végrehajtása során.
        /// </returns>
        string Calculate(Stack operatorStack, Queue<string> postfixQueue, 
            double functionMultiplier, ref double ansValue);
    }
}
