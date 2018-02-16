using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Outputs
{
    public class SecondaryOutput
    {
        public RegularScope RegularScope { get; set; }

        public SecondaryOutput(RegularScope regularScope)
        {
            RegularScope = regularScope;
        }
    }
}
