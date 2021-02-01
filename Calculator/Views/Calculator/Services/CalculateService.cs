using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Calculator.Core.Extensions;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.Exceptions;
using Calculator.Views.Calculator.Services.Helpers.CalculateService;
using Calculator.Views.Calculator.Services.Interfaces;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.Services
{
    public class CalculateService : ICalculateService
    {
        public double Calculate(Queue<string> queue, AngleUnitType angleUnitType)
        {
            Stack stack = new Stack();

            while (queue.Any())
            {
                string item = queue.Dequeue();

                if (string.IsNullOrWhiteSpace(item))
                {
                    throw new NotAllowedElementInQueueException($"The following element does not allowed in the queue! Element value: {item}");
                }

                if (item.IsDecimalNumber())
                {
                    stack.Push(item);
                }
                else if (Functions.IsTrigonometricFunction(item))
                {
                    TrigonometricFunctionType trigonometricFunctionType = Functions.GetTrigonometricFunctionTypeByText(item);

                    CalculateWithTrigonometricFunction(stack, trigonometricFunctionType, angleUnitType);
                }
                else if (Functions.IsFunction(item))
                {
                    FunctionType functionType = Functions.GetFunctionTypeByText(item);

                    CalculateWithFunction(stack, functionType);
                }
                else if (Operators.IsOperator(item))
                {
                    OperatorType operatorType = Operators.GetOperatorTypeByText(item);

                    CalculateWithOperator(stack, operatorType);
                }
                else
                {
                    throw new UnknownElementInQueueException($"The following element does not allowed in the queue! Element value: {item}");
                }
            }

            if (stack.Count != 1)
            {
                throw new SyntaxErrorException("The stack contains more items than it should!");
            }

            return double.Parse(Convert.ToString(stack.Pop()));
        }

        private static void CalculateWithTrigonometricFunction(Stack stack, TrigonometricFunctionType trigonometricFunctionType, AngleUnitType angleUnitType)
        {
            double operand;

            switch (trigonometricFunctionType)
            {
                case TrigonometricFunctionType.Sin:
                    operand = double.Parse(Convert.ToString(stack.Pop()));

                    switch (angleUnitType)
                    {
                        case AngleUnitType.Radian:
                            stack.Push(Math.Sin(operand) * 1);
                            break;
                        case AngleUnitType.Degree:
                            stack.Push(Math.Sin(operand * (Math.PI / 180)));
                            break;
                        default:
                            throw new InvalidEnumArgumentException(nameof(angleUnitType), (int)angleUnitType, typeof(AngleUnitType));
                    }

                    break;
                case TrigonometricFunctionType.ArcSin:
                    operand = double.Parse(Convert.ToString(stack.Pop()));

                    switch (angleUnitType)
                    {
                        case AngleUnitType.Radian:
                            stack.Push(Math.Asin(operand) * 1);
                            break;
                        case AngleUnitType.Degree:
                            stack.Push(Math.Asin(operand) * 180 / Math.PI);
                            break;
                        default:
                            throw new InvalidEnumArgumentException(nameof(angleUnitType), (int)angleUnitType, typeof(AngleUnitType));
                    }

                    break;
                case TrigonometricFunctionType.Cos:
                    operand = double.Parse(Convert.ToString(stack.Pop()));

                    switch (angleUnitType)
                    {
                        case AngleUnitType.Radian:
                            stack.Push(Math.Cos(operand) * 1);
                            break;
                        case AngleUnitType.Degree:
                            stack.Push(Math.Cos(operand * (Math.PI / 180)));
                            break;
                        default:
                            throw new InvalidEnumArgumentException(nameof(angleUnitType), (int)angleUnitType, typeof(AngleUnitType));
                    }

                    break;
                case TrigonometricFunctionType.ArcCos:
                    operand = double.Parse(Convert.ToString(stack.Pop()));

                    switch (angleUnitType)
                    {
                        case AngleUnitType.Radian:
                            stack.Push(Math.Acos(operand) * 1);
                            break;
                        case AngleUnitType.Degree:
                            stack.Push(Math.Acos(operand) * 180 / Math.PI);
                            break;
                        default:
                            throw new InvalidEnumArgumentException(nameof(angleUnitType), (int)angleUnitType, typeof(AngleUnitType));
                    }

                    break;
                case TrigonometricFunctionType.Tan:
                    operand = double.Parse(Convert.ToString(stack.Pop()));

                    switch (angleUnitType)
                    {
                        case AngleUnitType.Radian:
                            stack.Push(Math.Tan(operand) * 1);
                            break;
                        case AngleUnitType.Degree:
                            stack.Push(Math.Tan(operand * (Math.PI / 180)));
                            break;
                        default:
                            throw new InvalidEnumArgumentException(nameof(angleUnitType), (int)angleUnitType, typeof(AngleUnitType));
                    }

                    break;
                case TrigonometricFunctionType.ArcTan:
                    operand = double.Parse(Convert.ToString(stack.Pop()));

                    switch (angleUnitType)
                    {
                        case AngleUnitType.Radian:
                            stack.Push(Math.Atan(operand) * 1);
                            break;
                        case AngleUnitType.Degree:
                            stack.Push(Math.Atan(operand) * 180 / Math.PI);
                            break;
                        default:
                            throw new InvalidEnumArgumentException(nameof(angleUnitType), (int)angleUnitType, typeof(AngleUnitType));
                    }

                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(trigonometricFunctionType), (int)trigonometricFunctionType, typeof(TrigonometricFunctionType));
            }
        }

        private static void CalculateWithFunction(Stack stack, FunctionType functionType)
        {
            double firstOperand;
            double secondOperand;

            switch (functionType)
            {
                case FunctionType.Ln:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    stack.Push(Math.Log(firstOperand));

                    break;
                case FunctionType.Log:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    stack.Push(Math.Log10(firstOperand));

                    break;
                case FunctionType.Sqrt:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    stack.Push(Math.Sqrt(firstOperand));

                    break;
                case FunctionType.YRoot:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    secondOperand = double.Parse(Convert.ToString(stack.Pop()));

                    stack.Push(Math.Pow(firstOperand, 1 / secondOperand));
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(functionType), (int)functionType, typeof(FunctionType));
            }
        }

        private static void CalculateWithOperator(Stack stack, OperatorType operatorType)
        {
            double firstOperand;
            double secondOperand;

            switch (operatorType)
            {
                case OperatorType.Addition:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    secondOperand = double.Parse(Convert.ToString(stack.Pop()));

                    stack.Push(firstOperand + secondOperand);

                    break;
                case OperatorType.Subtraction:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    secondOperand = double.Parse(Convert.ToString(stack.Pop()));

                    stack.Push(firstOperand - secondOperand);

                    break;
                case OperatorType.Multiplication:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    secondOperand = double.Parse(Convert.ToString(stack.Pop()));

                    stack.Push(firstOperand * secondOperand);

                    break;
                case OperatorType.Division:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    secondOperand = double.Parse(Convert.ToString(stack.Pop()));

                    if (firstOperand == 0)
                    {
                        throw new DivideByZeroException();
                    }

                    stack.Push(secondOperand / firstOperand);

                    break;
                case OperatorType.Modulus:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    stack.Push(firstOperand / 100);

                    break;
                case OperatorType.Pow:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    secondOperand = double.Parse(Convert.ToString(stack.Pop()));

                    if (secondOperand == 0 && firstOperand < 0)
                    {
                        throw new NotFiniteNumberException(secondOperand);
                    }

                    stack.Push(Math.Pow(secondOperand, firstOperand));

                    break;
                case OperatorType.Factorial:
                    firstOperand = double.Parse(Convert.ToString(stack.Pop()));
                    stack.Push(FactorialHelper.CalculateFactorial(firstOperand));

                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(operatorType), (int)operatorType, typeof(OperatorType));
            }
        }
    }
}
