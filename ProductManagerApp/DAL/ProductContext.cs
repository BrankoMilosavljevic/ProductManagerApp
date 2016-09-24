using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProductManagerApp.Models;

namespace ProductManagerApp.DAL
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