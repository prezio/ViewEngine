using System.Collections.Generic;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Common
{
    // This type of expression cant be used in template scope
    public class MixinUsageExpression : IRegularExpression
    {
        public string MixinName { get; set; }
        public Dictionary<string, IVarContent> VariableAssignments { get; set; }

        public MixinUsageExpression(string mixinName,
            Dictionary<string, IVarContent> variableAssignments)
        {
            MixinName = mixinName;
            VariableAssignments = variableAssignments;
        }
    }
}
