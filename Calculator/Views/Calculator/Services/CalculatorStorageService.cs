using System;
using System.Collections;
using System.Collections.Generic;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.Exceptions;
using Calculator.Views.Calculator.Services.Interfaces;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.Services
{
    public class CalculatorStorageService : ICalculatorStorageService
    {
        private readonly Stack _stack;

        public Queue<string> Queue { get; }

        public double Ans { get; private set; }

        public int LeftParenthesisNumber { get; private set; }

        public UserInteractionType LastUserInteractionType { get; private set; }

        public CalculatorStorageService()
        {
            _stack = new Stack();
            Queue = new Queue<string>();

            Ans = 0;
            LeftParenthesisNumber = 0;

            LastUserInteractionType = UserInteractionType.Unknown;
        }

        public void SetLastUserInteractionType(UserInteractionType userInteractionType)
        {
            LastUserInteractionType = userInteractionType;
        }

        public void AddValueToQueue(string value)
        {
            Queue.Enqueue(value);
        }

        public void AddOperatorToStack(string inputOperatorText, OperatorType inputOperatorType)
        {
            if (inputOperatorType == OperatorType.LeftParenthesis || inputOperatorType == OperatorType.RightParenthesis || inputOperatorType == OperatorType.Equal)
            {
                throw new NotAllowedOperatorInMethodException($"the following operator is not allowed in the method. {inputOperatorType}");
            }

            if (string.IsNullOrWhiteSpace(inputOperatorText))
            {
                throw new ArgumentNullException(nameof(inputOperatorText), @"The value cannot be null!");
            }

            while (_stack.Count != 0)
            {
                string currentStackItem = Convert.ToString(_stack.Peek());

                if (Operators.IsOperator(currentStackItem) && !Operators.IsParenthesisOperator(currentStackItem) &&
                    Operators.GetOperatorPriority(inputOperatorType) <= Operators.GetOperatorPriority(Operators.GetOperatorTypeByText(currentStackItem)))
                {
                    Queue.Enqueue(Convert.ToString(_stack.Pop()));
                }
                else
                {
                    break;
                }
            }

            _stack.Push(inputOperatorText);
        }

        public void AddLeftParenthesisOperatorToStack(string inputOperatorText)
        {
            _stack.Push(inputOperatorText);

            LeftParenthesisNumber++;
        }

        public void AddRightParenthesisOperatorToStack(string inputOperatorText)
        {
            while (LeftParenthesisNumber > 0)
            {
                string currentStackItem = Convert.ToString(_stack.Pop());

                if (!Operators.IsLeftParenthesisOperator(currentStackItem))
                {
                    AddValueToQueue(currentStackItem);
                }
                else
                {
                    LeftParenthesisNumber--;

                    break;
                }
            }
        }

        public void AddFunctionToStack(string inputFunctionText)
        {
            _stack.Push(inputFunctionText);
        }

        public void AddAllItemsInTheStackToTheQueue()
        {
            while (_stack.Count != 0)
            {
                AddValueToQueue(Convert.ToString(_stack.Pop()));
            }
        }

        public void SetAns(double ans)
        {
            Ans = ans;
        }

        public void ClearQueue()
        {
            Queue.Clear();
        }

        public void ClearStack()
        {
            _stack.Clear();
        }

        public void ClearLeftParenthesisNumber()
        {
            LeftParenthesisNumber = default;
        }
    }
}
