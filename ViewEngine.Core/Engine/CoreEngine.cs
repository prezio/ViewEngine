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
using ViewEngine.Core.Templates.OutputView;

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

        private static HelperOutput ParseHelperFile(string secondaryFilePath)
        {
            var parser = GenerateParser(secondaryFilePath);
            var visitor = CreateVisitor();
            visitor.Visit(parser.secondary());
            return new HelperOutput(visitor.Mixins);
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
        private readonly TemplateViewManager _viewManager;
        #endregion

        #region ICoreEngine Interface methods
        public string RenderMainView(
            string viewName,
            string namespaceName,
            string mainFilePath)
        {
            var mainOutput = ParseMainFile(mainFilePath);
            var renderOutput = _viewManager.GenerateMainView(
                viewName,
                namespaceName,
                mainOutput
            );
            return ArrangeUsingRoslyn(renderOutput);
        }

        public string RenderHelper(
            string helperName,
            string namespaceName,
            string helperFilePath)
        {
            var helperOutput = ParseHelperFile(helperFilePath);
            var renderOutput = _viewManager.GenerateHelper(
                helperName,
                namespaceName,
                helperOutput
            );
            return ArrangeUsingRoslyn(renderOutput);
        }
        #endregion

        public CoreEngine()
        {
            _viewManager = TemplateViewManager.CreateViewManager();
        }
    }
}
