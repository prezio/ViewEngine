using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using ViewEngine.Core.Engine;

namespace ViewEngine.Core.Web.Mvc
{
    [Guid("B2324D22-DCEA-4B3C-BF45-781BA15C7EC4")]
    public class ViewEngineGenerationTool : IVsSingleFileGenerator
    {
        private ICoreEngine _coreEngine;
        public ICoreEngine CoreEngine 
            => _coreEngine ?? (_coreEngine = new CoreEngine());

        /*private string GenerateClass(string content)
        {
            CoreEngine.RenderMainView()
        }*/

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".myview";
            return VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace,
            IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            if (bstrInputFileContents == null)
            {
                throw new ArgumentNullException(bstrInputFileContents);
            }
            throw new NotImplementedException();
            //CoreEngine.
        }
    }
}
