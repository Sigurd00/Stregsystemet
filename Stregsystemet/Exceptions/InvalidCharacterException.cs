using System;
using System.Runtime.Serialization;

namespace Stregsystemet
{
    [Serializable]
    public class InvalidCharacterException : Exception
    {
        public InvalidCharacterException()
        {
        }

        public InvalidCharacterException(string? message) : base(message)
        {
        }

        public InvalidCharacterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidCharacterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}