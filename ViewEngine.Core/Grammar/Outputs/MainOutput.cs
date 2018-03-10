using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.MixinDeclaration;
using ViewEngine.Core.Grammar.Model;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Outputs
{
    public class MainOutput
    {
        public RegularScope RegularScope { get; set; }
        public ModelIntroduceExpression Model { get; set; }
        public List<MixinDeclarationExpression> Mixins { get; set; }

        public MainOutput(RegularScope regularScope,
            ModelIntroduceExpression model,
            List<MixinDeclarationExpression> mixins)
        {
            RegularScope = regularScope;
            Model = model;
            Mixins = mixins;
        }
    }
}
