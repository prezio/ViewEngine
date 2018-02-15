using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar
{
    public class TemplateScopeVisitor : TemplateScopeBaseVisitor<object>
    {
        public List<ITemplateExpression> Result { get; } = new List<ITemplateExpression>();

        private TemplateLineExpression ParseLineToTemplateLineExpression(string content)
        {
            var inputStream = new AntlrInputStream(content);
            var templateTextLineLexer = new TemplateTextLineLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(templateTextLineLexer);
            var templateTextLineParser = new TemplateTextLineParser(commonTokenStream);

            var templateContext = templateTextLineParser.template_text_line();
            var visitor = new TemplateTextLineVisitor();

            visitor.Visit(templateContext);
            return new TemplateLineExpression(visitor.Result);
        }

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

        public override object VisitTemplateLineExp(TemplateScopeParser.TemplateLineExpContext context)
        {
            var templateLine = context.TEMPLATE_LINE().GetText();

            Result.Add(ParseLineToTemplateLineExpression(templateLine));

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
