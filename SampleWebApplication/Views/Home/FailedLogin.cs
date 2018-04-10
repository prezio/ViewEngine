/*  ===========================================================
     
    This code is auto generated and should not be modified
    This is definition of class for rendering "FailedLogin"
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
    // FailedLoginView - generated class which
    // should be used for rendering views
    [ViewRenderer(@"E:\kodzenie\mgr\final\ViewEngine.Core\ViewEngine\SampleWebApplication\Views\Home\FailedLogin.myview")]
    class FailedLoginView : IView
    {
        // SECTION WITH MIXIN DECLARATIONS ======
        // END OF SECTION WITH MIXIN DECLARATIONS
        // ======================================
        // Render Method
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            CommonHelper.Html(writer, () =>
            {
                writer.Write(@"Blad w logowaniu");
            }

            , () =>
            {
                CommonHelper.Line(writer, () =>
                {
                    writer.Write(@"Jest nam przykro, ale podany login i haslo sa niepoprawne");
                }

                );
                CommonHelper.ActionLink(writer, "Home", "Index", () =>
                {
                    writer.Write(@"Powrot na strone glowna");
                }

                );
            }

            );
        }
    }
}