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
            string methodParams,
            string functionBody)
        {
            var template = new MethodDefinitionTemplate
            {
                MethodName = functionName,
                MethodParams = 
                    string.IsNullOrEmpty(methodParams) ? string.Empty : $",{methodParams}",
                MethodContent = functionBody
            };
            return template.TransformText();
        }

        public string GenerateParamDecl(string paramName)
        {
            return $"Action {paramName}";
        }
    }
}
