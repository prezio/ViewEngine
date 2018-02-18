using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar
{
    public class TemplateTextLineVisitor : TemplateTextLineBaseVisitor<object>
    {
        public List<ITemplateLinePart> Result { get; set; } = new List<ITemplateLinePart>();

        #region Template Text Line statements expressions
        public override object VisitVariable_usage(TemplateTextLineParser.Variable_usageContext context)
        {
            var extVarUsage = context.EXT_VAR_USAGE();
            if (extVarUsage != null)
            {
                var textVarUsage = extVarUsage.GetText();
                Result.Add(
                    new CodeVarUsage(textVarUsage.Substring(2, textVarUsage.Length - 3)
                    ));
            }

            var varUsage = context.VAR_USAGE();
            if (varUsage != null)
            {
                var textVarUsage = varUsage.GetText();
                Result.Add(
                    new TemplateVarUsage(textVarUsage.Substring(1, textVarUsage.Length - 1)
                    ));
            }

            return null;
        }

        public override object VisitRaw_part(TemplateTextLineParser.Raw_partContext context)
        {
            var rawText = context.RAW_PART() != null ? context.RAW_PART().GetText() : context.RAW_CHARACTER().GetText();
            var lastPart = Result.LastOrDefault();
            if (lastPart != null && lastPart is TemplateRawText rawPart)
            {
                rawPart.RawText += rawText;
            }
            else
            {
                Result.Add(new TemplateRawText(rawText));
            }

            return null;
        }
        #endregion
    }
}
