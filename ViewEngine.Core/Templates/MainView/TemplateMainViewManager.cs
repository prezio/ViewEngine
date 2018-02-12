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
                    new TemplateMethodDefinitionManager()
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
            var renderSection = _scopeManager.GenerateRegularScope(mainOutput.RegularScope);
            //var modelParams = _modelManager.GenerateModelParams(mainOutput.Models);

            var template = new MainViewTemplate
            {
                ViewName = viewName,
                NamespaceName = namespaceName,
                RenderSection = renderSection
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
