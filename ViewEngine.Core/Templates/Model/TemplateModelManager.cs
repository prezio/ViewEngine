using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.Model
{
    public class TemplateModelManager
    {
        public string GenerateModelDefinition(string modelType)
        {
            var template = new ModelDefinitionTemplate
            {
                ModelType = modelType
            };

            return template.TransformText();
        }
    }
}
