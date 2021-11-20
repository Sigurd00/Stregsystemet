using System;

namespace Stregsystemet
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) : base(user)
        {
            //Amount must be positive for an insertion 
            if(amount < 0)
            {
                throw new ArgumentException($"Amount must be positive, got amount: {amount}");
            }
            Amount = amount;

        }

        public override void Execute()
        {
            User.Balance += Amount;
        }

        public override string ToString()
        {
            return $"INDBETALING: {Amount}, {User}, {Date} <{ID}>";
        }
    }
}
