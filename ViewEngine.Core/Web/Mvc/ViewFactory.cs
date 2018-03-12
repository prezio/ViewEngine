using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewEngine.Core.Engine;

namespace ViewEngine.Core.Web.Mvc
{
    public class ViewFactory : VirtualPathProviderViewEngine
    {
        private static Dictionary<string, IView> _viewRenderersDispatcher;

        private void SearchGeneratedViews(Assembly assembly)
        {
            _viewRenderersDispatcher = new Dictionary<string, IView>();
            foreach (var type in assembly.GetTypes())
            {
                var attribute = (ViewRendererAttribute)type.GetCustomAttributes(typeof(ViewRendererAttribute))
                    .FirstOrDefault();
                if (attribute != null)
                {
                    //_viewRenderersDispatcher[attribute.ViewPathKey] = (IView)Activator.CreateInstance(type);
                }
            }
        }

        public ViewFactory(Assembly assembly)
        {
            ViewLocationFormats = new [] { "~/Views/{1}/{0}.myview" };
            SearchGeneratedViews(assembly);
        }

        protected override IView CreatePartialView
            (ControllerContext controllerContext, string partialPath)
        {
            throw new NotImplementedException();
        }

        protected override IView CreateView
            (ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return _viewRenderersDispatcher[viewPath];
        }
    }
}