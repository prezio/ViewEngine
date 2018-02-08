namespace ViewEngine.Core.Grammar.FuncUsage
{
    public class VarFuncUsageParam : IFuncUsageParam
    {
        public string VarName { get; set; }

        public VarFuncUsageParam(string varName)
        {
            VarName = varName;
        }
    }
}
