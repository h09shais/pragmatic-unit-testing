using System;
using System.Runtime.Serialization;

namespace Shopping.Core.Models
{
    [Serializable]
    public class NotFoundException : Exception
    {
        private int _Id;

        public NotFoundException()
        {
        }

        public NotFoundException(int Id)
        {
            this._Id = Id;
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
