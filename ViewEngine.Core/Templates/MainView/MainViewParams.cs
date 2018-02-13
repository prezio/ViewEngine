using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Templates.MainView
{
    public partial class MainViewTemplate
    {
        public string NamespaceName { get; set; }
        public string ViewName { get; set; }
        public string ContentSection { get; set; }

        public string ModelDeclarations { get; set; }
        public string ModelParams { get; set; }
        public string ModelAssignments { get; set; }
        public string ModelPassed { get; set; }
    }
}
