using System;
using System.Runtime.Serialization;

namespace Stregsystemet
{
    [Serializable]
    public class InactiveProductException : Exception
    {
        public InactiveProductException()
        {
        }

        public InactiveProductException(string? message) : base(message)
        {
        }

        public InactiveProductException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InactiveProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}