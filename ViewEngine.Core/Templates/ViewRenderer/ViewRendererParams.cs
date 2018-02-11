using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.ViewRenderer
{
    public class ViewRendererParams
    {
        public string NamespaceName { get; set; }
        public string ViewName { get; set; }
        public string ModelParams { get; set; }
        public string AdditionalSection { get; set; }
        public string ContentSection { get; set; }
    }
}
