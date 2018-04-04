using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Instructions
{
    public class EvaluateExpression : IRegularExpression
    {
        public string VarName { get; set; }

        public EvaluateExpression(string varName)
        {
            VarName = varName;
        }
    }
}
