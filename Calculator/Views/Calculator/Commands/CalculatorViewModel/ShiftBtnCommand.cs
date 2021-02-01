using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.Core.Commands;
using Calculator.Views.Calculator.StaticValues;
using Calculator.Views.Calculator.StaticValues.Enums;

namespace Calculator.Views.Calculator.ViewModels
{
    public partial class CalculatorViewModel
    {
        private ICommand _shiftBtnCommand;
        public ICommand ShiftBtnCommand =>
            _shiftBtnCommand ?? (_shiftBtnCommand = new RelayCommandAsync(ShiftBtnCommandHandler));

        private Task ShiftBtnCommandHandler()
        {
            _isShiftEnabled = !_isShiftEnabled;

            if (_isShiftEnabled)
            {
                SinButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.ArcSin);
                CosButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.ArcCos);
                TanButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.ArcTan);
                LnButtonTextBlockValue = Operations.GetOperationText(OperationType.EToTheNthPow);
                LogButtonTextBlockValue = Operations.GetOperationText(OperationType.TenToTheNthPow);
            }
            else
            {
                SinButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.Sin);
                CosButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.Cos);
                TanButtonTextBlockValue = Functions.GetTrigonometricFunctionText(TrigonometricFunctionType.Tan);
                LnButtonTextBlockValue = Functions.GetFunctionText(FunctionType.Ln);
                LogButtonTextBlockValue = Functions.GetFunctionText(FunctionType.Log);
            }

            return Task.FromResult(true);
        }
    }
}
