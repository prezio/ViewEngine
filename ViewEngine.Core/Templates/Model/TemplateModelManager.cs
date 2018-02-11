using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Model;

namespace ViewEngine.Core.Templates.Model
{
    public class TemplateModelManager
    {
        public string GenerateModelParams(List<ModelIntroduceExpression> modelExpressions)
        {
            return string.Join(",",
                modelExpressions.Select(model => $"{model.VarType} {model.VarName}"));
        }
    }
}
