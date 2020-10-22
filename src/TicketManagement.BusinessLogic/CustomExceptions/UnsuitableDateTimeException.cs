using System;
using System.Runtime.Serialization;

namespace Ticketmanagement.BusinessLogic.Services
{
    [Serializable]
    public class UnsuitableDateTimeException : Exception
    {
        private const string ExceptionMessage = "The venue already have has event for the same time.";

        public UnsuitableDateTimeException()
            : base(ExceptionMessage)
        {
        }

        public UnsuitableDateTimeException(string message)
            : base(message)
        {
        }

        public UnsuitableDateTimeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnsuitableDateTimeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
