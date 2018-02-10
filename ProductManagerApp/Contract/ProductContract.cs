using System;
using ProductManagerApp.Model;

namespace ProductManagerApp.Contract
{
    public class ProductContract
    {
        public void UpdateDomainModel(Product product)
        {
            product.Name = Name;
            product.Photo = Photo;
            product.Price = Price;
            product.LastUpdated = LastUpdated;
        }

        public Product ToDomainModel()
        {
            return new Product()
            {
                Name = Name,
                Photo = Photo,
                Price = Price,
                LastUpdated = LastUpdated

            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
