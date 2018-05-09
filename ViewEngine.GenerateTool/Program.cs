using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using ViewEngine.Core.Engine;

namespace ViewEngine.GenerateTool
{
    class Program
    {
        static void WriteMessage(string message)
        {
            Console.WriteLine($"ViewEngine.GenerateTool> {message}");
        }

        static void RenderMainViews(ICoreEngine engine, string solutionDirectory, string namespaceName)
        {
            var mainFilePaths = Directory.GetFiles(solutionDirectory, "*.myview", SearchOption.AllDirectories);
            foreach (var path in mainFilePaths)
            {
                WriteMessage($"Generating {Path.GetFileName(path)}...");
                var viewName = Path.GetFileNameWithoutExtension(path);
                var content = engine.RenderMainView(viewName,
                    path,
                    namespaceName,
                    new StreamReader(path));
                var outputPath = Path.Combine(Path.GetDirectoryName(path), $"{viewName}.cs");
                File.WriteAllText(outputPath, content);
            }
        }

        static void RenderHelpers(ICoreEngine engine, string solutionDirectory, string namespaceName)
        {
            var helperFilePaths = Directory.GetFiles(solutionDirectory, "*.myhelper", SearchOption.AllDirectories);
            foreach (var path in helperFilePaths)
            {
                WriteMessage($"Generating {Path.GetFileName(path)}...");
                var helperName = Path.GetFileNameWithoutExtension(path);
                var content = engine.RenderHelper(helperName,
                    namespaceName,
                    new StreamReader(path));
                var outputPath = Path.Combine(Path.GetDirectoryName(path), $"{helperName}.cs");
                File.WriteAllText(outputPath, content);
            }
        }

        static void RunOptionsAndReturnExitCode(Options options)
        {
            WriteMessage("Starting View Generation...");
            var engine = new CoreEngine();
            RenderMainViews(engine, options.SolutionDirectory, options.NamespaceName);
            RenderHelpers(engine, options.SolutionDirectory, options.NamespaceName);
            WriteMessage("View Generation completed.");
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(RunOptionsAndReturnExitCode);
        }
    }
}
