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
        bool Create(Product product);
        bool Update(Product product);
        bool Delete(int id);
        DataTable Search(string name, double? priceFrom, double? priceTo);
    }
}
