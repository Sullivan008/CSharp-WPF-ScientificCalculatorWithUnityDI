using System;

namespace Calculator.Views.Calculator.Exceptions
{
    public class UnknownElementInQueueException : Exception
    {
        public UnknownElementInQueueException()
        { }

        public UnknownElementInQueueException(string message) : base(message)
        { }

        public UnknownElementInQueueException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
