using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.MixinDeclaration;
using ViewEngine.Core.Grammar.Using;

namespace ViewEngine.Core.Grammar.Outputs
{
    public class HelperOutput
    {
        public List<MixinDeclarationExpression> Mixins { get; set; }
        public List<UsingExpression> Usings { get; set; }

        public HelperOutput(List<MixinDeclarationExpression> mixins,
            List<UsingExpression> usings)
        {
            Mixins = mixins;
            Usings = usings;
        }
    }
}
