using System.Data.Entity;
using ProductManagerApp.Model;

namespace ProductManagerApp.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext()
            : base("ProductContext.DbConnection")
        {
            Database.SetInitializer<ProductContext>(new ProductDbInitializer());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
    }
}