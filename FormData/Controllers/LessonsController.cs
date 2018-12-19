using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormData.Controllers
{
    public class LessonsController : Controller
    {
        // GET: Lessons
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lesson9(FormCollection form)  // string num1, string num2
        {
            ViewBag.a = form["num1"];  // num1
            ViewBag.b = form["num2"];  // num2

            double num1, num2;

            //var num1Success = Double.TryParse(form["num1"], out num1);
            //var num2Success = Double.TryParse(form["num2"], out num2);
            //if (num1Success && num2Success)
            //{
            //    ViewBag.Total = num1 + num2;
            //}

            if (Double.TryParse(form["num1"], out num1) && Double.TryParse(form["num2"], out num2))
            {
                ViewBag.Total = num1 + num1;
            }
            else
            {
                ViewBag.Total = "Invalid";
            }

            ViewBag.Names = new string[] { "Joe", "Jim", "Janice", "Joan" };

            return View();
        }

    } // end of LessonsController
}