using System;
using System.Collections.Generic;
using System.IO;
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
                    _viewRenderersDispatcher[ToLocationFormat(attribute.ViewKeyPath)] = (IView)Activator.CreateInstance(type);
                }
            }
        }

        private string ToLocationFormat(string path)
        {
            return $"~\\Views{path.Split(new[] { "Views" }, StringSplitOptions.None).Last()}".ToLower();
        }

        public ViewFactory(Assembly assembly)
        {
            ViewLocationFormats = new [] { "~\\Views\\{1}\\{0}.myview" };
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
            return _viewRenderersDispatcher[viewPath.ToLower()];
        }
    }
}