using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Instructions;
using ViewEngine.Core.Grammar.MixinDeclaration;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.Assignment;
using ViewEngine.Core.Templates.MethodDefinition;
using ViewEngine.Core.Templates.Model;
using ViewEngine.Core.Templates.StringWrite;
using ViewEngine.Core.Templates.Usage;

namespace ViewEngine.Core.Templates.Scope
{
    public class TemplateScopeManager
    {
        private readonly TemplateWriteManager _writeManager;
        private readonly TemplateVariableAssignmentManager _assignmentManager;
        private readonly TemplateMethodDefinitionManager _methodDefinitionManager;
        private readonly TemplateUsageManager _usageManager;

        public string GenerateMixinDeclaration(MixinDeclarationExpression exp)
        {
            var funcBody = string.Empty;
            if (exp.MixinBody is RegularScope regularScope)
            {
                funcBody = GenerateRegularScope(regularScope);
            }
            else if (exp.MixinBody is TemplateScope templateScope)
            {
                funcBody = GenerateTemplateScope(templateScope);
            }

            return _methodDefinitionManager.GenerateMethodDefinition(
                    exp.MixinName, string.Join(",",
                        exp.Parameters.Select(_methodDefinitionManager.GenerateParamDecl)), 
                    funcBody
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
                return _writeManager.GenerateTextWrite(textString.Text);
            }
            if (varContent is Variable variable)
            {
                return _usageManager.GenerateVariableUsage(variable.Name);
            }
            if (varContent is CodeVarUsage codeUsage)
            {
                return _writeManager.GenerateCodeWrite(codeUsage.VarUsageString);
            }

            return string.Empty;
        }

        public string GenerateMixinUsage(MixinUsageExpression exp)
        {
            var generatedParams = exp.VariableContents.Select(GenerateVarContent)
                .Select(_assignmentManager.GenerateParamContent);
            var assignmentString = string.Join(",", generatedParams);
            return _usageManager.GenerateMethodUsage(assignmentString,
                exp.MixinName);
        }

        public string GenerateTemplateLine(TemplateLineExpression templateLine)
        {
            var ret = new StringBuilder();
            foreach (var part in templateLine.Parts)
            {
                if (part is TemplateRawText rawText)
                {
                    ret.Append(
                        _writeManager.GenerateTextWrite(rawText.RawText)
                        );
                }
                else if (part is TemplateVarUsage templateUsage)
                {
                    ret.Append(
                        _usageManager.GenerateVariableUsage(templateUsage.VarUsageString)
                        );
                }
                else if (part is CodeVarUsage codeUsage)
                {
                    ret.Append(
                        _writeManager.GenerateCodeWrite(codeUsage.VarUsageString)
                        );
                }
            }
            ret.AppendLine(_writeManager.GenerateFormatStringWrite("\\r\\n"));
            return ret.ToString();
        }

        public string GenerateRegularScope(RegularScope scope)
        {
            var ret = new StringBuilder();
            foreach (var expression in scope.Result)
            {
                if (expression is CodeLineExpression codeLine)
                {
                    ret.Append(codeLine.CodeLine);
                }
                else if (expression is MixinUsageExpression mixinUsage)
                {
                    ret.Append(GenerateMixinUsage(mixinUsage));
                }
                else if (expression is VariableAssignmentExpression varAssign)
                {
                    ret.Append(_assignmentManager.GenerateVariableAssignment(varAssign.VarName,
                    GenerateVarContent(varAssign.Content)));
                }
                else if (expression is EvaluateExpression evalVar)
                {

                }
                else if (expression is TemplateScope templateScope)
                {
                    ret.Append(GenerateTemplateScope(templateScope));
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
                    ret.Append(codeLine.CodeLine);
                }
                else if (expression is TemplateLineExpression templateLine)
                {
                    ret.Append(
                        GenerateTemplateLine(templateLine));
                }
            }
            return ret.ToString();
        }

        public TemplateScopeManager(
            TemplateWriteManager writeManager,
            TemplateVariableAssignmentManager assignmentManager,
            TemplateMethodDefinitionManager methodDefinitionManager,
            TemplateUsageManager methodUsageManager)
        {
            _writeManager = writeManager;
            _assignmentManager = assignmentManager;
            _methodDefinitionManager = methodDefinitionManager;
            _usageManager = methodUsageManager;
        }
    }
}
