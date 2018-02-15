using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Scope
{
    public class TemplateVarUsage : ITemplateLinePart
    {
        public string VarUsageString { get; set; }

        public TemplateVarUsage(string varUsage)
        {
            VarUsageString = varUsage;
        }
    }
}
