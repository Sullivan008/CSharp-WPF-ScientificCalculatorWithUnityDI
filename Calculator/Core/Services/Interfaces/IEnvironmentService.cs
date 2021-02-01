using Calculator.Core.Enums;

namespace Calculator.Core.Services.Interfaces
{
    public interface IEnvironmentService
    {
        EnvironmentType GetEnvironmentType();
    }
}
