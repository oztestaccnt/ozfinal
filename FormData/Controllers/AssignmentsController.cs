
using FormData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormData.Controllers
{
    public class AssignmentsController : Controller
    {
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// I created Assignments controller for navigate to every Assignment true this controller.
        /// Every Assignment it's return a new ActionResult from the Views.
        /// </summary>
        /// <returns></returns>

        // GET: Assignments
        public ActionResult Bake()
        {
            return View();
        }

        public ActionResult Year(int? id)
        {
            ViewBag.Year = id;
            return View();
        }

        public ActionResult Assignment9(FormCollection form)
        {

            ViewBag.Color_1 = new string[] { "Red", "Green", "Blue" };
            ViewBag.Color_2 = new string[] { "Red", "Green", "Blue" };

            ViewBag.Select_1 = form["col1"];
            ViewBag.Select_2 = form["col2"];


            if (form["col1"] == "Red" && form["col2"] == "Red")
            {
                ViewBag.Output = "Red";
            }
            else if (form["col1"] == "Red" && form["col2"] == "Green")
            {
                ViewBag.Output = "Yellow";
            }
            else if (form["col1"] == "Green" && form["col2"] == "Red")
            {
                ViewBag.Output = "Yellow";
            }
            else if (form["col1"] == "Red" && form["col2"] == "Blue")
            {
                ViewBag.Output = "Magenta";
            }
            else if (form["col1"] == "Blue" && form["col2"] == "Red")
            {
                ViewBag.Output = "Magenta";
            }
            else if (form["col1"] == "Green" && form["col2"] == "Green")
            {
                ViewBag.Output = "Green";
            }
            else if (form["col1"] == "Green" && form["col2"] == "Blue")
            {
                ViewBag.Output = "Cyan";
            }
            else if (form["col1"] == "Blue" && form["col2"] == "Green")
            {
                ViewBag.Output = "Cyan";
            }

            return View();
        } // end of Assignment9 method

        /// <summary>
        ///  OrderForm is a method for make an orders.
        /// </summary>
        /// <returns></returns>

        // create an array of strings
        string[] balloons = { "Red", "Green", "Purple" };

        public ActionResult Birthday()
        {
            // "put" the array in the ViewBag
            ViewBag.balloons = balloons;

            return View();
        }

        [HttpPost]
        public ActionResult Birthday(FormCollection form)
        {
            List<string> balloonList = new List<string>();

            ViewBag.ResultName = form["name"];
            ViewBag.ResultBD = form["birthday"];

            var balloon = form.AllKeys
                .Where(k => k.StartsWith("balloon"))
                .Select(k => form[k]);

            ViewBag.BalloonList = balloon;
            // This is another way do it.
            //foreach (var balloon in balloons)
            //{
            //    // one way to do it
            //    //var b = form[balloon].Split(',');
            //    //var val = b[0];
            //    //bool isChecked = Convert.ToBoolean(val);
            //    // insert isChecked inside if() - if(isCheckd)

            //    // another way.
            //    string isChecked = form[balloon].ToString();
            //    string otherSelection = form[balloon].ToString();

            //    if (isChecked.Equals("true,false") || isChecked.Equals(otherSelection))
            //    {
            //        balloonList.Add(balloon);
            //    }
            //}
            //ViewBag.BalloonList = balloonList; // save list of checked balloons

            return View("BirthdayResults");
        }

        /// <summary>
        /// Midterm
        /// </summary>

        public ActionResult CustomerModel()
        {
            //CustomerInfo customerOut = new CustomerInfo();
            //var customerOutput = customerOut.GetCustInfo();

            return View();  // customerOutput
        }

        // from Models/Customer class - using FormData.Models;

        List<Customer> customers = new List<Customer>();
        List<Order> orders = new List<Order>();
        ProductContext order = new ProductContext();
        List<object> formModel = new List<object>();


        public ActionResult OrderForm(FormCollection form) // string fN, string lN, string shipAddr
        {
            // from Models/Customer class - using FormData.Models;
            customers.Add(new Customer(form["fName"], form["lName"], form["shipAdd"]));
            
            var context = order.GetAll();

            formModel.Add(customers);
            formModel.Add(context);

            return View(formModel);  // orders
        }

        [HttpPost]
        public ActionResult ProcessOrder(FormCollection form)
        {
            customers.Add(new Customer(HttpContext.Request.Form["id_fName"], HttpContext.Request.Form["id_lName"], HttpContext.Request.Form["id_address"]));

            ProductContext productContext = new ProductContext();
            List<Product> products = productContext.GetAll();

            Int16 qty = 0;

            foreach (var p in products)
            {
                if (Int16.TryParse(form[p.Id], out qty) && qty > 0)
                {
                    orders.Add(new Order { Prod = p, Qty = qty });
                }
            }

            formModel.Add(customers);
            formModel.Add(orders);

            return View(formModel);
        }

    } // end of Assignments class
}