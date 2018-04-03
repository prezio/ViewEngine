/*  ===========================================================
     
    This code is auto generated and should not be modified
    This is definition of class for rendering "Index"
    Code was generated by ViewEngine
    Version 1.0 - 2018
    
    ===========================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Engine;
using System.Web.Mvc;

// Additional Namespaces
// =====================
namespace SampleWebApplication.Views
{
    // IndexView - generated class which
    // should be used for rendering views
    [ViewRenderer(@"C:\magisterium\ViewEngine\SampleWebApplication\Views\Home\Index.myview")]
    class IndexView : IView
    {
        // SECTION WITH MIXIN DECLARATIONS ======
        // END OF SECTION WITH MIXIN DECLARATIONS
        // ======================================
        // Render Method
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            var Model = (SampleWebApplication.Models.ExampleModel)viewContext.ViewData.Model;
            CommonHelper.Html(writer, new Dictionary<string, Action>()
            {{"body", () =>
            {
                CommonHelper.H1(writer, new Dictionary<string, Action>()
                {{"header", () =>
                {
                    writer.Write(@"Example site generated with ViewEngine");
                }
                }, });
                writer.Write(@"My name is: ");
                writer.Write($"{Model.Name}");
                writer.Write("\r\n");
                writer.Write(@"My surname is: ");
                writer.Write($"{Model.Surname}");
                writer.Write("\r\n");
                CommonHelper.Line(writer, new Dictionary<string, Action>()
                {});
                for (int i = 0; i < 10; i++)
                {
                    CommonHelper.Line(writer, new Dictionary<string, Action>()
                    {{"line", () =>
                    {
                        writer.Write(@"Row no. ");
                        writer.Write($"{i}");
                        writer.Write("\r\n");
                    }
                    }, });
                }

                var a = "pompka";
                CommonHelper.Line(writer, new Dictionary<string, Action>()
                {{"line", () =>
                {
                    writer.Write($"{a}");
                }
                }, });
                Action k = () =>
                {
                    writer.Write(@" Pomoc!!! ");
                    writer.Write("\r\n");
                }

                ;
                CommonHelper.Line(writer, new Dictionary<string, Action>()
                {{"line", () =>
                {
                    writer.Write(@"End line");
                }
                }, });
            }
            }, {"title", () =>
            {
                writer.Write(@"Example Title");
            }
            }, });
        }
    }
}