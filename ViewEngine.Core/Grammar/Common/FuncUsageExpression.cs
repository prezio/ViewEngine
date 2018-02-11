using System.Collections.Generic;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Common
{
    // This type of expression cant be used in template scope
    public class FuncUsageExpression : IRegularExpression
    {
        public string FunctionName { get; set; }
        public Dictionary<string, IVarContent> InvokeParameters { get; set; }

        public FuncUsageExpression(string functionName,
            Dictionary<string, IVarContent> invokeParameters)
        {
            FunctionName = functionName;
            InvokeParameters = invokeParameters;
        }
    }
}
