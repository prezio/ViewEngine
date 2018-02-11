using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Common
{
    public class TextString : IVarContent
    {
        public string Text { get; set; }

        public TextString(string text)
        {
            Text = text;
        }
    }
}
