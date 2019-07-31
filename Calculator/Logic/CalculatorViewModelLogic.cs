using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calculator.Logic
{
    public class CalculatorViewModelLogic
    {
        /// <summary>
        ///     A felhasználó által INPUT-ban megadott SZÁMOKAT és OPERÁTOR-okat tartalmazó SOR.
        /// </summary>
        private Queue<string> postfixQueue = new Queue<string>();

        /// <summary>
        ///     A számítás során használt PREFIX VEREM
        /// </summary>
        private Stack prefixStack = new Stack();

        /// <summary>
        ///     A felhasználó által bevitt OPERÁTOROKAT tartalmazó VEREM.
        /// </summary>
        private Stack operatorStack = new Stack();

        /// <summary>
        ///     A legutolsó számítási eredményt tartalmazó Változó. Ennek tárolásával az ANS funkciót tudjuk megvalósítani.
        /// </summary>
        public double AnsValue { get; set; } = 0;

        /// <summary>
        ///     Tároljuk, hogy az utolsó végrehajtási művelet egy SZÁMÍTÁS volt-e.
        /// </summary>
        public bool LastOperationIsEqual { get; set; } = false;

        /// <summary>
        ///     Tároljuk a Függvényszorzót (RAD, DEG)
        /// </summary>
        public double FunctionMultipler { get; set; } = 1;

        /// <summary>
        ///     Tároljuk a BAL Zárójelek számát.
        /// </summary>
        public int LeftParNumber { get; set; } = 0;

        /// <summary>
        ///     Tároljuk a függvények számát.
        /// </summary>
        public int FuntionsNumber { get; set; } = 0;

        #region PUBLIC Methods
        /// <summary>
        ///     Az INPUT mezőben megadott számot, eltároljuk a POSTFIX SOR-ban.
        /// </summary>
        /// <param name="value">Az INPUT mezőben megadott szám.</param>
        public void AddValueForPostfixQueue(string value)
        {
            postfixQueue.Enqueue(value);
        }

        /// <summary>
        ///     Az alapvető aritmetikai jelet, hozzáadjuk az operátorokat tartalmazó veremhez.
        /// </summary>
        /// <param name="operatorValue">A ViewModel-től kapott aritmetikai művelet.</param>
        public void AddBasicArithmeticOperatorOperatorStack(string operatorValue)
        {
            string operatorStackTopValue;

            while (operatorStack.Count != 0)
            {
                /// Kiszedjük az OPERÁTOROKAT tartalmazó VEREM teteljén lévő elemet, anélkül hogy törölnénk a VEREM-ből.
                operatorStackTopValue = (string)operatorStack.Peek();

                if (operatorValue.Equals("+") || operatorValue.Equals("-"))
                {
                    /// Megvizsgáljuk, hogy az operátor megfelel-e az összeadás és kivonás végrehajtási sorrend betartásához megadott
                    /// Operátorok valamelyikével. Ha igen, akkor hozzáadjuk a POSTFIX SOR-hoz.
                    if (AcceptedOperatorsForAddAndSubFromStackTop(operatorStackTopValue))
                    {
                        postfixQueue.Enqueue((string)operatorStack.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
                else if (operatorValue.Equals("*") || operatorValue.Equals("/"))
                {
                    /// Megvizsgáljuk, hogy az operátor megfelel-e a szorzás és osztás végrehajtási sorrend betartásához megadott
                    /// Operátorok valamelyikével. Ha igen, akkor hozzáadjuk a POSTFIX SOR-hoz.
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

            operatorStack.Push(operatorValue);
        }

        /// <summary>
        ///     Az OPERATOR STACK-ből kiolvasva az elemeket addig jarjuk be, amíg nem találjuk meg a JOBB zárójelhez tartozó
        ///     BAL zárójel párját. Ez a metódus nem veszi figyelembe, azt, hogy függvényt hajtottunk-e végre a sázmológépen.
        /// </summary>
        public void AddItemsPostfixQueueRightParOperationWithoutFunction()
        {
            string operatorStackTopValue;

            while (LeftParNumber > 0)
            {
                /// Kiszedjük az OPERÁTOROKAT tartalmazó VEREM teteljén lévő elemet.
                operatorStackTopValue = (string)operatorStack.Pop();

                if(!operatorStackTopValue.Equals("("))
                {
                    postfixQueue.Enqueue(operatorStackTopValue);
                }
                else
                {
                    LeftParNumber--;

                    break;
                }
            }
        }

        /// <summary>
        ///     Az OPERATOR STACK-ből kiolvasva az elemeket addig jarjuk be, amíg nem találjuk meg a JOBB zárójelhez tartozó
        ///     BAL zárójel párját. Ez a metódus nem veszi figyelembe veszi, hogy függvényt hajtottunk e végre a számológépen.
        /// </summary>
        public void AddItemsPostfixQueueRightParOperationWithFunction()
        {
            string operatorStackTopValue;

            while (FuntionsNumber > 0)
            {
                operatorStackTopValue = (string)operatorStack.Pop();

                if (!operatorStackTopValue.Equals("("))
                {
                    postfixQueue.Enqueue(operatorStackTopValue);

                    if (IsAlphanumeric(operatorStackTopValue))
                    {
                        FuntionsNumber--;
                    }
                }
                else
                {
                    LeftParNumber--;

                    if(LeftParNumber != 0)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     A paraméterben átadott operátorokat (Zárójel, Sin, Cos, stb..) hozzá adja az OPERÁTOR VEREM-hez.
        ///     Ha az operátor megköveteli, hogy zárójel tartozik hozzá (pl.: ( zárójel) akkor növeljük a BAL zárójelek számát.
        ///     Ha az operátor megköveteli, hogy függvény-t adtunk-e hozzá, akkor növeljük a FÜGGVÉNYEK számát.
        /// </summary>
        /// <param name="operatorValue">A ViewModel-től kapott aritmetikai művelet.</param>
        public void AddOtherOperatorsOperatorStack(string operatorValue)
        {
            operatorStack.Push(operatorValue);

            if(ShouldIncNumberOfLeftPar(operatorValue))
            {
                LeftParNumber++;
            }

            if(ShuldIncNumberOfFunctionNumber(operatorValue))
            {
                FuntionsNumber++;
            }
        }

        /// <summary>
        ///     Kalkulálás. A felhasználó által megadott összes művelet végrehajtása.
        /// </summary>
        /// <returns>
        ///     NUMBER - Ha minden művelet végrehajtható, akkor a kalkulálás eredménye.
        ///     SYNTAXT ERROR - Hibás bevitel esetén.
        ///     MA ERROR - Helytelen művelet végrehajtása során.
        /// </returns>
        public string Calculate()
        {
            string currentPostfixQueueItem;

            string firstOperator = string.Empty;
            string secondOperator = string.Empty;

            /// Az OPERÁTOR-okat tartalmazó VERM-et bejárva, kiszedjük az összes OPERÁTOR-t belőle
            /// és hozzáadjuk a POSTFIX SOR-hoz, ezeket az OPERÁTOROKAT.
            while (operatorStack.Count != 0)
            {
                postfixQueue.Enqueue((string)operatorStack.Pop());
            }

            try
            {
                /// A felhasználó által megadott összes művelet végrehajtása a POSTFIX SOR-t bejárva
                /// végrehajtjuk.
                while (postfixQueue.Count != 0)
                {
                    /// A POSTFIX SOR (FIFO) első elemét kiolvassuk.
                    currentPostfixQueueItem = postfixQueue.Dequeue();

                    /// Megvizsgáljuk, hogy a kiolvasott elem, az szám-e. Ha igen, akkor hozzáadjuk a 
                    ///     PREFIX VEREM-hez.
                    /// Ha a kiolvasott elem ALPHANUMERIKUS karakter, akkor elvégzünk egy függvény műveletet, a hozzá tartozó
                    ///     kiolvasott OPERANDUSSAl.
                    /// Ha a kiolvasott elem egy OPERÁTOR akkor a kiolvasott OPERANDUS-okkal elvégezzük a műveletet.
                    if (IsNumeric(currentPostfixQueueItem))
                    {
                        prefixStack.Push(currentPostfixQueueItem);
                    }
                    else if(IsAlphanumeric(currentPostfixQueueItem))
                    {
                        firstOperator = (string)prefixStack.Pop();

                        switch(currentPostfixQueueItem)
                        {
                            case "sin":
                                AnsValue = Math.Sin(double.Parse(firstOperator) * FunctionMultipler);
                                break;
                            case "InvSin":
                                AnsValue = Math.Asin(double.Parse(firstOperator) / FunctionMultipler);
                                break;
                            case "cos":
                                AnsValue = Math.Cos(double.Parse(firstOperator) * FunctionMultipler);
                                break;
                            case "InvCos":
                                AnsValue = Math.Acos(double.Parse(firstOperator) / FunctionMultipler);
                                break;
                            case "tan":
                                AnsValue = Math.Tan(double.Parse(firstOperator) * FunctionMultipler);
                                break;
                            case "InvTan":
                                AnsValue = Math.Atan(double.Parse(firstOperator) / FunctionMultipler);
                                break;
                            case "sqrt":
                                AnsValue = Math.Sqrt(double.Parse(firstOperator));
                                break;
                            case "ln":
                                AnsValue = Math.Log(double.Parse(firstOperator));
                                break;
                            case "log":
                                AnsValue = Math.Log10(double.Parse(firstOperator));
                                break;
                            case "root":
                                secondOperator = (String)prefixStack.Pop();
                                AnsValue = Math.Pow(double.Parse(firstOperator), (1 / double.Parse(secondOperator)));
                                break;
                            case "fact":
                                double number = double.Parse(firstOperator);
                                AnsValue = Factorial(number);
                                break;
                            default:
                                break;
                        }

                        /// A két operandus által adott eredményt hozzáadjuk a PREFIX VEREM-hez. Ez az eredmény
                        /// lesz az egyik következő operandusunk, ha a POSTFIX SOR tartalmaz még egy Számot és Operátort legalább.
                        prefixStack.Push(AnsValue.ToString());
                    }
                    else
                    {
                        /// Kiolvassuk a PREFIX VEREM (LIFO) első elemét. Ez lesz az első operandusunk.
                        if (prefixStack.Count != 0)
                        {
                            firstOperator = (string)prefixStack.Pop();
                        }

                        /// Kiolvassuk a PREFIX VEREM (LIFO) első elemét. Ez lesz a második operandusunk.
                        if (prefixStack.Count != 0)
                        {
                            secondOperator = (string)prefixStack.Pop();
                        }

                        /// Ha az aktuális POSTFIX SOR eleme egy operátor, akkor, az operátornak megfelelő
                        /// műveletet végrehajtjuk a korábban kiolvasott két operandus segítségével.
                        switch (currentPostfixQueueItem)
                        {
                            case "+":
                                AnsValue = double.Parse(firstOperator) + double.Parse(secondOperator);
                                break;
                            case "-":
                                AnsValue = double.Parse(secondOperator) - double.Parse(firstOperator);
                                break;
                            case "*":
                                AnsValue = double.Parse(firstOperator) * double.Parse(secondOperator);
                                break;
                            case "/":
                                if (firstOperator.Equals("0"))
                                {
                                    throw new Exception("Ma ERROR");
                                }
                                else
                                {
                                    AnsValue = double.Parse(secondOperator) / double.Parse(firstOperator);
                                }
                                break;
                            case "^":
                                if(double.Parse(firstOperator) <= 0 && secondOperator.Equals("0"))
                                {
                                    throw new Exception("Ma ERROR");
                                }
                                else
                                {
                                    AnsValue = Math.Pow(double.Parse(secondOperator), double.Parse(firstOperator));
                                }
                                break;
                            case "%":
                                AnsValue = double.Parse(firstOperator) / 100;
                                break;
                            default:
                                break;
                        }

                        /// A két operandus által adott eredményt hozzáadjuk a PREFIX VEREM-hez. Ez az eredmény
                        /// lesz az egyik következő operandusunk, ha a POSTFIX SOR tartalmaz még egy Számot és Operátort legalább.
                        prefixStack.Push(AnsValue.ToString());
                    }
                }

                /// Ha a PREFIX VEREM nem tartalmaz elemet, akkor hibás volt a bevitel, különben
                /// a PREFIX VEREM utolsó eleme lesz a számítási eredmény értéke.
                if (prefixStack.Count == 0)
                {
                    throw new FormatException("Syntaxt ERROR");
                }
                else
                {
                    return (string)prefixStack.Pop();
                }
            }
            catch (FormatException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                ClearBasicData();

                /// Eltároljuk, hogy az utolsó műveleti jel, egy egyenlőség jel volt, annak érdekében,
                /// hogy addig ne tudjunk újabb számítást végrehajtani, amíg nem végeztünk valamilyen további műveletet.
                LastOperationIsEqual = true;
            }
        }

        /// <summary>
        ///     A kalkulációhoz használt, szükséges adatokat, alapértelmezettre való állítása.
        /// </summary>
        public void ClearBasicData()
        {
            /// SOR és VEREM ürítése
            postfixQueue.Clear();
            operatorStack.Clear();
            prefixStack.Clear();

            /// Zárójelek számának nullázása.
            LeftParNumber = 0;
        }

        /// <summary>
        ///     Beállítja a függvényszorzót, a DEGREE módnak megfelelően.
        /// </summary>
        public void SetFunctionMultiplerValueToDegree()
        {
            FunctionMultipler = Math.PI / 180;
        }

        /// <summary>
        ///     Beállítja a függvényszorzót, a RADIAN módnak megfelelően.
        /// </summary>
        public void SetFunctionMultiplerValueToRadian()
        {
            FunctionMultipler = 1;
        }
        #endregion

        #region PRIVATE HELPER Methods
        /// <summary>
        ///     Függvény, amely megvizsgálja, hogy a paraméterben kapott OPERÁTOR megfelel-e az Összeadás és Kivonás
        ///     végrehajtási sorrend betartásához meghatározott Operátorok valamelyikével.
        /// </summary>
        /// <param name="operatorStackTopValue">A vizsgálandó operátor.</param>
        /// <returns>
        ///     TRUE  - Ha megfelel a végrehajtási sorrendben meghatározott Operátorok valamelyikével.
        ///     FALSE - Ha nem.
        /// </returns>
        private bool AcceptedOperatorsForAddAndSubFromStackTop(string operatorStackTopValue) =>
            operatorStackTopValue.Equals("+") || operatorStackTopValue.Equals("-") ||
            operatorStackTopValue.Equals("*") || operatorStackTopValue.Equals("/") ||
            operatorStackTopValue.Equals("^") || operatorStackTopValue.Equals("%") ||
            operatorStackTopValue.Equals("fact");

        /// <summary>
        ///     Függvény, amely megvizsgálja, hogy a paraméterben kapott OPERÁTOR megfelel-e a Szorzás és Osztás
        ///     végrehajtási sorrend betartásához meghatározott Operátorok valamelyikével.
        /// </summary>
        /// <param name="operatorStackTopValue">A vizsgálandó operátor.</param>
        /// <returns>
        ///     TRUE  - Ha megfelel a végrehajtási sorrendben meghatározott Operátorok valamelyikével.
        ///     FALSE - Ha nem.
        /// </returns>
        private bool AcceptedOperatorsForMulAndDivFromStackTop(string operatorStackTopValue) =>
            operatorStackTopValue.Equals("*") || operatorStackTopValue.Equals("/") ||
            operatorStackTopValue.Equals("^") || operatorStackTopValue.Equals("fact");

        /// <summary>
        ///     Függvény, amely meghatározza, hogy a paraméterben kapott string, az Numerikus
        ///     karaktersorozat-e.
        /// </summary>
        /// <param name="postfixQueueItem">A vizsgálandó String</param>
        /// <returns>
        ///     TRUE  - Ha Numerikus karaktersorozat.
        ///     FALSE - Ha nem.
        /// </returns>
        private bool IsNumeric(string postfixQueueItem)
        {
            try
            {
                double.Parse(postfixQueueItem, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Függvény, amely meghatározza, hogy a paraméterben kapott string, az Alphanumerikus
        ///     karaktersorozat-e.
        /// </summary>
        /// <param name="postfixQueueItem">A vizsgálandó String</param>
        /// <returns>
        ///     TRUE  - Ha Numerikus karaktersorozat.
        ///     FALSE - Ha nem.
        /// </returns>
        private bool IsAlphanumeric(string postfixQueueItem) => Regex.Match(postfixQueueItem, @"([A-Za-z]+)").Success;

        /// <summary>
        ///     A paraméterben kapott operátor alapján eldöntésre kerül, hogy kell-e növelni a bal zárójelek számát, vagy sem,
        ///     a függvényben meghatározott operátorok alapján.
        /// </summary>
        /// <param name="operatorValue">A vizsgálandó operátor.</param>
        /// <returns>
        ///     TRUE - Ha szükséges növelni.
        ///     FALSE - Ha nem szükséges növelni.
        /// </returns>
        private bool ShouldIncNumberOfLeftPar(string operatorValue) => operatorValue.Equals("(");

        /// <summary>
        ///     A paraméterben kapott operátor alapján eldöntésre kerül, hogy kell-e növelni a végrehajtott függvények számát, vagy sem,
        ///     a függvényben meghatározott operátorok alapján.
        /// </summary>
        /// <param name="operatorValue">A vizsgálandó operátor.</param>
        /// <returns>
        ///     TRUE - Ha szükséges növelni.
        ///     FALSE - Ha nem szükséges növelni.
        /// </returns>
        private bool ShuldIncNumberOfFunctionNumber(string operatorValue) => operatorValue.Equals("sin") ||
            operatorValue.Equals("InvSin") || operatorValue.Equals("cos") || operatorValue.Equals("InvCos") ||
            operatorValue.Equals("tan") || operatorValue.Equals("InvTan") || operatorValue.Equals("√") || 
            operatorValue.Equals("ln") || operatorValue.Equals("log") || operatorValue.Equals("root");

        /// <summary>
        ///     Rekurzív faktoriális számítás
        /// </summary>
        /// <param name="number">A faktoriálandó szám.</param>
        /// <returns>A paraméterben átadott szám faktoriálisa.</returns>
        private double Factorial(double number)
        {
            if (number == 1)
            {
                return 1;
            }
            else
            {
                return number * Factorial(number - 1);
            }
        }
        #endregion
    }
}
