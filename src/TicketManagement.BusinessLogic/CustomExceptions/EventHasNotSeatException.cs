using System;
using System.Runtime.Serialization;

namespace Ticketmanagement.BusinessLogic.CustomExceptions
{
    [Serializable]
    public class EventHasNotSeatException : Exception
    {
        private const string ExceptionMessage = "Event does not have any seat.";

        public EventHasNotSeatException()
            : base(ExceptionMessage)
        {
        }

        public EventHasNotSeatException(string message)
            : base(message)
        {
        }

        public EventHasNotSeatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EventHasNotSeatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
