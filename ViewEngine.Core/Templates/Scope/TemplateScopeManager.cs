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
using ViewEngine.Core.Templates.Usage;

namespace ViewEngine.Core.Templates.Scope
{
    public class TemplateScopeManager
    {
        private readonly TemplateStringWriteManager _stringWriteManager;
        private readonly TemplateVariableAssignmentManager _assignmentManager;
        private readonly TemplateMethodDefinitionManager _methodDefinitionManager;
        private readonly TemplateUsageManager _usageManager;
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

            return _methodDefinitionManager.GenerateMethodDefinition(
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
                assignments.Append(_assignmentManager.GenerateVariableAssignment(varAssign.Key,
                    GenerateVarContent(varAssign.Value, modelNames)));
            }
            return _usageManager.GenerateMethodUsage(assignments.ToString(),
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
                    ret.Append(
                        _stringWriteManager.GenerateTextAddition(rawText.RawText)
                        );
                }
                if (part is TemplateVarUsage varUsage)
                {
                    var varName = varUsage.VarUsageString.Split('.').First().Trim();
                    if (modelNames.Contains(varName))
                    {
                        ret.Append(
                            _variableWriteManager.GenerateVarAddition(varUsage.VarUsageString)
                            );
                    }
                    else
                    {
                        ret.Append(
                            _usageManager.GenerateParameterUsage(varName)
                            );
                    }
                }
            }
            ret.AppendLine(_stringWriteManager.GenerateTextAddition("\\r\\n"));
            return ret.ToString();
        }

        public string GenerateRegularScope(RegularScope scope, string[] modelNames)
        {
            var ret = new StringBuilder();
            foreach (var expression in scope.Result)
            {
                if (expression is CodeLineExpression codeLine)
                {
                    ret.Append(codeLine.CodeLine);
                }
                else if (expression is FuncUsageExpression funcUsage)
                {
                    ret.Append(GenerateFuncUsage(funcUsage, modelNames));
                }
                else if (expression is FuncDeclarationExpression funcDeclaration)
                {
                    ret.Append(GenerateFuncDeclaration(funcDeclaration, modelNames));
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
                    ret.Append(codeLine.CodeLine);
                }
                else if (expression is TemplateLineExpression templateLine)
                {
                    ret.Append(
                        GenerateTemplateLine(templateLine, modelNames));
                }
            }
            return ret.ToString();
        }

        public TemplateScopeManager(
            TemplateStringWriteManager stringWriteManager,
            TemplateVariableAssignmentManager assignmentManager,
            TemplateMethodDefinitionManager methodDefinitionManager,
            TemplateUsageManager methodUsageManager,
            TemplateVariableWriteManager variableWriteManager)
        {
            _stringWriteManager = stringWriteManager;
            _assignmentManager = assignmentManager;
            _methodDefinitionManager = methodDefinitionManager;
            _usageManager = methodUsageManager;
            _variableWriteManager = variableWriteManager;
        }
    }
}
