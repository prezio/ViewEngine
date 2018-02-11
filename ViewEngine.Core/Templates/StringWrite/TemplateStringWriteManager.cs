using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.StringWrite
{
    public class TemplateStringWriteManager
    {
        public string GenerateTextAddition(string text)
        {
            var template = new StringWriteTemplate
            {
                TextString = text
            };

            return template.TransformText();
        }
    }
}
