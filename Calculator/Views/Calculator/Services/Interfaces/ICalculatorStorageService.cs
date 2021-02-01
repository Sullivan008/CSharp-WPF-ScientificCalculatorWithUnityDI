using System.Collections.Generic;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.Services.Interfaces
{
    public interface ICalculatorStorageService
    {
        Queue<string> Queue { get; }

        double Ans { get; }

        int LeftParenthesisNumber { get; }

        UserInteractionType LastUserInteractionType { get; }

        void SetLastUserInteractionType(UserInteractionType userInteractionType);

        void AddValueToQueue(string value);

        void AddOperatorToStack(string inputOperatorText, OperatorType inputOperatorType);

        void AddLeftParenthesisOperatorToStack(string inputOperatorText);

        void AddRightParenthesisOperatorToStack(string inputOperatorText);

        void AddFunctionToStack(string inputFunctionText);

        void AddAllItemsInTheStackToTheQueue();

        void SetAns(double ans);

        void ClearQueue();

        void ClearStack();

        void ClearLeftParenthesisNumber();
    }
}
