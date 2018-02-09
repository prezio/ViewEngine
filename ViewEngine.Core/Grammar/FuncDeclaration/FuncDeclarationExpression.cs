using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.FuncDeclaration
{
    public class FuncDeclarationExpression : IExpression
    {
        public string FunctionName { get; set; }
        public List<IFuncDeclParam> DeclarationParameters { get; set; }

        public FuncDeclarationExpression(string functionName,
            List<IFuncDeclParam> declarationParameters)
        {
            FunctionName = functionName;
            DeclarationParameters = declarationParameters;
        }
    }
}
