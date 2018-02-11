using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar
{
    public class TemplateScopeVisitor : TemplateScopeBaseVisitor<object>
    {
        public List<ITemplateExpression> Result { get; } = new List<ITemplateExpression>();

        #region Template statements expressions
        public override object VisitCodeLineExp(TemplateScopeParser.CodeLineExpContext context)
        {
            var codeLine = context.CODE_LINE().GetText().Remove(0, 2);
            Result.Add(new CodeLineExpression(codeLine));

            var nextStatements = context.template_statement();
            if (nextStatements != null)
            {
                Visit(nextStatements);
            }
            return null;
        }

        public override object VisitRawLineExp(TemplateScopeParser.RawLineExpContext context)
        {
            var templateLine = context.RAW_TEXT_LINE().GetText();
            Result.Add(new TemplateLineExpression(templateLine));

            var nextStatements = context.template_statement();
            if (nextStatements != null)
            {
                Visit(nextStatements);
            }
            return null;
        }
        #endregion
    }
}
