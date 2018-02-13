using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Test view
namespace ViewEngine.Runtime
{
    class WidokRenderer
    {
        public StreamWriter Output { get; }
        
        public WidokRenderer(StreamWriter streamWriter)
        {
            Output = streamWriter;
        }

        private void InvokeMethod(Dictionary<string, Action> parameters, string paramName)
        {
            if (parameters.ContainsKey(paramName))
            {
                parameters[paramName]();
            }
        }

        public void Render()
        {

            Action<Dictionary<string, Action>> Html = (parameters) =>
            {
                Output.WriteLine($"<html>");
                Output.WriteLine($"<body>");
                Output.WriteLine($"@body");
                Output.WriteLine($"</body>");
                Output.WriteLine($"</html>");
            };
            {
                var parameters = new Dictionary<string, Action>()
                {
                    {
                        "@body" , () => {
                            Output.WriteLine($"Hello World!!");
                        }
                    },
                };
                Html(parameters);
            }
        }
    }

    public class WidokRenderManager
    {
        public void Render(StreamWriter streamWriter)
        {
            var renderer = new WidokRenderer(streamWriter);
            renderer.Render();
        }
    }
}
