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
        public List<IRegularExpression> Expressions { get; set; }

        public SecondaryOutput(List<IRegularExpression> expressions)
        {
            Expressions = expressions;
        }
    }
}
