/*  ===========================================================
     
    This code is auto generated and should not be modified
    This is definition of class for rendering "Medium"
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
using ViewEngine.IntegratedTests.Medium;

// =====================
namespace ViewEngine.IntegratedTests
{
    // MediumView - generated class which
    // should be used for rendering views
    [ViewRenderer(@"E:\kodzenie\mgr\final\ViewEngine.Core\ViewEngine\ViewEngine.IntegratedTests\Medium\Medium.myview")]
    class MediumView : IView
    {
        // SECTION WITH MIXIN DECLARATIONS ======
        public static void If(TextWriter writer, bool condition, Action trueState, Action falseState)
        {
            if (condition)
            {
                trueState();
            }
            else
            {
                falseState();
            }
        }

        public static void Html(TextWriter writer, Action title, Action body)
        {
            writer.Write(@"<html>");
            writer.Write("\r\n");
            writer.Write(@"<head>");
            writer.Write("\r\n");
            writer.Write(@"<title>");
            writer.Write("\r\n");
            title();
            writer.Write("\r\n");
            writer.Write(@"</title>");
            writer.Write("\r\n");
            writer.Write(@"</head>");
            writer.Write("\r\n");
            writer.Write(@"<body>");
            writer.Write("\r\n");
            body();
            writer.Write("\r\n");
            writer.Write(@"</body>");
            writer.Write("\r\n");
            writer.Write(@"</html>");
            writer.Write("\r\n");
        }

        public static void Line(TextWriter writer, Action content)
        {
            content();
            writer.Write(@"</br>");
            writer.Write("\r\n");
        }

        // END OF SECTION WITH MIXIN DECLARATIONS
        // ======================================
        // Render Method
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            var Model = (CustomerViewModel)viewContext.ViewData.Model;
            Html(writer, () =>
            {
                writer.Write(@"Tytuł strony");
            }

            , () =>
            {
                Line(writer, () =>
                {
                    writer.Write(@"Witaj świecie");
                }

                );
                Line(writer, () =>
                {
                    writer.Write(@"Imię klienta: ");
                    writer.Write($"{Model.Name}");
                    writer.Write("\r\n");
                }

                );
                If(writer, Model.Address.Contains("USA"), () =>
                {
                    Line(writer, () =>
                    {
                        writer.Write(@"Welcome in Poland");
                    }

                    );
                }

                , () =>
                {
                    Line(writer, () =>
                    {
                        writer.Write(@"Witamy w Polsce");
                    }

                    );
                }

                );
            }

            );
        }
    }
}