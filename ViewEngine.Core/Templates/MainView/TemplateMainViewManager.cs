using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Outputs;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.Model;

namespace ViewEngine.Core.Templates.MainView
{
    public class TemplateMainViewManager
    {
        private TemplateModelManager _modelManager;

        private void VisitInvokeParameters(StringBuilder section,
            Dictionary<string, IVarContent> parameters)
        {
            foreach (var parameter in parameters)
            {
                
            }
        }

        private void VisitRegularExpression(StringBuilder mainSection,
            StringBuilder additionalSection, IRegularExpression expression)
        {
            if (expression is CodeLineExpression codeLine)
            {
                mainSection.AppendLine(codeLine.CodeLine);
            }
            else if (expression is FuncUsageExpression funcUsage)
            {
                //funcUsage.
            }
        }

        public void GenerateMainView(MainOutput mainOutput, SecondaryOutput[] secondaryOutputs)
        {
            var additionalSection = new StringBuilder();
            var mainSection = new StringBuilder();
            var modelParams = _modelManager.GenerateModelParams(mainOutput.Models);

            foreach (var regularExp in mainOutput.Expressions)
            {

            }
        }
    }
}
