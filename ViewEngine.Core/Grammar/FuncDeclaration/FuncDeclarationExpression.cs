using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.FuncDeclaration
{
    public class FuncDeclarationExpression : IRegularExpression
    {
        public string FunctionName { get; set; }
        public IFunctionBody FunctionBody { get; set; }

        public FuncDeclarationExpression(string functionName,
            IFunctionBody functionBody)
        {
            FunctionName = functionName;
            FunctionBody = functionBody;
        }
    }
}
