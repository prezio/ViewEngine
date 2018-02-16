using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Scope
{
    public class CodeVarUsage : ITemplateLinePart
    {
        public string VarUsageString { get; set; }

        public CodeVarUsage(string varUsage)
        {
            VarUsageString = varUsage;
        }
    }
}
