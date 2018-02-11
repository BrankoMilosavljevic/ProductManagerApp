using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProductManagerApp.Domain;
using ProductManagerApp.Services;

namespace ProductManagerApp.Tests
{
    [Category("unit")]
    public class ProductFixture
    {
        [TestCase("TestBook", 60, 58, 62, 1, TestName = "Regular case")]
        [TestCase("TestBook", 60, 61, 62, 0, TestName = "Price from too high")]
        [TestCase("TestBook", 60, 58, 59, 0, TestName = "Price to too low")]
        public void ProductQueryExtension_Test(string name, double actualPrice, double priceFrom, double priceTo, int expectedCount)
        {
            Product product = new ProductBuilder()
                                .WithName(name)
                                .WithPrice(actualPrice);

            IQueryable<Product> query = Enumerable.Repeat(product, 1).AsQueryable();
            var result = query.WhereFilterIsSatisfied(name, priceFrom, priceTo);

            result.Should().HaveCount(expectedCount);
        }
    }
}
