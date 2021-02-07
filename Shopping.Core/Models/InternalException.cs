using System;
using System.Runtime.Serialization;

namespace Shopping.Core.Models
{
    [Serializable]
    public class InternalException : Exception
    {
        private int _id;

        public InternalException()
        {
        }

        public InternalException(int id)
        {
            _id = id;
        }

        public InternalException(string message) : base(message)
        {
        }

        public InternalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InternalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
