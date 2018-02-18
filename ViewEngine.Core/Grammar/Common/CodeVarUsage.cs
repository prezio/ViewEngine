using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Common
{
    public class CodeVarUsage : ITemplateLinePart, IVarContent
    {
        public string VarUsageString { get; set; }

        public CodeVarUsage(string varUsage)
        {
            VarUsageString = varUsage;
        }
    }
}
