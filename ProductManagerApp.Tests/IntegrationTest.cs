using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagerApp.Models;
using ProductManagerApp.Service;
using System.Data;

namespace ProductManagerApp.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void ProductTest()
        {
            Product product = new Product{ Id = 0, Name = "White Swan - test", Photo = "C:/Images/Books/BlackSwan.jpg", Price = 24.2, LastUpdated = DateTime.Now };
            ProductServiceController service = new ProductServiceController();

            service.PostProduct(product);
            DataTable dt = service.GetAll("White Swan - test", 0, 50);
            Assert.AreEqual(dt.Rows.Count, 1);

            product.Name = "White Swan - test (renamed)";
            service.PutProduct(product);
            dt = service.GetAll("White Swan - test (renamed)", 0, 50);
            Assert.AreEqual(dt.Rows.Count, 1);

            service.Delete(Int32.Parse(dt.Rows[0]["Id"].ToString()));
            dt = service.GetAll("White Swan - test (renamed)", 0, 50);
            Assert.AreEqual(dt.Rows.Count, 0);

        }
    }
}
