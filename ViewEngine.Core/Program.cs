﻿using Antlr4.Runtime;
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

            var inputStream = new AntlrInputStream(reader);
            var viewEngineLexer = new ViewEngineLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(viewEngineLexer);
            var viewEngineParser = new ViewEngineParser(commonTokenStream);

            var statementContext = viewEngineParser.statement();
            var visitor = new ViewEngineVisitor();

            Console.WriteLine(visitor.Visit(statementContext));
        }
    }
}
