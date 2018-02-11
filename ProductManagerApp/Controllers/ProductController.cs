using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using ProductManagerApp.Contract;
using ProductManagerApp.Data;
using ProductManagerApp.Domain;
using ProductManagerApp.Services;

namespace ProductManagerApp.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductController()
        {
            var context = new ProductManagerContext();
            _productRepository = new EntityFrameworkRepository<Product>(context);
            _unitOfWork = new ProductManagerUnitOfWork(context);
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

        [Route("api/ProductService")]
        [HttpPost]
        public void Post(ProductContract productContract)
        {
            if (productContract.Id == 0)
            {
                AddProduct(productContract);
                return;
            }

            UpdateProduct(productContract);    
        }

        [Route("api/ProductService/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            Product product = _productRepository.FindById(id);
            _productRepository.Delete(product);
            _unitOfWork.SaveChanges();
        }

        private void UpdateProduct(ProductContract productContract)
        {  
            Product product = _productRepository.FindById(productContract.Id);
            productContract.UpdateDomainModel(product);
            _unitOfWork.SaveChanges();
        }

        private void AddProduct(ProductContract productContract)
        {
            _productRepository.Add(productContract.ToDomainModel());
            _unitOfWork.SaveChanges();
        }
    }
}