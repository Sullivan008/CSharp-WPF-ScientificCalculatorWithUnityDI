using System;
using System.Configuration;
using Calculator.Core.Enums;
using Calculator.Core.EnvironmentVariables;
using Calculator.Core.EnvironmentVariables.Enums;
using Calculator.Core.EnvironmentVariables.Exceptions;
using Calculator.Core.Services.Interfaces;

namespace Calculator.Core.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        public EnvironmentType GetEnvironmentType()
        {
            int environmentTypeCode;

            string environmentVariableKey =
                EnvironmentVariableKeys.GetEnvironmentVariableKey(EnvironmentVariableKey.EnvironmentType);

            if (int.TryParse(Environment.GetEnvironmentVariable(environmentVariableKey), out environmentTypeCode))
            {
                return (EnvironmentType) environmentTypeCode;
            }

            if (int.TryParse(ConfigurationManager.AppSettings[environmentVariableKey], out environmentTypeCode))
            {
                return (EnvironmentType)environmentTypeCode;
            }

            throw new MissingEnvironmentVariableException($"Missing OS Environment Variable name: {environmentVariableKey}");
        }
    }
}
