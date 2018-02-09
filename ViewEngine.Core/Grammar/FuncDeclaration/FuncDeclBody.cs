using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.FuncDeclaration
{
    public class FuncDeclBody
    {
        public List<IFuncDeclBodyLine> TextBody { get; set; }

        public FuncDeclBody(List<IFuncDeclBodyLine> textBody)
        {
            TextBody = textBody;
        }
    }
}
