using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.OutputView
{
    public partial class HelperViewTemplate
    {
        public string NamespaceName { get; set; }
        public string HelperName { get; set; }
        public string MixinDeclarations { get; set; }
    }
}
