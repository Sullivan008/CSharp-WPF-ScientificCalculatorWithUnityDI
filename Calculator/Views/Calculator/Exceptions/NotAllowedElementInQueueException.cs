using System;

namespace Calculator.Views.Calculator.Exceptions
{
    public class NotAllowedElementInQueueException : Exception
    {
        public NotAllowedElementInQueueException()
        { }

        public NotAllowedElementInQueueException(string message) : base(message)
        { }

        public NotAllowedElementInQueueException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
