using System;
using System.Runtime.Serialization;

namespace Ticketmanagement.BusinessLogic.CustomExceptions
{
    [Serializable]
    public class DescriptionUniqueException : Exception
    {
        public DescriptionUniqueException()
            : base()
        {
        }

        public DescriptionUniqueException(string message)
            : base(message)
        {
        }

        public DescriptionUniqueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DescriptionUniqueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
