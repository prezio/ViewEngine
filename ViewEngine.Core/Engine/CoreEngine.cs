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

namespace ViewEngine.Core.Engine
{
    public class CoreEngine
    {
        #region private methods
        private ViewEngineParser GenerateParser(StreamReader reader)
        {
            var inputStream = new AntlrInputStream(reader);
            var viewEngineLexer = new ViewEngineLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(viewEngineLexer);
            return new ViewEngineParser(commonTokenStream);
        }

        private ViewEngineParser GenerateParser(string filePath)
        {
            var reader = new StreamReader(filePath);
            return GenerateParser(reader);
        }

        private ViewEngineVisitor CreateVisitor()
        {
            return new ViewEngineVisitor();
        }

        private MainOutput ParseMainFile(string mainFilePath)
        {
            var parser = GenerateParser(mainFilePath);
            var visitor = CreateVisitor();
            visitor.Visit(parser.main());
            return new MainOutput(new RegularScope(visitor.Result),
                visitor.Models);
        }

        private SecondaryOutput ParseSecondaryFile(string secondaryFilePath)
        {
            var parser = GenerateParser(secondaryFilePath);
            var visitor = CreateVisitor();
            visitor.Visit(parser.secondary());
            return new SecondaryOutput(visitor.Result);
        }
        #endregion

        public string Render(string mainFilePath, string[] secondaryFilePaths)
        {
            var mainOutput = ParseMainFile(mainFilePath);
            var secondaryOutputs = secondaryFilePaths.Select(ParseSecondaryFile);

            return string.Empty;
        }
    }
}
