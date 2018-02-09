using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.FuncDeclaration;
using ViewEngine.Core.Grammar.FuncUsage;

namespace ViewEngine.Core.Grammar
{
    public class ViewEngineVisitor : ViewEngineBaseVisitor<object>
    {
        public List<IExpression> Result { get; } = new List<IExpression>();

        #region Main Expression visitors
        public override object VisitFuncUsageExp([NotNull] ViewEngineParser.FuncUsageExpContext context)
        {
            var funcUsage = (IExpression)Visit(context.func_usage());
            Result.Add(funcUsage);

            Visit(context.statement());
            return null;
        }

        public override object VisitCodeLineExp([NotNull] ViewEngineParser.CodeLineExpContext context)
        {
            var codeLine = context.CODE_LINE().GetText().Remove(0, 2);
            return new CodeLineExpression(codeLine);
        }

        public override object VisitFuncDeclExp(ViewEngineParser.FuncDeclExpContext context)
        {
            var funcDeclaration = (IExpression) Visit(context.func_declaration());
            Result.Add(funcDeclaration);

            Visit(context.statement());
            return null;
        }

        public override object VisitModelIntroduceExp([NotNull] ViewEngineParser.ModelIntroduceExpContext context)
        {
            var modelIntroduce = (IExpression) Visit(context.model_introduction());
            Result.Add(modelIntroduce);

            Visit(context.statement());
            return null;
        }
        #endregion

        #region Model Introduce Expression

        public override object VisitModel_introduction([NotNull] ViewEngineParser.Model_introductionContext context)
        {
            var varType = context.ID().GetText();
            var varName = context.VARID().GetText().TrimStart('@');

            return new ModelIntroduceExpression(varType, varName);
        }

        #endregion

        #region Function Usage Expression
        public override object VisitFunc_usage([NotNull] ViewEngineParser.Func_usageContext context)
        {
            var functionName = context.ID().GetText();
            var usageArgs = (List<IFuncUsageParam>)Visit(context.func_usage_args());

            return new FuncUsageExpression(functionName, usageArgs);
        }

        public override object VisitFunc_usage_args([NotNull] ViewEngineParser.Func_usage_argsContext context)
        {
            var expArgs = context.func_usage_args2();
            return expArgs != null ? (List<IFuncUsageParam>)Visit(expArgs) : new List<IFuncUsageParam>();
        }

        public override object VisitFunc_usage_args2([NotNull] ViewEngineParser.Func_usage_args2Context context)
        {
            var usageArgs = new List<IFuncUsageParam> {(IFuncUsageParam) Visit(context.func_usage_param()) };
            var nextArgsExp = context.func_usage_args2();
            var nextArgs = nextArgsExp != null ? (List<IFuncUsageParam>)Visit(nextArgsExp) : new List<IFuncUsageParam>();
            usageArgs.AddRange(nextArgs);
            return usageArgs;
        }

        public override object VisitFunc_usage_param([NotNull] ViewEngineParser.Func_usage_paramContext context)
        {
            var variableArg = context.VARID();
            if (variableArg != null)
            {
                // we are interested in name of variable without '@' at the begining
                return new VarFuncUsageParam(variableArg.GetText().TrimStart('@'));
            }
            var stringArg = context.TEXT_STRING();
            return new StringFuncUsageParam(stringArg.GetText().Trim('"'));
        }
        #endregion

        #region Function declaration expression
        public override object VisitFunc_declaration(ViewEngineParser.Func_declarationContext context)
        {
            var funcName = context.ID().GetText();
            var declArgs = (List<IFuncDeclParam>)Visit(context.func_decl_args());

            // to do: add visiting body expression
            var bodyExp = (List<IFuncDeclBodyLine>)Visit(context.func_body());

            return new FuncDeclarationExpression(funcName, declArgs, bodyExp);
        }

        public override object VisitFunc_body([NotNull] ViewEngineParser.Func_bodyContext context)
        {
            var bodyLines = new List<IFuncDeclBodyLine>();

            var codeLine = context.CODE_LINE();
            if (codeLine != null)
            {
                bodyLines.Add(
                        new FuncDeclBodyCodeLine(codeLine.GetText().Remove(0, 2))
                    );
            }

            var templateLine = context.FUNC_BODY_LINE();
            if (templateLine != null)
            {
                bodyLines.Add(
                        new FuncDeclBodyTemplateLine(templateLine.GetText())
                    );
            }

            // we ommit epsilon and comment line
            var nextLinesExp = context.func_body();
            if (nextLinesExp != null)
            {
                var nextLines = (List<IFuncDeclBodyLine>)Visit(nextLinesExp);
                bodyLines.AddRange(nextLines);
            }

            return bodyLines;
        }

        public override object VisitFunc_decl_args(ViewEngineParser.Func_decl_argsContext context)
        {
            var expArgs = context.func_decl_args2();
            return expArgs != null ? (List<IFuncDeclParam>)Visit(expArgs) : new List<IFuncDeclParam>();
        }

        public override object VisitFunc_decl_args2(ViewEngineParser.Func_decl_args2Context context)
        {
            var declArgs = new List<IFuncDeclParam> { (IFuncDeclParam)Visit(context.func_decl_param()) };
            var nextArgsExp = context.func_decl_args2();
            var nextArgs = nextArgsExp != null ? (List<IFuncDeclParam>)Visit(nextArgsExp) : new List<IFuncDeclParam>();
            declArgs.AddRange(nextArgs);
            return declArgs;
        }

        public override object VisitFunc_decl_param(ViewEngineParser.Func_decl_paramContext context)
        {
            var paramType = context.ID().GetText();
            var paramName = context.VARID().GetText().Remove(0, 1);
            return new VarFuncDeclParam(paramType, paramName);
        }

        #endregion
    }
}
