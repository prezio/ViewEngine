/*  ===========================================================
     
    This code is auto generated and should not be modified
    This is definition of class for rendering "Login"
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
using SampleWebApplication.Models;

// =====================
namespace SampleWebApplication.Views
{
    // LoginView - generated class which
    // should be used for rendering views
    [ViewRenderer(@"E:\kodzenie\mgr\final\ViewEngine.Core\ViewEngine\SampleWebApplication\Views\Home\Login.myview")]
    class LoginView : IView
    {
        // SECTION WITH MIXIN DECLARATIONS ======
        public static void ShowTreeStructure(TextWriter writer, Node node)
        {
            CommonHelper.If(writer, node.Children.Any(), () =>
            {
                CommonHelper.P(writer, () =>
                {
                    writer.Write(@" Węzęł o nazwie """);
                    writer.Write($"{node.Name}");
                    writer.Write(@""" z dziećmi: ");
                    writer.Write("\r\n");
                }

                );
                CommonHelper.Ul(writer, () =>
                {
                    foreach (var child in node.Children)
                    {
                        CommonHelper.Li(writer, () =>
                        {
                            ShowTreeStructure(writer, child);
                        }

                        );
                    }
                }

                );
            }

            , () =>
            {
                CommonHelper.P(writer, () =>
                {
                    writer.Write(@" Węzęł o nazwie """);
                    writer.Write($"{node.Name}");
                    writer.Write(@""" bez dzieci :( ");
                    writer.Write("\r\n");
                }

                );
            }

            );
        }

        public static void Part1(TextWriter writer)
        {
            CommonHelper.H3(writer, () =>
            {
                writer.Write(@"Operacje na zmiennych");
            }

            );
            Action temp = () =>
            {
                writer.Write(@"Linia tekstu bez helpera </br>");
                writer.Write("\r\n");
                var a = 2;
                writer.Write(@"Przypadkowa zmienna: ");
                writer.Write($"{a}");
                writer.Write(@" </br>");
                writer.Write("\r\n");
            }

            ;
            Action temp2 = () =>
            {
                CommonHelper.Line(writer, () =>
                {
                    writer.Write(@"Linia tekstu");
                }

                );
                temp();
            }

            ;
            Action temp3 = () =>
            {
                CommonHelper.Line(writer, () =>
                {
                    writer.Write(@"Linia tekstu2");
                }

                );
                writer.Write(@"<p> Przypadkowy tekst w paragrafie </p>");
                writer.Write("\r\n");
                temp();
                temp2();
            }

            ;
            temp3();
            Action temp4 = () =>
            {
                writer.Write(@"Witaj świecie!! Dzisiaj jest: ");
                writer.Write($"{DateTime.Now}");
                writer.Write("\r\n");
            }

            ;
            temp4();
        }

        public static void Part2(TextWriter writer, Node sample)
        {
            CommonHelper.H3(writer, () =>
            {
                writer.Write(@"Rekurencyjny szablon");
            }

            );
            ShowTreeStructure(writer, sample);
        }

        public static void Part3(TextWriter writer)
        {
            CommonHelper.H3(writer, () =>
            {
                writer.Write(@"Link do akcji");
            }

            );
            CommonHelper.ActionLink(writer, "Home", "Basic", () =>
            {
                writer.Write(@"Kliknij tutaj");
            }

            );
        }

        // END OF SECTION WITH MIXIN DECLARATIONS
        // ======================================
        // Render Method
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            var Model = (LoginViewModel)viewContext.ViewData.Model;
            CommonHelper.Html(writer, () =>
            {
                writer.Write(@"Strona Prywatna");
            }

            , () =>
            {
                CommonHelper.H1(writer, () =>
                {
                    writer.Write(@" Witaj ");
                    writer.Write($"{Model.Name}");
                    writer.Write(@" !! ");
                    writer.Write("\r\n");
                }

                );
                CommonHelper.H2(writer, () =>
                {
                    writer.Write(@" Pokaz możliwosci silnika ");
                    writer.Write("\r\n");
                }

                );
                Part1(writer);
                Part2(writer, Model.Tree);
                Part3(writer);
            }

            );
        }
    }
}