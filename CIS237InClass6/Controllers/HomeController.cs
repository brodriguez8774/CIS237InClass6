using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS237InClass6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            // More testing of how default routes work.
            // ViewBag is a way to send data to the view.
            ViewBag.SomethingElse = "Something Else Here";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Added method to test how default route works.
        // Will get called if browser is pointed to /home/foo
        public ActionResult Foo()
        {
            // Content is a calss that implements ActionResult so we can return it.
            // Simply returns some text. Nothing fancy.
            return View();
        }
    }
}