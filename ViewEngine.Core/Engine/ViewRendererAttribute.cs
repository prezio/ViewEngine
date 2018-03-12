using System;

namespace ViewEngine.Core.Engine
{
    public class ViewRendererAttribute : Attribute
    {
        public string ViewName { get; set; }
        public string ControllerName { get; set; }

        public ViewRendererAttribute(string viewName, string controllerName)
        {
            ViewName = viewName;
            ControllerName = controllerName;
        }
    }
}