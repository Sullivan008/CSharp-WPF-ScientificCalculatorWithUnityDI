using System;
using Calculator.Core.Enums;

namespace Calculator.Core.Exceptions.Models
{
    public class ErrorModel
    {
        public string Message { get; }

        public string Exception { get; }

        public ErrorModel(EnvironmentType environmentType, Exception ex)
        {
            Message = environmentType == EnvironmentType.Dev 
                ? ex.Message 
                : "Please check whether your data is correct and repeat the action." +
                  " If this error occurs again there seems to be a more serious malfunction in the application, and you better close it";
            Exception = environmentType == EnvironmentType.Dev
                ? ex.ToString()
                : string.Empty;
        }
    }
}
