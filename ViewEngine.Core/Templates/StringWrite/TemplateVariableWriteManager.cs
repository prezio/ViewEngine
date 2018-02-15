using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.StringWrite
{
    public class TemplateVariableWriteManager
    {
        public string GenerateVarAddition(string text)
        {
            var template = new VariableWriteTemplate
            {
               VarText = text
            };

            return template.TransformText();
        }
    }
}
