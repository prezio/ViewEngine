using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.StringWrite;

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
