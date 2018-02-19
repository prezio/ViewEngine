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
        public List<ModelIntroduceExpression> Models { get; set; }
        public List<MixinDeclarationExpression> Mixins { get; set; }

        public MainOutput(RegularScope regularScope,
            List<ModelIntroduceExpression> models,
            List<MixinDeclarationExpression> mixins)
        {
            RegularScope = regularScope;
            Models = models;
            Mixins = mixins;
        }
    }
}
