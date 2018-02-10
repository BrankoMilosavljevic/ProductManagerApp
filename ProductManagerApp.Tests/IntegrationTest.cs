using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagerApp.Models;
using System.Data;
using System.Linq;
using ProductManagerApp.Controllers;

namespace ProductManagerApp.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void ProductTest()
        {
            Product product = new Product{ Id = 0, Name = "White Swan - test", Photo = "C:/Images/Books/BlackSwan.jpg", Price = 24.2, LastUpdated = DateTime.Now };
            ProductController controller = new ProductController();

            controller.PostProduct(product);
            List<Product> products = controller.GetAllByFilter("White Swan - test", 0, 50);
            Assert.AreEqual(products.Count, 1);

            product.Name = "White Swan - test (renamed)";
            controller.PutProduct(product);
            products = controller.GetAllByFilter("White Swan - test (renamed)", 0, 50);
            Assert.AreEqual(products.Count, 1);

            controller.Delete(Int32.Parse(products.First().Id.ToString()));
            products = controller.GetAllByFilter("White Swan - test (renamed)", 0, 50);
            Assert.AreEqual(products.Count, 0);

        }
    }
}
