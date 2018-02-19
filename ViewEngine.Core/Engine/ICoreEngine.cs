using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Engine
{
    public interface ICoreEngine
    {
        string RenderMainView(
            string viewName,
            string namespaceName,
            string mainFilePath);

        string RenderHelper(
            string helperName,
            string namespaceName,
            string helperFilePath);
    }
}
