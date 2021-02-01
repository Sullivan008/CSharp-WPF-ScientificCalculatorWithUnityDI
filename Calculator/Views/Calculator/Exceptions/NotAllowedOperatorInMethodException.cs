using System;

namespace Calculator.Views.Calculator.Exceptions
{
    public class NotAllowedOperatorInMethodException : Exception
    {
        public NotAllowedOperatorInMethodException()
        { }

        public NotAllowedOperatorInMethodException(string message) : base(message)
        { }

        public NotAllowedOperatorInMethodException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
