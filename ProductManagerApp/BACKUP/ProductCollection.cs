using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagerApp.Models
{
    public class ProductCollection : IProductCollection
    {
        private List<Product> products = new List<Product>();
        private int _nextId = 1;

        public ProductCollection()
        {
            Add(new Product { Name = "Shirt", Photo = "C:\\Pictures\\Shirt1.jpg", Price = 24.5, LastUpdated = DateTime.Now });
            Add(new Product { Name = "Coat", Photo = "C:\\Pictures\\Coat1.jpg", Price = 188.5, LastUpdated = DateTime.Now });
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product Get(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public Product Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            products.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = products.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            products.Add(item);
            return true;
        }
    }
}