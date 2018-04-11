using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleWebApplication.Models;

namespace SampleWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Basic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(PersonModel person)
        {
            string name = person.Name;
            string password = person.Password;

            // simple check if name == password, then we allow access
            if (name != password)
            {
                return View("FailedLogin");
            }

            var loginViewModel = new LoginViewModel
            {
                Name = person.Name,
                Tree = new Node
                {
                    Name = "Jarmarek",
                    Children = new List<Node>
                    {
                        new Node
                        {
                            Name = "Jar",
                            Children = new List<Node>()
                        },
                        new Node
                        {
                            Name = "marek",
                            Children = new List<Node>
                            {
                                new Node
                                {
                                    Name = "ma",
                                    Children = new List<Node>()
                                },
                                new Node
                                {
                                    Name = "rek",
                                    Children = new List<Node>()
                                }
                            }
                        }
                    }
                }
            };

            return View(loginViewModel);
        }
    }
}