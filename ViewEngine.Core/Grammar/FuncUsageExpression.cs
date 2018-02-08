namespace ViewEngine.Core.Grammar
{
    public class FuncUsageExpression : IExpression
    {
        string FunctionName { get; set; }
        string InvokeParameters { get; set; }

        public FuncUsageExpression(string functionName,
            string invokeParameters)
        {
            FunctionName = functionName;
            InvokeParameters = invokeParameters;
        }
    }
}
