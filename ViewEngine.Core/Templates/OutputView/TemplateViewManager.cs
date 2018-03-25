using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.MixinDeclaration;
using ViewEngine.Core.Grammar.Outputs;
using ViewEngine.Core.Grammar.Scope;
using ViewEngine.Core.Grammar.Using;
using ViewEngine.Core.Templates.Assignment;
using ViewEngine.Core.Templates.MethodDefinition;
using ViewEngine.Core.Templates.Model;
using ViewEngine.Core.Templates.Usage;
using ViewEngine.Core.Templates.Scope;
using ViewEngine.Core.Templates.StringWrite;

namespace ViewEngine.Core.Templates.OutputView
{
    public class TemplateViewManager
    {
        public static TemplateViewManager CreateViewManager()
        {
            var usageManager = new TemplateUsageManager();
            return new TemplateViewManager(
                new TemplateScopeManager(
                    new TemplateWriteManager(), 
                    new TemplateVariableAssignmentManager(),
                    new TemplateMethodDefinitionManager(),
                    usageManager
                ),
                new TemplateModelManager(),
                usageManager);
        }
        
        private readonly TemplateScopeManager _scopeManager;
        private readonly TemplateUsageManager _usageManager;
        private readonly TemplateModelManager _modelManager;

        public string GenerateContent(MainOutput mainOutput)
        {
            var ret = new StringBuilder();
            if (mainOutput.Model != null)
            {
                ret.Append(
                    _modelManager.GenerateModelDefinition(mainOutput.Model.VarType));
            }
            ret.Append(_scopeManager.GenerateRegularScope(mainOutput.RegularScope));
            return ret.ToString();
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

        public string GenerateUsingNamespaces(List<UsingExpression> usings)
        {
            var ret = new StringBuilder();
            foreach (var usingExpression in usings)
            {
                ret.AppendLine(_usageManager.GenerateUsingNamespace(usingExpression.NamespaceName));
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
            var usingDeclarations = GenerateUsingNamespaces(mainOutput.Usings);
            var contentSection = GenerateContent(mainOutput);

            var template = new MainViewTemplate
            {
                ViewName = viewName,
                ViewPathKey = viewPathKey,
                UsingNamespaces = usingDeclarations,
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
            var usingDeclarations = GenerateUsingNamespaces(helperOutput.Usings);

            var template = new HelperViewTemplate
            {
                UsingNamespaces = usingDeclarations,
                NamespaceName = namespaceName,
                HelperName = helperName,
                MixinDeclarations =  mixinDeclarations
            };

            return template.TransformText();
        }

        public TemplateViewManager(
            TemplateScopeManager scopeManager,
            TemplateModelManager modelManager,
            TemplateUsageManager usageManager)
        {
            _scopeManager = scopeManager;
            _modelManager = modelManager;
            _usageManager = usageManager;
        }
    }
}
