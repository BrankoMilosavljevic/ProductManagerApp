using ProductManagerApp.DAL;
using ProductManagerApp.Extensions;
using ProductManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProductManagerApp.Service
{
    public class ProductService : ApiController, IProductService
    {
        public DataRow GetById(int id)
        {
            using (var context = new ProductContext())
            {
                Product product = context.Products.Where(i => i.Id == id).FirstOrDefault();
                return product.ToDataRow<Product>();
            }
        }

        public DataTable GetAll()
        {
            using (var context = new ProductContext())
            {
                IList<Product> products = context.Products.ToList();
                return products.ToDataTable<Product>();
            }
        }

        public bool Create(Product Product)
        {
            using (var context = new ProductContext())
            {
                context.Products.Add(Product);
                return context.SaveChanges() > 0;
            }
        }

        public bool Update(Product Product)
        {
            using (var context = new ProductContext())
            {
                context.Products.Attach(Product);
                context.Entry(Product).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var context = new ProductContext())
            {
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                return context.SaveChanges() > 0;
            }
        }

        public DataTable Search(string name, double? priceFrom, double? priceTo)
        {
            using (var context = new ProductContext())
            {
                List<Product> products = context.Products.Where(p => ((name == string.Empty || p.Name == name) 
                                                                        && (priceFrom == null || p.Price >= priceFrom) 
                                                                        && (priceTo == null || p.Price <= priceTo)
                                                                        )).ToList();
                return products.ToDataTable<Product>();
            }
        }
    }
}