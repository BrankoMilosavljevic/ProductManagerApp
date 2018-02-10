using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProductManagerApp.Model;

namespace ProductManagerApp.Data
{
    public class ProductDbInitializer : CreateDatabaseIfNotExists<ProductContext>, IDatabaseInitializer<ProductContext>
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