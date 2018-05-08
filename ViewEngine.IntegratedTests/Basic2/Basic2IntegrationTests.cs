using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.IntegratedTests.Basic2
{
    [TestClass]
    public class Basic2IntegrationTests
    {
        Basic2View _testRenderer;
        IntegrationTestManager _testManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _testRenderer = new Basic2View();
            _testManager = new IntegrationTestManager();
        }

        [TestMethod]
        public void IntegrationTest_Basic2_Test1()
        {
            var result = _testManager.PerformTest(_testRenderer,
                $"<p>{Directory.GetCurrentDirectory()}</p>");

            Assert.IsTrue(result);
        }
    }
}
