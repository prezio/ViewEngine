using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.FuncDeclaration;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.Assignment;
using ViewEngine.Core.Templates.MethodDefinition;
using ViewEngine.Core.Templates.StringWrite;

namespace ViewEngine.Core.Templates.Scope
{
    public class TemplateScopeManager
    {
        private readonly TemplateStringWriteManager _stringWriteManager;
        private readonly TemplateVariableAssignmentManager _assignmentManager;
        private readonly TemplateMethodDefinitionManager _methodDefinitionManager;

        public string GenerateFuncDeclaration(FuncDeclarationExpression exp)
        {
            var funcBody = string.Empty;
            if (exp.FunctionBody is RegularScope regularScope)
            {
                funcBody = GenerateRegularScope(regularScope);
            }
            else if (exp.FunctionBody is TemplateScope templateScope)
            {
                funcBody = GenerateTemplateScope(templateScope);
            }

            return _methodDefinitionManager.GenerateLambdaMethodDefinition(
                    exp.FunctionName, funcBody
                );
        }

        public string GenerateRegularScope(RegularScope scope)
        {
            var ret = new StringBuilder();
            foreach (var expression in scope.Result)
            {
                if (expression is CodeLineExpression codeLine)
                {
                    ret.AppendLine(codeLine.CodeLine);
                }
                else if (expression is FuncUsageExpression funcUsage)
                {
                    
                }
                else if (expression is FuncDeclarationExpression funcDeclaration)
                {
                    ret.AppendLine(GenerateFuncDeclaration(funcDeclaration));
                }
            }
            return ret.ToString();
        }

        public string GenerateTemplateScope(TemplateScope scope)
        {
            var ret = new StringBuilder();
            foreach (var expression in scope.Result)
            {
                if (expression is CodeLineExpression codeLine)
                {
                    ret.AppendLine(codeLine.CodeLine);
                }
                else if (expression is TemplateLineExpression templateLine)
                {
                    ret.AppendLine(
                        _stringWriteManager.GenerateTextAddition(
                            templateLine.TemplateLine));
                }
            }
            return ret.ToString();
        }

        public TemplateScopeManager(
            TemplateStringWriteManager stringWriteManager,
            TemplateVariableAssignmentManager assignmentManager,
            TemplateMethodDefinitionManager methodDefinitionManager)
        {
            _stringWriteManager = stringWriteManager;
            _assignmentManager = assignmentManager;
            _methodDefinitionManager = methodDefinitionManager;
        }
    }
}
