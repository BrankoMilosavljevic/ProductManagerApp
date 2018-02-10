using System;

namespace ProductManagerApp.Client.Contract
{
    public class ProductContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
