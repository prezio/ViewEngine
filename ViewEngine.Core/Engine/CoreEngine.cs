using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using ViewEngine.Core.Grammar;
using ViewEngine.Core.Grammar.Outputs;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.MainView;

namespace ViewEngine.Core.Engine
{
    public class CoreEngine
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
                visitor.Models);
        }

        private static SecondaryOutput ParseSecondaryFile(string secondaryFilePath)
        {
            var parser = GenerateParser(secondaryFilePath);
            var visitor = CreateVisitor();
            visitor.Visit(parser.secondary());
            return new SecondaryOutput(visitor.Result);
        }
        #endregion

        #region private region for class templates
        private readonly TemplateMainViewManager _mainViewManager;
        #endregion
        
        public string Render(
            string viewName,
            string namespaceName,
            string mainFilePath,
            string[] secondaryFilePaths)
        {
            var mainOutput = ParseMainFile(mainFilePath);
            //var secondaryOutputs = secondaryFilePaths.Select(ParseSecondaryFile);
            var renderOutput = _mainViewManager.GenerateMainView(
                viewName,
                namespaceName,
                mainOutput,
                null
            );
            return renderOutput;
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
            _mainViewManager = TemplateMainViewManager.CreateMainViewManager();
        }
    }
}
