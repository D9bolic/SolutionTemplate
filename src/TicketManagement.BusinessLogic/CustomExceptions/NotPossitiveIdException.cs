using System;
using System.Runtime.Serialization;

namespace Ticketmanagement.BusinessLogic.CustomExceptions
{
    [Serializable]
    public class NotPossitiveIdException : Exception
    {
        private const string ExceptionMessage = "Id should be more than 0.";

        public NotPossitiveIdException()
            : base(ExceptionMessage)
        {
        }

        public NotPossitiveIdException(string message)
            : base(message)
        {
        }

        public NotPossitiveIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NotPossitiveIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
