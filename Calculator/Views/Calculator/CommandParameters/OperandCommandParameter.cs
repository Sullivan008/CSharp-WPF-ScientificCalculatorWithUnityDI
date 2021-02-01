using Calculator.Core.CommandParameters.Interfaces;
using Calculator.Views.Calculator.Enums;

namespace Calculator.Views.Calculator.CommandParameters
{
    public class OperandCommandParameter : ICommandParameter
    {
        public UserInteractionType UserInteractionType { get; }

        public int OperandValue { get; }

        public OperandCommandParameter(UserInteractionType userInteractionType, int operandValue)
        {
            UserInteractionType = userInteractionType;
            OperandValue = operandValue;
        }
    }
}
