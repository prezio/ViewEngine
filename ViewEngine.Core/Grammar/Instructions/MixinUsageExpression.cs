using System.Collections.Generic;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Instructions
{
    // This type of expression cant be used in template scope
    public class MixinUsageExpression : IRegularExpression
    {
        public string MixinName { get; set; }
        public List<IVarContent> VariableContents { get; set; }

        public MixinUsageExpression(string mixinName,
            List<IVarContent> variableContents)
        {
            MixinName = mixinName;
            VariableContents = variableContents;
        }
    }
}
