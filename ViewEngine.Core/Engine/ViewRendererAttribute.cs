using System;

namespace ViewEngine.Core.Engine
{
    public class ViewRendererAttribute : Attribute
    {
        public string ViewKeyPath { get; set; }

        public ViewRendererAttribute(string viewKeyPath)
        {
            ViewKeyPath = viewKeyPath;
        }
    }
}