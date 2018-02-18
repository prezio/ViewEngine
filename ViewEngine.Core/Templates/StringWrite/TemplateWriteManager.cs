using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.StringWrite
{
    public class TemplateWriteManager
    {
        public string GenerateTextWrite(string text)
        {
            var template = new StringWriteTemplate
            {
                TextString = text
            };

            return template.TransformText();
        }

        public string GenerateCodeWrite(string codeText)
        {
            var template = new CodeWriteTemplate
            {
                CodeText = codeText
            };

            return template.TransformText();
        }
    }
}
