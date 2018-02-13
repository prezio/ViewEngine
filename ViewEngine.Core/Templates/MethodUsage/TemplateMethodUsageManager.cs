using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.MethodUsage
{
    public class TemplateMethodUsageManager
    {
        public string GenerateMethodUsage(string varAssignments, 
            string methodName)
        {
            var template = new MethodUsageTemplate
            {
                VariableAssignments = varAssignments,
                MethodName = methodName
            };
            return template.TransformText();
        }
    }
}
