using System;
using System.Runtime.Serialization;

namespace Ticketmanagement.BusinessLogic.CustomExceptions
{
    [Serializable]
    public class NegativePriceException : Exception
    {
        private const string ExceptionMessage = "Price should be more or equals zero.";

        public NegativePriceException()
            : base(ExceptionMessage)
        {
        }

        public NegativePriceException(string message)
            : base(message)
        {
        }

        public NegativePriceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NegativePriceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
