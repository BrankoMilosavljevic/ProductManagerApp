using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProductManagerApp.Domain;

namespace ProductManagerApp.Data
{
    public class ProductDbInitializer : CreateDatabaseIfNotExists<ProductManagerContext>, IDatabaseInitializer<ProductManagerContext>
    {
        protected override void Seed(ProductManagerContext context)
        {
            var products = new List<Product> {
                new Product {  Id = 1, Name = "Black Swan", Photo = "C:/Images/Books/BlackSwan.jpg", Price = 24.2, LastUpdated = DateTime.Now  }
            };

            products.ForEach(p => context.Products.Add(p));
        }
    }
}