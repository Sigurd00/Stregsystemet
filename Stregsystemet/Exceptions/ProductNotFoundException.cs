using System;
using System.Runtime.Serialization;

namespace Stregsystemet
{
    [Serializable]
    public class ProductNotFoundException : Exception
    {
        public int? ProductID { get; set; }
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string? message) : base(message)
        {
        }
        public ProductNotFoundException(int id) 
            : base($"Could not find product with id={id}")
        {
            ProductID = id;
        }

        public ProductNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}