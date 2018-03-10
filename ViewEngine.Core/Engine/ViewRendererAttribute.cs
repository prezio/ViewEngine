using System;

namespace ViewEngine.Core.Engine
{
    public class ViewRendererAttribute : Attribute
    {
        public string ViewPathKey { get; set; }

        public ViewRendererAttribute(string viewPathKey)
        {
            ViewPathKey = viewPathKey;
        }
    }
}