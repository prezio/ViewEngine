using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.Instructions
{
    public class TemplateInstructionsManager
    {
        public string GenerateEvalStatement(string varName)
        {
            return $"{varName}();";
        }
    }
}
