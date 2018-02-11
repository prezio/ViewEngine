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
        public List<string> DeclarationParameters { get; set; }
        public IFunctionBody FunctionBody { get; set; }

        public FuncDeclarationExpression(string functionName,
            List<string> declarationParameters,
            IFunctionBody functionBody)
        {
            FunctionName = functionName;
            DeclarationParameters = declarationParameters;
            FunctionBody = functionBody;
        }
    }
}
