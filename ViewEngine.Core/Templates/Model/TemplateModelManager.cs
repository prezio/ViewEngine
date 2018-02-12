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

        public string GenerateModelAssignments(List<ModelIntroduceExpression> modelExpressions)
        {
            return string.Join(";\n",
                modelExpressions.Select(model => $"this.{model.VarName} = {model.VarName};"));
        }

        public string GenerateModelDeclarations(List<ModelIntroduceExpression> modelExpressions)
        {
            return string.Join(";\n",
                modelExpressions.Select(model => $"{model.VarType} {model.VarName};"));
        }

        public string GenerateModelPassed(List<ModelIntroduceExpression> modelExpressions)
        {
            return string.Join(",",
                modelExpressions.Select(model => model.VarName));
        }
    }
}
