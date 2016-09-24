using ProductManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductManagerApp.DAL
{
    public class ProductDbInitializer : CreateDatabaseIfNotExists<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            var products = new List<Product> {
                new Product {  Id = 1, Name = "Black Swan", Photo = "C:/Images/Books/BlackSwan.jpg", Price = 24.2, LastUpdated = DateTime.Now  }
            };

            products.ForEach(p => context.Products.Add(p));
        }
    }
}