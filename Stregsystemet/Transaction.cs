using System;

namespace Stregsystemet
{
    public abstract class Transaction
    {
        private static int s_ID = 0;
        public int ID { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public abstract void Execute();
        public abstract override string ToString();

        protected Transaction(User user)
        {
            User = user;
            Date = DateTime.Now;
            ID = ++s_ID;
        }
    }
}
