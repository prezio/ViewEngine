using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.Assignment
{
    public class TemplateVariableAssignmentManager
    {
        public string GenerateVariableAssignment(string variableName,
            string varContent)
        {
            var template = new VariableAssignmentTemplate
            {
                VarName = variableName,
                Content = varContent
            };
            return template.TransformText();
        }
    }
}
