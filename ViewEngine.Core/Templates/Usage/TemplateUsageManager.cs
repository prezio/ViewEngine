using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.Usage
{
    public class TemplateUsageManager
    {
        public string GenerateMethodUsage(string varAssignments, 
            string methodName)
        {
            var template = new MethodUsageTemplate
            {
                VariableAssignments = 
                    string.IsNullOrEmpty(varAssignments) ? string.Empty : $",{varAssignments}",
                MethodName = methodName
            };

            return template.TransformText();
        }

        public string GenerateVariableUsage(string varName)
        {
            return $"{varName}();";
        }

        public string GenerateUsingNamespace(string namespaceName)
        {
            var template = new UsingNamespaceTemplate
            {
                NamespaceName = namespaceName
            };

            return template.TransformText();
        }
    }
}
