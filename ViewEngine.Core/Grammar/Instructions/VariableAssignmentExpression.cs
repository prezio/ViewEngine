using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Instructions
{
    public class VariableAssignmentExpression : IRegularExpression
    {
        public string VarName { get; set; }
        public IVarContent Content { get; set; }

        public VariableAssignmentExpression(string varName,
            IVarContent varContent)
        {
            VarName = varName;
            Content = varContent;
        }
    }
}
