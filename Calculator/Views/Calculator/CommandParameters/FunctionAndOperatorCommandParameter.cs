using Calculator.Core.CommandParameters.Interfaces;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.CommandParameters
{
    public class FunctionAndOperatorCommandParameter : ICommandParameter
    {
        public UserInteractionType FunctionUserInteractionType { get; }

        public UserInteractionType OperatorUserInteractionType { get; }

        public FunctionType FunctionType { get; }

        public OperatorType OperatorType { get; }

        public string FunctionText { get; }

        public string OperatorText { get; }

        public FunctionAndOperatorCommandParameter(UserInteractionType functionUserInteractionType, UserInteractionType operatorUserInteractionType, FunctionType functionType, OperatorType operatorType)
        {
            FunctionUserInteractionType = functionUserInteractionType;
            OperatorUserInteractionType = operatorUserInteractionType;

            FunctionType = functionType;
            OperatorType = operatorType;

            FunctionText = Functions.GetFunctionText(functionType);
            OperatorText = Operators.GetOperatorText(operatorType);
        }
    }
}
