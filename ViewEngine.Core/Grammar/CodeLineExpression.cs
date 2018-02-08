namespace ViewEngine.Core.Grammar
{
    public class CodeLineExpression : IExpression
    {
        public string CodeLine { get; set; }

        public CodeLineExpression(string codeLine)
        {
            CodeLine = codeLine;
        }
    }
}
