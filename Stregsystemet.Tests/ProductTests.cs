using Xunit;

namespace Stregsystemet.Tests
{
    public class ProductTests
    {
        [Fact]
        public void ProductName_IsNotNull()
        {
            Product product = new Product(1, "Diverse", 100, true);
            Assert.NotNull(product.Name);
        }
        [Fact]
        public void ToString_returns_correct_string()
        {
            Product product = new Product(1, "Diverse", 100, true);
            Assert.Equal("1 Diverse 100", product.ToString());
        }
        [Fact]
        public void SeasonalProduct_IsA_Product()
        {
            Product product = new SeasonalProduct(1, "Diverse", 100, true);
            Assert.IsAssignableFrom<Product>(product);
        }
    }
}
