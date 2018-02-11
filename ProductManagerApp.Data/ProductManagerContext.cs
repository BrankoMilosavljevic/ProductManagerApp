using System.Data.Entity;
using ProductManagerApp.Domain;

namespace ProductManagerApp.Data
{
    public class ProductManagerContext : DbContext
    {
        public ProductManagerContext()
            : base("ProductContext.DbConnection")
        {
            Database.SetInitializer<ProductManagerContext>(new ProductDbInitializer());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
    }
}