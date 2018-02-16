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
                VariableAssignments = varAssignments,
                MethodName = methodName
            };

            return template.TransformText();
        }

        public string GenerateParameterUsage(string varName)
        {
            var template = new ParameterUsageTemplate
            {
                VarName = varName
            };

            return template.TransformText();
        }

        public string GenerateCodeVarUsage(string varText)
        {
            var template = new CodeVarUsageTemplate
            {
                VarText = varText
            };

            return template.TransformText();
        }
    }
}
