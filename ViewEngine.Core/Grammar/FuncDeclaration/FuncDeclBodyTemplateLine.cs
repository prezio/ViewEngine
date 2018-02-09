using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.FuncDeclaration
{
    public class FuncDeclBodyTemplateLine : IFuncDeclBodyLine
    {
        public string TemplateLine { get; set; }

        public FuncDeclBodyTemplateLine(string templateLine)
        {
            TemplateLine = templateLine;
        }
    }
}
