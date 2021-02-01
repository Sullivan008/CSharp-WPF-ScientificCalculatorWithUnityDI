using Calculator.Core.CommandParameters.Interfaces;
using Calculator.Views.Calculator.Enums;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.CommandParameters
{
    public class TrigonometricFunctionCommandParameter : ICommandParameter
    {
        public UserInteractionType UserInteractionType { get; }

        public UserInteractionType InverseUserInteractionType { get; }

        public TrigonometricFunctionType TrigonometricFunctionType { get; }

        public TrigonometricFunctionType InverseTrigonometricFunctionType { get; }

        public string TrigonometricFunctionText { get; }

        public string InverseTrigonometricFunctionText { get; }

        public TrigonometricFunctionCommandParameter(UserInteractionType userInteractionType, UserInteractionType inverseUserInteractionType,
            TrigonometricFunctionType trigonometricFunctionType, TrigonometricFunctionType inverseTrigonometricFunctionType)
        {
            UserInteractionType = userInteractionType;
            InverseUserInteractionType = inverseUserInteractionType;

            TrigonometricFunctionType = trigonometricFunctionType;
            InverseTrigonometricFunctionType = inverseTrigonometricFunctionType;

            TrigonometricFunctionText = Functions.GetTrigonometricFunctionText(trigonometricFunctionType);
            InverseTrigonometricFunctionText = Functions.GetTrigonometricFunctionText(inverseTrigonometricFunctionType);
        }
    }
}
