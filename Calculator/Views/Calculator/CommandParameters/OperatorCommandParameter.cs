using Calculator.Core.CommandParameters.Interfaces;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.CommandParameters
{
    public class OperatorCommandParameter : ICommandParameter
    {
        public UserInteractionType UserInteractionType { get; }

        public OperatorType OperatorType { get; }

        public string OperatorText { get; }

        public OperatorCommandParameter(UserInteractionType userInteractionType, OperatorType operatorType)
        {
            UserInteractionType = userInteractionType;
            OperatorType = operatorType;
            OperatorText = Operators.GetOperatorText(operatorType);
        }
    }
}
