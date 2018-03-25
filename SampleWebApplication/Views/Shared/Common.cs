using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Engine;

// Additional Namespaces
// =====================
namespace SampleWebApplication.Views
{
    public static class CommonHelper
    {
        public static void Html(TextWriter writer, IReadOnlyDictionary<string, Action> environment)
        {
            writer.Write(@"<html>");
            writer.Write("\r\n");
            writer.Write(@"<head>");
            writer.Write("\r\n");
            writer.Write(@"<title>");
            writer.Write("\r\n");
            RenderHelpers.InvokeVariable(environment, "title");
            writer.Write("\r\n");
            writer.Write(@"</title>");
            writer.Write("\r\n");
            writer.Write(@"</head>");
            writer.Write("\r\n");
            writer.Write(@"<body>");
            writer.Write("\r\n");
            RenderHelpers.InvokeVariable(environment, "body");
            writer.Write("\r\n");
            writer.Write(@"</body>");
            writer.Write("\r\n");
            writer.Write(@"</html>");
            writer.Write("\r\n");
        }

        public static void H1(TextWriter writer, IReadOnlyDictionary<string, Action> environment)
        {
            writer.Write(@"<h1> ");
            RenderHelpers.InvokeVariable(environment, "header");
            writer.Write(@" </h1>");
            writer.Write("\r\n");
        }

        public static void Line(TextWriter writer, IReadOnlyDictionary<string, Action> environment)
        {
            RenderHelpers.InvokeVariable(environment, "line");
            writer.Write(@" </br>");
            writer.Write("\r\n");
        }
    }
}