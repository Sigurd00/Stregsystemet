﻿using System;

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
                throw new InsufficientCreditsException($"USER: {User} tried to buy PRODUCT: {_product}, but didnt have enough credit");
            }
            if(!_product.Active)
            {
                throw new InactiveProductException($"USER: {User} tried to buy PRODUCT: {_product}, but product is inactive");
            }
            User.Balance -= Amount;
        }

        public override string ToString()
        {
            return $"KØB: {Amount}, {User}, {_product}, {Date} <{ID}>";
        }
    }
}
