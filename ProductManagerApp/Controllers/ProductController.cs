using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using ProductManagerApp.Contract;
using ProductManagerApp.Data;
using ProductManagerApp.Domain;

namespace ProductManagerApp.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IRepository<Product> _productRepository;
        public ProductController()
        {
            _productRepository = new EntityFrameworkRepository<Product>(new ProductContext());
        }

        [Route("api/ProductService/{id}")]
        [HttpGet]
        public ProductContract GetById(int id)
        {
            var product = _productRepository.Items
                .FirstOrDefault(i => i.Id == id);

            return Mapper.Map<ProductContract>(product);
        }

        [Route("api/ProductService/all")]
        [HttpGet]
        public List<ProductContract> GetAll()
        {
            var products = _productRepository.Items.ToList();
            return Mapper.Map<List<ProductContract>>(products);
        }

        [Route("api/ProductService/{name}/{priceFrom}/{priceTo}")]
        [HttpGet]
        public List<ProductContract> GetAllByFilter(string name, double priceFrom, double priceTo)
        {
            var products = _productRepository.Items
                .WhereFilterIsSatisfied(name, priceFrom, priceTo)
                .ToList();
            return Mapper.Map<List<ProductContract>>(products);
        }

        public void UpdateProduct(ProductContract productContract)
        {  
            Product product = _productRepository.FindById(productContract.Id);
            productContract.UpdateDomainModel(product);
        }

        public void AddProduct(ProductContract productContract)
        {
            _productRepository.Add(productContract.ToDomainModel());
        }

        public void Delete(int id)
        {
            Product product = _productRepository.FindById(id);
            _productRepository.Delete(product);
        }
    }
}