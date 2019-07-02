using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobyDick_Web.Controllers;
using MobyDick_Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MobyDick_Web_Test
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void CheckReturnedModel()
        {
            // Arrange  
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            var dataModel = (List<DataModel>)result.ViewData.Model;
            Assert.AreEqual(10, dataModel.Count);
           
        }
    }
}
