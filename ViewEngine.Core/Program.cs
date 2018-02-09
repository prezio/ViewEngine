using Antlr4.Runtime;
using ViewEngine.Core.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core
{
    class Program
    {
        public static void Main()
        {
            var reader = new System.IO.StreamReader(@"Samples\FuncBodyWithUsage.view");

            AntlrInputStream inputStream = new AntlrInputStream(reader);
            ViewEngineLexer viewEngineLexer = new ViewEngineLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(viewEngineLexer);
            ViewEngineParser viewEngineParser = new ViewEngineParser(commonTokenStream);

            ViewEngineParser.StatementContext statementContext = viewEngineParser.statement();
            ViewEngineVisitor visitor = new ViewEngineVisitor();

            Console.WriteLine(visitor.Visit(statementContext));
        }
    }
}
