using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.FuncDeclaration;
using ViewEngine.Core.Grammar.Model;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar
{
    public class ViewEngineVisitor : ViewEngineBaseVisitor<object>
    {
        public List<IRegularExpression> Result { get; } = new List<IRegularExpression>();
        public List<ModelIntroduceExpression> Models { get; } = new List<ModelIntroduceExpression>();

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
        public override object VisitFuncUsageExp([NotNull] ViewEngineParser.FuncUsageExpContext context)
        {
            var funcUsage = (IRegularExpression)Visit(context.func_usage());
            Result.Add(funcUsage);

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

        public override object VisitFuncDeclExp(ViewEngineParser.FuncDeclExpContext context)
        {
            var funcDeclaration = (IRegularExpression) Visit(context.func_declaration());
            Result.Add(funcDeclaration);

            Visit(context.regular_statement());
            return null;
        }
        #endregion

        #region Model Introduce Section
        public override object VisitModel_introduction([NotNull] ViewEngineParser.Model_introductionContext context)
        {
            var varType = context.ID().GetText();
            var varName = context.VARID().GetText().TrimStart('@');

            Models.Add(new ModelIntroduceExpression(varType, varName));
            return null;
        }
        #endregion

        #region Function Usage Expression
        public override object VisitFunc_usage([NotNull] ViewEngineParser.Func_usageContext context)
        {
            var functionName = context.ID().GetText();
            var usageArgs = (Dictionary<string, IVarContent>)Visit(context.func_usage_args());

            return new FuncUsageExpression(functionName, usageArgs);
        }

        public override object VisitFunc_usage_args([NotNull] ViewEngineParser.Func_usage_argsContext context)
        {
            var expArgs = context.func_usage_args2();
            return expArgs != null ? (Dictionary<string, IVarContent>)Visit(expArgs) : new Dictionary<string, IVarContent>();
        }

        public override object VisitFunc_usage_args2([NotNull] ViewEngineParser.Func_usage_args2Context context)
        {
            var newParam = (Tuple<string, IVarContent>) Visit(context.func_usage_param());

            var nextArgsExp = context.func_usage_args2();
            var nextArgs = nextArgsExp != null ?
                (Dictionary<string, IVarContent>)Visit(nextArgsExp) 
                : new Dictionary<string, IVarContent>();

            nextArgs[newParam.Item1] = newParam.Item2;
            return nextArgs;
        }

        public override object VisitFunc_usage_param([NotNull] ViewEngineParser.Func_usage_paramContext context)
        {
            var varName = context.VARID().GetText().TrimStart('@');

            var templateScope = context.TEMPLATE_SCOPE();
            if (templateScope != null)
            {
                var content = templateScope.GetText();
                return new Tuple<string, IVarContent>(varName, 
                    ParseStringToTemplateScope(content.Substring(2, content.Length - 4)));
            }

            var regularScope = context.REGULAR_SCOPE();
            if (regularScope != null)
            {
                var content = regularScope.GetText();
                return new Tuple<string, IVarContent>(varName, 
                    ParseStringToRegularScope(content.Substring(2, content.Length - 4)));
            }

            var textString = context.TEXT_STRING();
            if (textString != null)
            {
                return new Tuple<string, IVarContent>(varName, new TextString(textString.GetText().Trim('"')));
            }
            
            return null;
        }
        #endregion

        #region Function declaration expression
        public override object VisitFunc_declaration(ViewEngineParser.Func_declarationContext context)
        {
            var funcName = context.ID().GetText();
            var declArgs = (List<string>)Visit(context.func_decl_args());

            // to do: add visiting body expression
            var bodyExp = (IFunctionBody)Visit(context.func_body());

            return new FuncDeclarationExpression(funcName, declArgs, bodyExp);
        }

        public override object VisitFunc_body([NotNull] ViewEngineParser.Func_bodyContext context)
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

        public override object VisitFunc_decl_args(ViewEngineParser.Func_decl_argsContext context)
        {
            var expArgs = context.func_decl_args2();
            return expArgs != null ? (List<string>)Visit(expArgs) : new List<string>();
        }

        public override object VisitFunc_decl_args2(ViewEngineParser.Func_decl_args2Context context)
        {
            var declParam = context.VARID().GetText().TrimStart('@');

            var nextArgsExp = context.func_decl_args2();
            var nextArgs = nextArgsExp != null ? (List<string>)Visit(nextArgsExp) : new List<string>();
            nextArgs.Add(declParam);
            return nextArgs;
        }
        #endregion
    }
}