using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.Outputs;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.Assignment;
using ViewEngine.Core.Templates.MethodDefinition;
using ViewEngine.Core.Templates.MethodUsage;
using ViewEngine.Core.Templates.Model;
using ViewEngine.Core.Templates.Scope;
using ViewEngine.Core.Templates.StringWrite;

namespace ViewEngine.Core.Templates.MainView
{
    public class TemplateMainViewManager
    {
        public static TemplateMainViewManager CreateMainViewManager()
        {
            return new TemplateMainViewManager(
                new TemplateModelManager(),
                new TemplateScopeManager(
                    new TemplateStringWriteManager(), 
                    new TemplateVariableAssignmentManager(),
                    new TemplateMethodDefinitionManager(),
                    new TemplateMethodUsageManager()
                    ));
        }

        private readonly TemplateModelManager _modelManager;
        private readonly TemplateScopeManager _scopeManager;

        public string GenerateMainView(
            string viewName,
            string namespaceName,
            MainOutput mainOutput,
            SecondaryOutput[] secondaryOutputs)
        {
            var contentSection = _scopeManager.GenerateRegularScope(mainOutput.RegularScope);
            var modelParams = _modelManager.GenerateModelParams(mainOutput.Models);
            var modelDeclarations = _modelManager.GenerateModelDeclarations(mainOutput.Models);
            var modelAssignments = _modelManager.GenerateModelAssignments(mainOutput.Models);
            var modelPassed = _modelManager.GenerateModelPassed(mainOutput.Models);

            var template = new MainViewTemplate
            {
                ViewName = viewName,
                NamespaceName = namespaceName,
                ModelParams = string.IsNullOrEmpty(modelParams) ? string.Empty : $",{modelParams}",
                ModelAssignments = modelAssignments,
                ModelPassed = string.IsNullOrEmpty(modelPassed) ? string.Empty : $",{modelPassed}",
                ModelDeclarations = modelDeclarations,
                ContentSection = contentSection
            };

            return template.TransformText();
        }

        public TemplateMainViewManager(
            TemplateModelManager modelManager,
            TemplateScopeManager scopeManager)
        {
            _modelManager = modelManager;
            _scopeManager = scopeManager;
        }
    }
}
