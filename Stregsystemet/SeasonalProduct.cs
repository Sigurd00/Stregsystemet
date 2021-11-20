using System;

namespace Stregsystemet
{
    public class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set;}
        public SeasonalProduct(int id, string name, decimal price, bool active)
            : base(id, name, price, active)
        {
        }
    }
}
