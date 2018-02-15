using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This code is auto generated
namespace Przestrzen
{
    class WidokRenderer
    {
        public StreamWriter Output
        {
            get;
        }

        public WidokRenderer(StreamWriter streamWriter)
        {
            Output = streamWriter;
        }

        public void Render()
        {
            void Html(IReadOnlyDictionary<string, Action> parameters)
            {
                Output.Write("<html>");
                Output.Write("<body>");
                parameters["body"]();
                Output.Write("</body>");
                Output.Write("</html>");
            }

            {
                var parameters = new Dictionary<string, Action>()
                {{"body", () =>
                    {
                        Output.Write("Hello World!!");
                    }
                }, };
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