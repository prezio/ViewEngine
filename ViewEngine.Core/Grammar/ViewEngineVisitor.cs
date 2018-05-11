using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ViewEngine.Core.Engine;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.MixinDeclaration;
using ViewEngine.Core.Grammar.Model;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Grammar.Using;
using ViewEngine.Core.Grammar.Instructions;

namespace ViewEngine.Core.Grammar
{
    public class ViewEngineVisitor : ViewEngineBaseVisitor<object>
    {
        public List<IRegularExpression> Result { get; } = new List<IRegularExpression>();
        public ModelIntroduceExpression Model { get; set; }
        public List<MixinDeclarationExpression> Mixins { get; } = new List<MixinDeclarationExpression>();
        public List<UsingExpression> Usings { get; } = new List<UsingExpression>();

        private static bool CheckIfTerminalIsCorrect(ITerminalNode node)
        {
            return node != null && node.Symbol.StartIndex != -1;
        }

        #region Conversion methods
        private TemplateScope ParseStringToTemplateScope(string content)
        {
            var inputStream = new AntlrInputStream(content);
            var templateScopeLexer = new TemplateScopeLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(templateScopeLexer);
            var templateScopeParser = new TemplateScopeParser(commonTokenStream);

            var templateContext = templateScopeParser.template_scope();
            var visitor = new TemplateScopeVisitor();

            visitor.Visit(templateContext);
            return new TemplateScope(visitor.Result);
        }

        private RegularScope ParseStringToRegularScope(string content)
        {
            var inputStream = new AntlrInputStream(content);
            var viewEngineLexer = new ViewEngineLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(viewEngineLexer);
            var viewEngineParser = new ViewEngineParser(commonTokenStream);

            var statementContext = viewEngineParser.regular_statement();
            var visitor = new ViewEngineVisitor();

            visitor.Visit(statementContext);
            return new RegularScope(visitor.Result);
        }
        #endregion

        #region Regular Statement Expression visitors
        public override object VisitMixinUsageExp([NotNull] ViewEngineParser.MixinUsageExpContext context)
        {
            var funcUsage = (IRegularExpression)Visit(context.mixin_usage());
            Result.Add(funcUsage);

            Visit(context.regular_statement());
            return null;
        }

        public override object VisitCodeScopeExp([NotNull] ViewEngineParser.CodeScopeExpContext context)
        {
            var content = context.CODE_SCOPE().GetText();
            Result.Add(
                new CodeScope(content.Substring(2, content.Length - 3))
                );

            Visit(context.regular_statement());
            return null;
        }

        public override object VisitCodeLineExp([NotNull] ViewEngineParser.CodeLineExpContext context)
        {
            var codeLine = context.CODE_LINE().GetText().Remove(0, 2);
            Result.Add(new CodeLineExpression(codeLine));

            Visit(context.regular_statement());
            return null;
        }

        public override object VisitTemplateScopeExp(ViewEngineParser.TemplateScopeExpContext context)
        {
            var templateScope = context.TEMPLATE_SCOPE().GetText();
            Result.Add(ParseStringToTemplateScope(templateScope.Substring(2, templateScope.Length - 4)));

            Visit(context.regular_statement());
            return null;
        }
        #endregion

        #region Model Introduce Section
        public override object VisitModel_introduction([NotNull] ViewEngineParser.Model_introductionContext context)
        {
            var model = context.MODEL();
            if (!CheckIfTerminalIsCorrect(model))
            {
                return null;
            }

            var varType = context.CODE_SCOPE();
            if (!CheckIfTerminalIsCorrect(varType))
            {
                throw new RenderException("No type for model was provided.");
            }
            
            var content = varType.GetText();
            var modelType = content.Substring(2, content.Length - 3);

            if (Model != null)
            {
                throw new RenderException("Only one model per renderer can be defined.");
            }

            Model = new ModelIntroduceExpression(modelType);
            return null;
        }
        #endregion

        #region Using Expression
        public override object VisitUsing_namespace(ViewEngineParser.Using_namespaceContext context)
        {
            var usingId = context.CODE_SCOPE();
            if (usingId != null)
            {
                var content = usingId.GetText();
                Usings.Add(new UsingExpression(usingId.GetText().Substring(2, content.Length - 3)));
            }
            return null;
        }
        #endregion

        #region Assignment Expression
        public override object VisitAssignment_exp([NotNull] ViewEngineParser.Assignment_expContext context)
        {
            var ids = context.ID();
            var varName = ids.First().GetText();

            if (ids.Length == 2)
            {
                Result.Add(new VariableAssignmentExpression(varName,
                    new Variable(ids[1].GetText())));
                return null;
            }

            var templateScope = context.TEMPLATE_SCOPE();
            if (templateScope != null)
            {
                var content = templateScope.GetText();
                Result.Add(new VariableAssignmentExpression(varName,
                    ParseStringToTemplateScope(content.Substring(2, content.Length - 4))));
                return null;
            }

            var regularScope = context.REGULAR_SCOPE();
            if (regularScope != null)
            {
                var content = regularScope.GetText();
                Result.Add(new VariableAssignmentExpression(varName,
                    ParseStringToRegularScope(content.Substring(2, content.Length - 4))));
                return null;
            }

            var textString = context.TEXT_STRING();
            if (textString != null)
            {
                Result.Add(new VariableAssignmentExpression(varName,
                    new TextString(textString.GetText().Trim('"'))));
                return null;
            }

            var codeString = context.CODE_SCOPE();
            if (codeString != null)
            {
                var content = codeString.GetText();
                Result.Add(new VariableAssignmentExpression(varName,
                    new CodeVarUsage(content.Substring(2, content.Length - 3))));
            }

            return null;
        }
        #endregion

        #region Evaluate Expression
        public override object VisitEval_exp([NotNull] ViewEngineParser.Eval_expContext context)
        {
            var variable = context.ID();
            if (!CheckIfTerminalIsCorrect(variable))
            {
                throw new RenderException("You need to put variable in 'eval' statement");
            }
            Result.Add(new EvaluateExpression(variable.GetText()));

            return null;
        }
        #endregion

        #region Mixin Usage Expression
        public override object VisitMixin_usage([NotNull] ViewEngineParser.Mixin_usageContext context)
        {
            var functionName = context.ID().GetText();
            var usageArgs = (List<IVarContent>)Visit(context.mixin_usage_args());

            return new MixinUsageExpression(functionName, usageArgs);
        }

        public override object VisitMixin_usage_args([NotNull] ViewEngineParser.Mixin_usage_argsContext context)
        {
            var expArgs = context.mixin_usage_args2();
            return expArgs != null ? ((LinkedList<IVarContent>)Visit(expArgs)).ToList() : new List<IVarContent>();
        }

        public override object VisitMixin_usage_args2([NotNull] ViewEngineParser.Mixin_usage_args2Context context)
        {
            var newParam = (IVarContent) Visit(context.mixin_usage_param());

            var nextArgsExp = context.mixin_usage_args2();
            var nextArgs = nextArgsExp != null ?
                (LinkedList<IVarContent>)Visit(nextArgsExp) 
                : new LinkedList<IVarContent>();

            nextArgs.AddFirst(newParam);
            return nextArgs;
        }

        public override object VisitMixin_usage_param([NotNull] ViewEngineParser.Mixin_usage_paramContext context)
        {
            var id = context.ID();

            if (CheckIfTerminalIsCorrect(id))
            {
                return new Variable(id.GetText());
            }

            var templateScope = context.TEMPLATE_SCOPE();
            if (CheckIfTerminalIsCorrect(templateScope))
            {
                var content = templateScope.GetText();
                return ParseStringToTemplateScope(content.Substring(2, content.Length - 4));
            }

            var regularScope = context.REGULAR_SCOPE();
            if (CheckIfTerminalIsCorrect(regularScope))
            {
                var content = regularScope.GetText();
                return ParseStringToRegularScope(content.Substring(2, content.Length - 4));
            }

            var textString = context.TEXT_STRING();
            if (CheckIfTerminalIsCorrect(textString))
            {
                return new TextString(textString.GetText().Trim('"'));
            }

            var codeString = context.CODE_SCOPE();
            if (CheckIfTerminalIsCorrect(codeString))
            {
                var content = codeString.GetText();
                return new CodeVarUsage(content.Substring(2, content.Length - 3));
            }

            return null;
        }
        #endregion

        #region Mixin declaration Expression
        public override object VisitMixin_declaration(ViewEngineParser.Mixin_declarationContext context)
        {
            var mixinName = context.ID();
            if (!CheckIfTerminalIsCorrect(mixinName))
            {
                throw new RenderException("There is no name for mixin declaration.");
            }

            var bodyExp = (IMixinBody)Visit(context.mixin_body());
            var mixinParams = (List<MixinDeclarationParam>) Visit(context.mixin_declaration_params());

            Mixins.Add(new MixinDeclarationExpression(mixinName.GetText(),
                mixinParams,
                bodyExp));
            return null;
        }

        public override object VisitMixin_declaration_params([NotNull] ViewEngineParser.Mixin_declaration_paramsContext context)
        {
            var expArgs = context.mixin_declaration_params2();
            return expArgs != null ? ((LinkedList<MixinDeclarationParam>)Visit(expArgs)).ToList() : new List<MixinDeclarationParam>();
        }

        public override object VisitMixin_declaration_params2([NotNull] ViewEngineParser.Mixin_declaration_params2Context context)
        {
            var parameterName = (MixinDeclarationParam)Visit(context.mixin_declaration_param());
            
            var nextArgsExp = context.mixin_declaration_params2();
            var nextArgs = nextArgsExp != null ?
                (LinkedList<MixinDeclarationParam>)Visit(nextArgsExp)
                : new LinkedList<MixinDeclarationParam>();

            nextArgs.AddFirst(parameterName);
            return nextArgs;
        }

        public override object VisitMixin_declaration_param([NotNull] ViewEngineParser.Mixin_declaration_paramContext context)
        {
            var paramId = context.ID();
            if (CheckIfTerminalIsCorrect(paramId))
            {
                return new MixinDeclarationParam(
                    MixinDeclarationParamType.MixinScope,
                    paramId.GetText()
                    );
            }

            var paramCodeScope = context.CODE_SCOPE();
            if (CheckIfTerminalIsCorrect(paramCodeScope))
            {
                var content = paramCodeScope.GetText();
                return new MixinDeclarationParam(
                    MixinDeclarationParamType.CodeScope,
                    content.Substring(2, content.Length - 3)
                    );
            }
            
            throw new RenderException("Unrecognized parameter");
        }

        public override object VisitMixin_body([NotNull] ViewEngineParser.Mixin_bodyContext context)
        {
            var templateScope = context.TEMPLATE_SCOPE();
            if (templateScope != null)
            {
                var templateString = templateScope.GetText();
                templateString = templateString.Substring(2, templateString.Length - 4);
                return ParseStringToTemplateScope(templateString);
            }

            var regularScope = context.REGULAR_SCOPE();
            if (regularScope != null)
            {
                var regularString = regularScope.GetText();
                regularString = regularString.Substring(2, regularString.Length - 4);
                return ParseStringToRegularScope(regularString);
            }

            return null;
        }
        #endregion
    }
}