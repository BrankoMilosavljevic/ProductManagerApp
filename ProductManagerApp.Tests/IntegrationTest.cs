using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagerApp.Contract;
using ProductManagerApp.Controllers;

namespace ProductManagerApp.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void ProductTest()
        {
            ProductContract productContract = new ProductContract { Id = 0, Name = "White Swan - test", Photo = "C:/Images/Books/BlackSwan.jpg", Price = 24.2, LastUpdated = DateTime.Now };
            ProductController controller = new ProductController();

            controller.Post(productContract);
            List<ProductContract> productContracts = controller.GetAllByFilter("White Swan - test", 0, 50);
            Assert.AreEqual(productContracts.Count, 1);

            productContract.Name = "White Swan - test (renamed)";
            controller.Post(productContract);
            productContracts = controller.GetAllByFilter("White Swan - test (renamed)", 0, 50);
            Assert.AreEqual(productContracts.Count, 1);

            controller.Delete(Int32.Parse(productContracts.First().Id.ToString()));
            productContracts = controller.GetAllByFilter("White Swan - test (renamed)", 0, 50);
            Assert.AreEqual(productContracts.Count, 0);

        }
    }
}
