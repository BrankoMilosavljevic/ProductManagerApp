using System;
using ProductManagerApp.Domain;

namespace ProductManagerApp.Tests
{
    public class ProductBuilder
    {
        private int _id = 1;
        private string _name = "Clean code";
        private string _photo = "C:/Images/Books/CleanCode.jpg";
        private double _price = 60;
        private DateTime _lastUpdated = DateTime.Now;

        public ProductBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public ProductBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public ProductBuilder WithPhoto(string photo)
        {
            _photo = photo;
            return this;
        }
        public ProductBuilder WithPrice(double price)
        {
            _price = price;
            return this;
        }
        public ProductBuilder LastUpdated(DateTime lastUpdated)
        {
            _lastUpdated = lastUpdated;
            return this;
        }

        public Product Build()
        {
            return new Product
            {
                Id = _id,
                Name = _name,
                Photo = _photo,
                Price = _price,
                LastUpdated = _lastUpdated
            };
        }

        public static implicit operator Product(ProductBuilder builder)
        {
            return builder.Build();
        }
    }
}
