using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerApp.Models
{
    public interface IProductCollection
    {
        Product Get(int id);
        Product Add(Product item);
        void Remove(int id);
        bool Update(Product item);
        IEnumerable<Product> GetAll();
    }
}
