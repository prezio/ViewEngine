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
        private readonly TemplateVariableWriteManager _variableWriteManager;

        public string GenerateFuncDeclaration(FuncDeclarationExpression exp, string[] modelNames)
        {
            var funcBody = string.Empty;
            if (exp.FunctionBody is RegularScope regularScope)
            {
                funcBody = GenerateRegularScope(regularScope, modelNames);
            }
            else if (exp.FunctionBody is TemplateScope templateScope)
            {
                funcBody = GenerateTemplateScope(templateScope, modelNames);
            }

            return _methodDefinitionManager.GenerateLambdaMethodDefinition(
                    exp.FunctionName, funcBody
                );
        }

        public string GenerateVarContent(IVarContent varContent, string[] modelNames)
        {
            if (varContent is RegularScope regularScope)
            {
                return GenerateRegularScope(regularScope, modelNames);
            }
            if (varContent is TemplateScope templateScope)
            {
                return GenerateTemplateScope(templateScope, modelNames);
            }
            if (varContent is TextString textString)
            {
                return _stringWriteManager.GenerateTextAddition(textString.Text);
            }
            return string.Empty;
        }

        public string GenerateFuncUsage(FuncUsageExpression exp, string[] modelNames)
        {
            var assignments = new StringBuilder();
            foreach (var varAssign in exp.VariableAssignments)
            {
                assignments.AppendLine(_assignmentManager.GenerateVariableAssignment(varAssign.Key,
                    GenerateVarContent(varAssign.Value, modelNames)));
            }
            return _methodUsageManager.GenerateMethodUsage(assignments.ToString(),
                exp.FunctionName);
        }

        public string GenerateTemplateLine(TemplateLineExpression templateLine,
                string[] modelNames)
        {
            var ret = new StringBuilder();
            foreach (var part in templateLine.Parts)
            {
                if (part is TemplateRawText rawText)
                {
                    ret.AppendLine(
                        _stringWriteManager.GenerateTextAddition(rawText.RawText)
                        );
                }
                if (part is TemplateVarUsage varUsage)
                {
                    var varName = varUsage.VarUsageString.Split('.').First().Trim();
                    if (modelNames.Contains(varName))
                    {
                        ret.AppendLine(
                            _variableWriteManager.GenerateVarAddition(varUsage.VarUsageString)
                            );
                    }
                    else
                    {
                        ret.AppendLine(
                            $"{varName}();"
                            );
                    }
                }
            }
            return ret.ToString();
        }

        public string GenerateRegularScope(RegularScope scope, string[] modelNames)
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
                    ret.AppendLine(GenerateFuncUsage(funcUsage, modelNames));
                }
                else if (expression is FuncDeclarationExpression funcDeclaration)
                {
                    ret.AppendLine(GenerateFuncDeclaration(funcDeclaration, modelNames));
                }
            }
            return ret.ToString();
        }

        public string GenerateTemplateScope(TemplateScope scope, string[] modelNames)
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
                        GenerateTemplateLine(templateLine, modelNames));
                }
            }
            return ret.ToString();
        }

        public TemplateScopeManager(
            TemplateStringWriteManager stringWriteManager,
            TemplateVariableAssignmentManager assignmentManager,
            TemplateMethodDefinitionManager methodDefinitionManager,
            TemplateMethodUsageManager methodUsageManager,
            TemplateVariableWriteManager variableWriteManager)
        {
            _stringWriteManager = stringWriteManager;
            _assignmentManager = assignmentManager;
            _methodDefinitionManager = methodDefinitionManager;
            _methodUsageManager = methodUsageManager;
            _variableWriteManager = variableWriteManager;
        }
    }
}
