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
using ViewEngine.Core.Templates.MethodUsage;
using ViewEngine.Core.Templates.StringWrite;

namespace ViewEngine.Core.Templates.Scope
{
    public class TemplateScopeManager
    {
        private readonly TemplateStringWriteManager _stringWriteManager;
        private readonly TemplateVariableAssignmentManager _assignmentManager;
        private readonly TemplateMethodDefinitionManager _methodDefinitionManager;
        private readonly TemplateMethodUsageManager _methodUsageManager;

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

        public string GenerateVarContent(IVarContent varContent)
        {
            if (varContent is RegularScope regularScope)
            {
                return GenerateRegularScope(regularScope);
            }
            if (varContent is TemplateScope templateScope)
            {
                return GenerateTemplateScope(templateScope);
            }
            if (varContent is TextString textString)
            {
                return _stringWriteManager.GenerateTextAddition(textString.Text);
            }
            return string.Empty;
        }

        public string GenerateFuncUsage(FuncUsageExpression exp)
        {
            var assignments = new StringBuilder();
            foreach (var varAssign in exp.VariableAssignments)
            {
                assignments.AppendLine(_assignmentManager.GenerateVariableAssignment(varAssign.Key,
                    GenerateVarContent(varAssign.Value)));
            }
            return _methodUsageManager.GenerateMethodUsage(assignments.ToString(),
                exp.FunctionName);
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
                    ret.AppendLine(GenerateFuncUsage(funcUsage));
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
            TemplateMethodDefinitionManager methodDefinitionManager,
            TemplateMethodUsageManager methodUsageManager)
        {
            _stringWriteManager = stringWriteManager;
            _assignmentManager = assignmentManager;
            _methodDefinitionManager = methodDefinitionManager;
            _methodUsageManager = methodUsageManager;
        }
    }
}
