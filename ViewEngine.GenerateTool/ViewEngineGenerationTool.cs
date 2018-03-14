
using Microsoft.VisualStudio.TextTemplating.VSHost;
using ViewEngine.Core.Engine;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.Shell;
using VSLangProj80;

namespace ViewEngine.GenerateTool
{
    [ComVisible(true)]
    [Guid("B2324D22-DCEA-4B3C-BF45-781BA15C7EC4")]
    [ProvideObject(typeof(ViewEngineGenerationTool))]
    [CodeGeneratorRegistration(typeof(ViewEngineGenerationTool), "ViewEngineGenerationTool", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    public class ViewEngineGenerationTool : BaseCodeGeneratorWithSite
    {
        private ICoreEngine _coreEngine;
        public ICoreEngine CoreEngine 
            => _coreEngine ?? (_coreEngine = new CoreEngine());


        public override string GetDefaultExtension()
        {
            return ".myview";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var viewName = Path.GetFileNameWithoutExtension(inputFileName);
            var controllerName = Path.GetFileName(Path.GetDirectoryName(inputFileName));

            var generatedText = CoreEngine.RenderMainView(controllerName, viewName, FileNamespace, new StringReader(inputFileContent));
            return Encoding.UTF8.GetBytes(generatedText);
        }
    }
}
