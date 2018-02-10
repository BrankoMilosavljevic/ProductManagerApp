using System;

namespace ProductManagerApp.Model
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}