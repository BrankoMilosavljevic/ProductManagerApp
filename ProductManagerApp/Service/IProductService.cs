using ProductManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerApp.Service
{
    public interface IProductService
    {
        DataRow GetById(int Id);
        DataTable GetAll();
        bool PostProduct(Product product);
        bool PutProduct(Product product);
        bool Delete(int id);
        DataTable GetAll(string name, double priceFrom, double priceTo);
    }
}
