using System;
using System.Runtime.Serialization;

namespace Stregsystemet
{
    [Serializable]
    public class InactiveProductException : Exception
    {
        public User? User { get; set; }
        public Product? Product { get; set; }

        public InactiveProductException()
        {
        }

        public InactiveProductException(string? message) : base(message)
        {
        }

        public InactiveProductException(User user, Product product) 
            : base($"USER: {user} tried to buy PRODUCT: {product}, but product is inactive")
        {
            User = user;
            Product = product;
        }

        public InactiveProductException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InactiveProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}