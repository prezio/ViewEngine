using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewEngine.Core.Engine;

namespace SampleWebApplication.Views.Home
{
    [ViewRenderer("~/Views/Home/Index.myview")]
    public class IndexView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            writer.Write("pompka");
        }
    }
}