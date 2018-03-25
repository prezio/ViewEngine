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
        private static ViewEngineParser GenerateParser(TextReader reader)
        {
            var inputStream = new AntlrInputStream(reader);
            var viewEngineLexer = new ViewEngineLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(viewEngineLexer);
            return new ViewEngineParser(commonTokenStream);
        }

        private static ViewEngineVisitor CreateVisitor()
        {
            return new ViewEngineVisitor();
        }

        private static MainOutput ParseMainFile(TextReader mainTextReader)
        {
            var parser = GenerateParser(mainTextReader);
            var visitor = CreateVisitor();
            visitor.Visit(parser.main());
            return new MainOutput(new RegularScope(visitor.Result),
                visitor.Model,
                visitor.Usings,
                visitor.Mixins);
        }

        private static HelperOutput ParseHelperFile(TextReader helperTextReader)
        {
            var parser = GenerateParser(helperTextReader);
            var visitor = CreateVisitor();
            visitor.Visit(parser.secondary());
            return new HelperOutput(visitor.Mixins,
                visitor.Usings);
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
            string viewPathKey,
            string namespaceName,
            TextReader mainTextReader)
        {
            var mainOutput = ParseMainFile(mainTextReader);
            var renderOutput = _viewManager.GenerateMainView(
                viewName,
                viewPathKey,
                namespaceName,
                mainOutput
            );
            return ArrangeUsingRoslyn(renderOutput);
        }

        public string RenderHelper(
            string helperName,
            string namespaceName,
            TextReader helperTextReader)
        {
            var helperOutput = ParseHelperFile(helperTextReader);
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
