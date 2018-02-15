using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Scope
{
    public class TemplateLineExpression : ITemplateExpression
    {
        public List<ITemplateLinePart> Parts { get; set; }

        public TemplateLineExpression(List<ITemplateLinePart> parts)
        {
            Parts = parts;
        }
    }
}
