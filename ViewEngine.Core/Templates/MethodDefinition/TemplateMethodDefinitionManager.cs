using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.MethodDefinition
{
    public class TemplateMethodDefinitionManager
    {
        public string GenerateMethodDefinition(string functionName,
            string functionBody)
        {
            var template = new MethodDefinitionTemplate
            {
                MethodName = functionName,
                MethodContent = functionBody
            };
            return template.TransformText();
        }
    }
}
