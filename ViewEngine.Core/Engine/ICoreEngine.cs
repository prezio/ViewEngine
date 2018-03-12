using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Engine
{
    public interface ICoreEngine
    {
        string RenderMainView(
            string controllerName,
            string viewName,
            string namespaceName,
            TextReader mainTextReader);

        string RenderHelper(
            string helperName,
            string namespaceName,
            TextReader helperTextReader);
    }
}
