using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.OutputView
{
    public partial class MainViewTemplate
    {
        public string UsingNamespaces { get; set; }
        public string NamespaceName { get; set; }
        public string ViewName { get; set; }
        public string ContentSection { get; set; }
        public string MixinDeclarations { get; set; }
        public string ViewPathKey { get; set; }
    }
}
