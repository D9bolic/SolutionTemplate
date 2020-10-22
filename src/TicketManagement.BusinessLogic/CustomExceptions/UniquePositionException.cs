using System;
using System.Runtime.Serialization;

namespace Ticketmanagement.BusinessLogic.CustomExceptions
{
    [Serializable]
    public class UniquePositionException : Exception
    {
        private const string ExceptionMessage = "Row and number should be unique for area.";

        public UniquePositionException()
            : base(ExceptionMessage)
        {
        }

        public UniquePositionException(string message)
            : base(message)
        {
        }

        public UniquePositionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UniquePositionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
