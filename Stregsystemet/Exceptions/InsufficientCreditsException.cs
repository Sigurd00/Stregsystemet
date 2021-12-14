using System;
using System.Runtime.Serialization;

namespace Stregsystemet
{
    [Serializable]
    public class InsufficientCreditsException : Exception
    {
        public User? User { get; set; }
        public Product? Product { get; set; }
        public InsufficientCreditsException()
        {
        }

        public InsufficientCreditsException(string? message) : base(message)
        {
        }
        public InsufficientCreditsException(User user, Product product) 
            : base($"USER: {user} tried to buy PRODUCT: {product}, but didnt have enough credit")
        {
            User = user;
            Product = product;
        }

        public InsufficientCreditsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsufficientCreditsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}