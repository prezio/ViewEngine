using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ViewEngine.Core.Grammar;
using ViewEngine.Core.Grammar.Outputs;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.MainView;

namespace ViewEngine.Core.Engine
{
    public class CoreEngine : ICoreEngine
    {
        #region private region for parsing file
        private static ViewEngineParser GenerateParser(StreamReader reader)
        {
            var inputStream = new AntlrInputStream(reader);
            var viewEngineLexer = new ViewEngineLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(viewEngineLexer);
            return new ViewEngineParser(commonTokenStream);
        }

        private static ViewEngineParser GenerateParser(string filePath)
        {
            var reader = new StreamReader(filePath);
            return GenerateParser(reader);
        }

        private static ViewEngineVisitor CreateVisitor()
        {
            return new ViewEngineVisitor();
        }

        private static MainOutput ParseMainFile(string mainFilePath)
        {
            var parser = GenerateParser(mainFilePath);
            var visitor = CreateVisitor();
            visitor.Visit(parser.main());
            return new MainOutput(new RegularScope(visitor.Result),
                visitor.Models,
                visitor.Mixins);
        }

        private static SecondaryOutput ParseSecondaryFile(string secondaryFilePath)
        {
            var parser = GenerateParser(secondaryFilePath);
            var visitor = CreateVisitor();
            visitor.Visit(parser.secondary());
            return new SecondaryOutput(visitor.Mixins);
        }

        private string ArrangeUsingRoslyn(string csCode)
        {
            var tree = CSharpSyntaxTree.ParseText(csCode);
            var root = tree.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();
            return ret;
        }
        #endregion

        #region private region for class templates
        private readonly TemplateViewManager _mainViewManager;
        #endregion
        
        public string Render(
            string viewName,
            string namespaceName,
            string mainFilePath,
            string[] secondaryFilePaths)
        {
            var mainOutput = ParseMainFile(mainFilePath);
            var secondaryOutputs = secondaryFilePaths.Select(ParseSecondaryFile).ToArray();
            var renderOutput = _mainViewManager.GenerateMainView(
                viewName,
                namespaceName,
                mainOutput,
                secondaryOutputs
            );
            return ArrangeUsingRoslyn(renderOutput);
        }

        public void Render(
            string viewName,
            string namespaceName,
            string mainFilePath,
            string[] secondaryFilePaths,
            string outputViewPath)
        {
            File.WriteAllText(outputViewPath, Render(viewName, namespaceName, mainFilePath, secondaryFilePaths));
        }

        public CoreEngine()
        {
            _mainViewManager = TemplateViewManager.CreateMainViewManager();
        }
    }
}
