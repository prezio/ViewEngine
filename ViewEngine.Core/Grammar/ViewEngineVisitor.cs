using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar
{
    public class ViewEngineVisitor : ViewEngineBaseVisitor<object>
    {
        public List<IExpression> Result { get; } = new List<IExpression>();

        public override object VisitFuncUsageExp([NotNull] ViewEngineParser.FuncUsageExpContext context)
        {
            var funcUsage = (IExpression)Visit(context.func_usage());
            Result.Add(funcUsage);

            Visit(context.statement());
            return true;
        }

        public override object VisitCodeLineExp([NotNull] ViewEngineParser.CodeLineExpContext context)
        {
            var codeLine = context.CODE_LINE().GetText().Remove(0, 2);
            return new CodeLineExpression(codeLine);
        }

        public override object VisitFunc_usage([NotNull] ViewEngineParser.Func_usageContext context)
        {
            var functionName = context.ID().GetText();
            var args = (string)Visit(context.func_usage_args());

            return new FuncUsageExpression(functionName, args);
        }

        public override object VisitFunc_usage_args([NotNull] ViewEngineParser.Func_usage_argsContext context)
        {
            var expArgs = context.func_usage_args2();
            return expArgs != null ? (string)Visit(expArgs) : "";
        }

        public override object VisitFunc_usage_args2([NotNull] ViewEngineParser.Func_usage_args2Context context)
        {
            var arg = (string)Visit(context.func_usage_param());
            var nextArgsExp = context.func_usage_args2();
            var nextArgs = nextArgsExp != null ? (string)Visit(nextArgsExp) : null;
            return !string.IsNullOrEmpty(nextArgs) ? $"{arg},{nextArgs}" : $"{arg}";
        }

        public override object VisitFunc_usage_param([NotNull] ViewEngineParser.Func_usage_paramContext context)
        {
            var variableArg = context.VARID();
            if (variableArg != null)
            {
                // we are interested in name of variable without '@' at the begining
                return variableArg.GetText().Remove(0, 1);
            }
            var stringArg = context.TEXT_STRING();
            if (stringArg != null)
            {
                return stringArg.GetText();
            }
            return string.Empty;
        }
    }
}
