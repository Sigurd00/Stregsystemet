using System;
using System.Runtime.Serialization;

namespace Stregsystemet
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public string? Username { get; set; }
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string? username) 
            : base($"Could not find user [{username}]")
        {
            Username = username;
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}