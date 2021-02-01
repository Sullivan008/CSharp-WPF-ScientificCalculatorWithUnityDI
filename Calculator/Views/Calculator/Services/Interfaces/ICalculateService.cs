using System.Collections.Generic;
using Calculator.Views.Calculator.Enums;

namespace Calculator.Views.Calculator.Services.Interfaces
{
    public interface ICalculateService
    {
        double Calculate(Queue<string> queue, AngleUnitType angleUnitType);
    }
}
