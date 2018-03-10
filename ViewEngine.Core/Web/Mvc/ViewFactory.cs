using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewEngine.Core.Web.Mvc
{
    public class ViewFactory : VirtualPathProviderViewEngine
    {
        private static Dictionary<string, IView> _viewRenderersDictionary;

        public ViewFactory()
        {
            this.ViewLocationFormats = new []
                { "~/Views/{1}/{0}.myview", "~/Views/Shared/{0}.myview" };

            this.PartialViewLocationFormats = new []
                { "~/Views/{1}/{0}.myview", "~/Views/Shared/{0}.myview" };
        }

        protected override IView CreatePartialView
            (ControllerContext controllerContext, string partialPath)
        {
            //var physicalpath = controllerContext.HttpContext.Server.MapPath(partialPath);
            return new TestView();
        }

        protected override IView CreateView
            (ControllerContext controllerContext, string viewPath, string masterPath)
        {
            //var physicalpath = controllerContext.HttpContext.Server.MapPath(viewPath);
            return new TestView();
        }
    }
}
