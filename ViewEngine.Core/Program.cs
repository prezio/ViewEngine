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
            string input = "pompka(\"frecel\",@pompka);www();kappa(@zet);";
            AntlrInputStream inputStream = new AntlrInputStream(input);
            ViewEngineLexer viewEngineLexer = new ViewEngineLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(viewEngineLexer);
            ViewEngineParser viewEngineParser = new ViewEngineParser(commonTokenStream);

            ViewEngineParser.StatementContext statementContext = viewEngineParser.statement();
            ViewEngineVisitor visitor = new ViewEngineVisitor();

            Console.WriteLine(visitor.Visit(statementContext));
        }
    }
}
