using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.FuncDeclaration
{
    public class FuncDeclBodyCodeLine : IFuncDeclBodyLine
    {
        public string CodeLine { get; set; }

        public FuncDeclBodyCodeLine(string codeLine)
        {
            CodeLine = codeLine;
        }
    }
}
