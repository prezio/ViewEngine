using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewEngine.IntegratedTests.Medium
{
    [TestClass]
    public class MediumIntegrationTests
    {
        MediumView _testRenderer;
        IntegrationTestManager _testManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _testRenderer = new MediumView();
            _testManager = new IntegrationTestManager();
        }

        [TestMethod]
        public void IntegrationTest_Medium_Test1()
        {
            var customer = new CustomerViewModel
            {
                Name = "Stanley",
                Address = "USA"
            };

            var viewContext = new ViewContext();
            viewContext.ViewData = new ViewDataDictionary();
            viewContext.ViewData.Model = customer;

            var result = _testManager.PerformTestWithOutputFilePath(
                _testRenderer,
                "Medium\\Test1.out",
                viewContext);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IntegrationTest_Medium_Test2()
        {
            var customer = new CustomerViewModel
            {
                Name = "Marek",
                Address = "Polska"
            };

            var viewContext = new ViewContext();
            viewContext.ViewData = new ViewDataDictionary();
            viewContext.ViewData.Model = customer;

            var result = _testManager.PerformTestWithOutputFilePath(
                _testRenderer,
                "Medium\\Test2.out",
                viewContext);
            Assert.IsTrue(result);
        }
    }
}
