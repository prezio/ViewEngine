using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.MixinDeclaration
{
    public class MixinDeclarationExpression
    {
        public string MixinName { get; set; }
        public IMixinBody MixinBody { get; set; }

        public MixinDeclarationExpression(string functionName,
            IMixinBody functionBody)
        {
            MixinName = functionName;
            MixinBody = functionBody;
        }
    }
}
