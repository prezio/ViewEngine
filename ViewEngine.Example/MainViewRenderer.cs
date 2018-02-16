/*  ===========================================================
     
    This code is auto generated
    This is definition of class for rendering "Main"
    Code was generated by ViewEngine
    
    ===========================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Example
{
    class MainRenderer
    {
        private void InvokeParameter(IReadOnlyDictionary<string, Action> parameters, string varName)
        {
            if (parameters.ContainsKey(varName))
            {
                parameters[varName]();
            }
        }

        public StreamWriter Output
        {
            get;
        }

        // SECTION WITH MODEL DECLARATIONS ======
        ExampleModel Model
        {
            get;
            set;
        }

        // END OF SECTION WITH MODEL DECLARATIONS
        // ======================================
        // View Renderer Constructor
        public MainRenderer(StreamWriter streamWriter, ExampleModel Model)
        {
            Output = streamWriter;
            this.Model = Model;
        }

        // Render Method
        public void Render()
        {
            void Html(IReadOnlyDictionary<string, Action> parameters)
            {
                Output.Write("<html>");
                Output.Write("\r\n");
                Output.Write("<head>");
                Output.Write("\r\n");
                Output.Write("<title> ");
                InvokeParameter(parameters, "title");
                Output.Write(" </title>");
                Output.Write("\r\n");
                Output.Write("</head>");
                Output.Write("\r\n");
                Output.Write("<body>");
                Output.Write("\r\n");
                InvokeParameter(parameters, "body");
                Output.Write("\r\n");
                Output.Write("</body>");
                Output.Write("\r\n");
                Output.Write("</html>");
                Output.Write("\r\n");
            }

            void H1(IReadOnlyDictionary<string, Action> parameters)
            {
                Output.Write("<h1>");
                InvokeParameter(parameters, "header");
                Output.Write("</h1>");
                Output.Write("\r\n");
            }

            void Line(IReadOnlyDictionary<string, Action> parameters)
            {
                InvokeParameter(parameters, "line");
                Output.Write(" </br>");
                Output.Write("\r\n");
            }

            Html(new Dictionary<string, Action>()
            {{"body", () =>
            {
                H1(new Dictionary<string, Action>()
                {{"header", () =>
                {
                    Output.Write("Example site generated with ViewEngine");
                }
                }, });
                Output.Write("My name is: ");
                Output.Write($"{Model.Name}");
                Output.Write("\r\n");
                Line(new Dictionary<string, Action>()
                {});
                for (int i = 0; i < 10; i++)
                {
                    Line(new Dictionary<string, Action>()
                    {{"line", () =>
                    {
                        Output.Write("Row no. ");
                        Output.Write($"{i}");
                        Output.Write("\r\n");
                    }
                    }, });
                }

                Line(new Dictionary<string, Action>()
                {{"line", () =>
                {
                    Output.Write("End line");
                }
                }, });
            }
            }, {"title", () =>
            {
                Output.Write("Example Title");
            }
            }, });
        }
    }

    // MainRenderManager - generated class which
    // should be used for rendering views
    public class MainRenderManager
    {
        public void Render(StreamWriter streamWriter, ExampleModel Model)
        {
            var renderer = new MainRenderer(streamWriter, Model);
            renderer.Render();
        }
    }
}