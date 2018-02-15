using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Scope
{
    public class TemplateRawText : ITemplateLinePart
    {
        public string RawText { get; set; }

        public TemplateRawText(string rawText)
        {
            RawText = rawText;
        }
    }
}
