using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Engine
{
    public interface ICoreEngine
    {
        string Render(
            string viewName,
            string namespaceName,
            string mainFilePath,
            string[] secondaryFilePaths);

        void Render(
            string viewName,
            string namespaceName,
            string mainFilePath,
            string[] secondaryFilePaths,
            string outputViewPath);
    }
}
