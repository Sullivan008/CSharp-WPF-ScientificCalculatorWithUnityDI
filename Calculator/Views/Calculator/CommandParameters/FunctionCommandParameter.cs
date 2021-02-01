using Calculator.Core.CommandParameters.Interfaces;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.CommandParameters
{
    public class FunctionCommandParameter : ICommandParameter
    {
        public UserInteractionType UserInteractionType { get; }

        public FunctionType FunctionType { get; }

        public string FunctionText { get; }

        public FunctionCommandParameter(UserInteractionType userInteractionType, FunctionType functionType)
        {
            UserInteractionType = userInteractionType;

            FunctionType = functionType;
            FunctionText = Functions.GetFunctionText(functionType);
        }
    }
}
