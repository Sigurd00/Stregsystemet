using System;

namespace Stregsystemet
{
    public class BuyTransaction : Transaction
    {
        private readonly Product _product;
        public BuyTransaction(User user, Product product) : base(user)
        {
            //This way, the amount of the transaction will be set whenever the transaction object is created
            //and therefore it will not change if the price of the product changes
            Amount = product.Price;
            _product = product;
        }

        public override void Execute()
        {
            if(User.Balance < Amount && !_product.CanBeBoughtOnCredit)
            {
                throw new InsufficientCreditsException(User, _product);
            }
            if(!_product.Active)
            {
                throw new InactiveProductException(User, _product);
            }
            User.Balance -= Amount;
        }

        public override string ToString()
        {
            return $"KØB: {Amount}, {User}, {_product}, {Date} <{ID}>";
        }
    }
}
