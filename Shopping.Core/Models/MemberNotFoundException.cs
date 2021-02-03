using System;
using System.Runtime.Serialization;

namespace Shopping.Core.Models
{
    [Serializable]
    internal class MemberNotFoundException : Exception
    {
        private int _memberId;

        public MemberNotFoundException()
        {
        }

        public MemberNotFoundException(int memberId)
        {
            this._memberId = memberId;
        }

        public MemberNotFoundException(string message) : base(message)
        {
        }

        public MemberNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
