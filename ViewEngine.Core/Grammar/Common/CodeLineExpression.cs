using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Common
{
    public class CodeLineExpression : IRegularExpression, ITemplateExpression
    {
        public string CodeLine { get; set; }

        public CodeLineExpression(string codeLine)
        {
            CodeLine = codeLine;
        }
    }
}
