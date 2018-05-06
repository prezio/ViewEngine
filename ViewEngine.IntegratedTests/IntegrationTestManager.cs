using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewEngine.IntegratedTests
{
    public class IntegrationTestManager
    {
        private string Normalize(string content)
        {
            return Regex.Replace(content, @"\s+", "");
        }

        public bool PerformTest(IView testRenderer,
            string correctOutput,
            ViewContext viewContext = null)
        {
            var writer = new StringWriter();
            testRenderer.Render(viewContext, writer);

            writer.Close();
            var testOutput = writer.ToString();

            return Normalize(testOutput)
                .Equals(Normalize(correctOutput));
        }

        public bool PerformTestWithOutputFilePath(IView testRenderer,
            string correctOutputPath,
            ViewContext viewContext = null)
        {
            var correctOutput = File.ReadAllText(correctOutputPath);
            return PerformTest(
                testRenderer,
                correctOutput,
                viewContext);
        }
    }
}
