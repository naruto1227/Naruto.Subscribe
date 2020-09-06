using System;
using System.Runtime.Serialization;

namespace Naruto.Subscribe.Object
{
    [Serializable]
    internal class NotMethodException : Exception
    {
        public NotMethodException()
        {
        }

        public NotMethodException(string message) : base(message)
        {
        }

        public NotMethodException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}