using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.MixinDeclaration;

namespace ViewEngine.Core.Grammar.Outputs
{
    public class SecondaryOutput
    {
        public List<MixinDeclarationExpression> Mixins { get; set; }

        public SecondaryOutput(List<MixinDeclarationExpression> mixins)
        {
            Mixins = mixins;
        }
    }
}
