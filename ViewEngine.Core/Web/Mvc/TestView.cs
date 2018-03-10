using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewEngine.Core.Web.Mvc
{
    public class TestView : IView
    {

        #region IView Members

        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            writer.Write("pompka jasiu");
        }

        #endregion
    }
}
