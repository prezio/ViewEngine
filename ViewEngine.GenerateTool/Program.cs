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
        static void RenderMainViews(ICoreEngine engine, string solutionDirectory, string namespaceName)
        {
            var mainFilePaths = Directory.GetFiles(solutionDirectory, "*.myview", SearchOption.AllDirectories);
            foreach (var path in mainFilePaths)
            {
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
            var mainFilePaths = Directory.GetFiles(solutionDirectory, "*.myhelper", SearchOption.AllDirectories);
            foreach (var path in mainFilePaths)
            {
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
            var engine = new CoreEngine();
            RenderMainViews(engine, options.SolutionDirectory, options.NamespaceName);
            RenderHelpers(engine, options.SolutionDirectory, options.NamespaceName);
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(RunOptionsAndReturnExitCode);
        }
    }
}
