namespace Stregsystemet
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }
        public Product(int id, string name, decimal price, bool active)
        {
            ID = id;
            Name = name;
            Price = price;
            Active = active;
        }
        public override string ToString()
        {
            return $"{ID} {Name} {Price}";
        }
    }
}
