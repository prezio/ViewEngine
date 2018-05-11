using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Scope
{
    public class CodeScope : IRegularExpression, ITemplateExpression
    {
        public string Content { get; set; }

        public CodeScope(string content)
        {
            Content = content;
        }
    }
}
