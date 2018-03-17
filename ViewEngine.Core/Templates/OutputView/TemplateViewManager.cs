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
using ViewEngine.Core.Templates.Scope;
using ViewEngine.Core.Templates.StringWrite;

namespace ViewEngine.Core.Templates.OutputView
{
    public class TemplateViewManager
    {
        public static TemplateViewManager CreateViewManager()
        {
            return new TemplateViewManager(
                new TemplateScopeManager(
                    new TemplateWriteManager(), 
                    new TemplateVariableAssignmentManager(),
                    new TemplateMethodDefinitionManager(),
                    new TemplateUsageManager()
                    ));
        }
        
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
            string viewPathKey,
            string namespaceName,
            MainOutput mainOutput)
        {
            var mixinDeclarations = GenerateMixinDeclarations(mainOutput.Mixins);
            var contentSection = GenerateContent(mainOutput);

            var template = new MainViewTemplate
            {
                ViewName = viewName,
                ViewPathKey = viewPathKey,
                NamespaceName = namespaceName,
                ContentSection = contentSection,
                MixinDeclarations = mixinDeclarations
            };

            return template.TransformText();
        }

        public string GenerateHelper(
            string helperName,
            string namespaceName,
            HelperOutput helperOutput)
        {
            var mixinDeclarations = GenerateMixinDeclarations(helperOutput.Mixins);

            var template = new HelperViewTemplate
            {
                NamespaceName = namespaceName,
                HelperName = helperName,
                MixinDeclarations =  mixinDeclarations
            };

            return template.TransformText();
        }

        public TemplateViewManager(
            TemplateScopeManager scopeManager)
        {
            _scopeManager = scopeManager;
        }
    }
}
