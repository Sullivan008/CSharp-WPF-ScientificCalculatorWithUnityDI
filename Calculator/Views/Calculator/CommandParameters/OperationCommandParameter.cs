using Calculator.Core.CommandParameters.Interfaces;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.CommandParameters
{
    public class OperationCommandParameter : ICommandParameter
    {
        public UserInteractionType UserInteractionType { get; }

        public OperationType OperationType { get; }

        public string OperationText { get; }

        public OperationCommandParameter(UserInteractionType userInteractionType, OperationType operationType)
        {
            UserInteractionType = userInteractionType;
            OperationType = operationType;
            OperationText = Operations.GetOperationText(operationType);
        }
    }
}
