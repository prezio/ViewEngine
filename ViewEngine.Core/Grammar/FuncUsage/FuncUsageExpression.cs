using System.Collections.Generic;

namespace ViewEngine.Core.Grammar.FuncUsage
{
    public class FuncUsageExpression : IExpression
    {
        public string FunctionName { get; set; }
        public List<IFuncUsageParam> InvokeParameters { get; set; }

        public FuncUsageExpression(string functionName,
            List<IFuncUsageParam> invokeParameters)
        {
            FunctionName = functionName;
            InvokeParameters = invokeParameters;
        }
    }
}
