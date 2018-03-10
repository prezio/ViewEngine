using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Model
{
    public class ModelIntroduceExpression
    {
        public string VarType { get; set; }

        public ModelIntroduceExpression(string varType)
        {
            VarType = varType;
        }
    }
}
