/*  ===========================================================
     
    This code is auto generated
    This is definition of class for rendering "Index"
    Code was generated by ViewEngine
    
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
namespace projekcik
{
    // IndexView - generated class which
    // should be used for rendering views
    [ViewRenderer(@"E:\kodzenie\mgr\final\ViewEngine.Core\ViewEngine\SampleWebApplication\Views\Home\Index.myview")]
    class IndexView : IView
    {
        // SECTION WITH MIXIN DECLARATIONS ======
        public static void Html(TextWriter writer, IReadOnlyDictionary<string, Action> environment)
        {
            writer.Write("<html>");
            writer.Write("\r\n");
            writer.Write("<body>");
            writer.Write("\r\n");
            RenderHelpers.InvokeVariable(environment, "body");
            writer.Write("\r\n");
            writer.Write("</body>");
            writer.Write("\r\n");
            writer.Write("</html>");
            writer.Write("\r\n");
        }

        // END OF SECTION WITH MIXIN DECLARATIONS
        // ======================================
        // Render Method
        public void Render(ViewContext viewContext, TextWriter writer)
        {
        }
    }
}