using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.MixinDeclaration;
using ViewEngine.Core.Grammar.Outputs;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Templates.Assignment;
using ViewEngine.Core.Templates.MethodDefinition;
using ViewEngine.Core.Templates.Usage;
using ViewEngine.Core.Templates.Model;
using ViewEngine.Core.Templates.Scope;
using ViewEngine.Core.Templates.StringWrite;

namespace ViewEngine.Core.Templates.MainView
{
    public class TemplateViewManager
    {
        public static TemplateViewManager CreateMainViewManager()
        {
            return new TemplateViewManager(
                new TemplateModelManager(),
                new TemplateScopeManager(
                    new TemplateWriteManager(), 
                    new TemplateVariableAssignmentManager(),
                    new TemplateMethodDefinitionManager(),
                    new TemplateUsageManager()
                    ));
        }

        private readonly TemplateModelManager _modelManager;
        private readonly TemplateScopeManager _scopeManager;

        public string GenerateContent(MainOutput mainOutput)
        {
            return _scopeManager.GenerateRegularScope(mainOutput.RegularScope);
        }

        public string GenerateMixinDeclarations(List<MixinDeclarationExpression> declarations)
        {
            var ret = new StringBuilder();
            foreach (var declaration in declarations)
            {
                ret.AppendLine(_scopeManager.GenerateMixinDeclaration(declaration));
            }
            return ret.ToString();
        }

        public string GenerateMainView(
            string viewName,
            string namespaceName,
            MainOutput mainOutput,
            SecondaryOutput[] secondaryOutputs)
        {
            var mixinDeclarations = GenerateMixinDeclarations(mainOutput.Mixins);
            var contentSection = GenerateContent(mainOutput);
            var modelParams = _modelManager.GenerateModelParams(mainOutput.Models);
            var modelDeclarations = _modelManager.GenerateModelDeclarations(mainOutput.Models);
            var modelAssignments = _modelManager.GenerateModelAssignments(mainOutput.Models);
            var modelPassed = _modelManager.GenerateModelPassed(mainOutput.Models);

            var template = new MainViewTemplate
            {
                ViewName = viewName,
                NamespaceName = namespaceName,
                ModelParams = modelParams,
                ModelAssignments = modelAssignments,
                ModelPassed = modelPassed,
                ModelDeclarations = modelDeclarations,
                ContentSection = contentSection,
                MixinDeclarations = mixinDeclarations
            };

            return template.TransformText();
        }

        public TemplateViewManager(
            TemplateModelManager modelManager,
            TemplateScopeManager scopeManager)
        {
            _modelManager = modelManager;
            _scopeManager = scopeManager;
        }
    }
}
