using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.FuncDeclaration;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.Scope;

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
