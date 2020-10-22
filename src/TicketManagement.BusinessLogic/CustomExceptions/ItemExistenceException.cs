using System;
using System.Runtime.Serialization;

namespace Ticketmanagement.BusinessLogic.CustomExceptions
{
    [Serializable]
    public class ItemExistenceException : Exception
    {
        public ItemExistenceException()
            : base()
        {
        }

        public ItemExistenceException(string message)
            : base(message)
        {
        }

        public ItemExistenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ItemExistenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
