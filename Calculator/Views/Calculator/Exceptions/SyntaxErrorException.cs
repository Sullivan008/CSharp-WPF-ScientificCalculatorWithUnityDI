using System;

namespace Calculator.Views.Calculator.Exceptions
{
    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException()
        { }

        public SyntaxErrorException(string message) : base(message)
        { }

        public SyntaxErrorException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
