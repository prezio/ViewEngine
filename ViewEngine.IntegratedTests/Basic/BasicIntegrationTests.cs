using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.IntegratedTests.Basic
{
    [TestClass]
    public class BasicIntegrationTests
    {
        BasicView _testRenderer;
        IntegrationTestManager _testManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _testRenderer = new BasicView();
            _testManager = new IntegrationTestManager();
        }

        [TestMethod]
        public void IntegrationTest_Basic_Test1()
        {
            var result = _testManager.PerformTest(_testRenderer, "Witaj świecie!!!");
            Assert.IsTrue(result);
        }
    }
}
