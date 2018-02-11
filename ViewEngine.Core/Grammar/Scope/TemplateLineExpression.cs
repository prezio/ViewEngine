using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Scope
{
    public class TemplateLineExpression : ITemplateExpression
    {
        public string TemplateLine { get; set; }

        public TemplateLineExpression(string templateLine)
        {
            TemplateLine = templateLine;
        }
    }
}
