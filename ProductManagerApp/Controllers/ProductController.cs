using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using ProductManagerApp.Data;
using ProductManagerApp.Model;

namespace ProductManagerApp.Controllers
{
    public class ProductController : ApiController
    {
        [Route("api/ProductService/{id}")]
        [HttpGet]
        public Product GetById(int id)
        {
            using (var context = new ProductContext())
            {
                return context.Products.FirstOrDefault(i => i.Id == id);
            }
        }

        [Route("api/ProductService/all")]
        [HttpGet]
        public List<Product> GetAll()
        {
            using (var context = new ProductContext())
            {
                return context.Products.ToList();
            }
        }

        [Route("api/ProductService/{name}/{priceFrom}/{priceTo}")]
        [HttpGet]
        public List<Product> GetAllByFilter(string name, double priceFrom, double priceTo)
        {
            using (var context = new ProductContext())
            {
                return context.Products.Where(p => ((name == "return_all" || p.Name == name) && p.Price >= priceFrom && p.Price <= priceTo)).ToList();
            }
        }

        public void PostProduct(Product product)
        {
            using (var context = new ProductContext())
            {
                if (product.Id == 0)
                    context.Products.Add(product);
                else
                {
                    context.Products.Attach(product);
                    context.Entry(product).State = EntityState.Modified;
                }
            }
        }

        public void PutProduct(Product product)
        {
            using (var context = new ProductContext())
            {
                context.Products.Attach(product);
                context.Entry(product).State = EntityState.Modified;
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
    }
}