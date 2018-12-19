using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FormData.DataLayer;
using FormData.Security;
using FormData.Models;
using System.Net;
using AutoMapper;

namespace FormData.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(FormData.DataLayer.Customer customer) // FormData.Models
        {
            using (NorthwndEntities db = new NorthwndEntities())
            {
                // verify not duplicate

                // will check if customer name is exist
                if (db.Customers.Any(c => c.CompanyName == customer.CompanyName))
                {
                    // if it exist - return the same view...
                    return View();
                }
                // encrypt the password
                customer.UserGuid = System.Guid.NewGuid();
                customer.Password = UserAccount.HashSHA1(customer.Password + customer.UserGuid);

                // save the password
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SignIn()
        {
            using (NorthwndEntities db = new NorthwndEntities())
            {
                var companies = db.Customers.OrderBy(c => c.CompanyName).ToList();
                ViewBag.CustomerId = new SelectList(companies, "CustomerId", "CompanyName");
                return View();
            }

        }

        [HttpPost]
        public ActionResult SignIn(CustomerViewModel customerViewModel, string ReturnUrl)
        {
            using (NorthwndEntities db = new NorthwndEntities())
            {
                if (ModelState.IsValid)
                {

                    FormData.DataLayer.Customer c = db.Customers.Find(customerViewModel.CustomerId);
                    string hashEnteredPassword = UserAccount.HashSHA1(customerViewModel.Password + c.UserGuid);

                    if (hashEnteredPassword == c.Password)
                    {
                        FormsAuthentication.SetAuthCookie(c.CustomerID.ToString(), false);
                        HttpCookie httpCookie = new HttpCookie("role");
                        httpCookie.Value = "customer";
                        Response.Cookies.Add(httpCookie);

                        TempData.Add("Message", "Login Succesful");

                        if (ReturnUrl != null)
                        {
                            return Redirect(ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("Password", "Incorrect Password");

                }
                

                var companies = db.Customers.OrderBy(x => x.CompanyName).ToList();
                ViewBag.CustomerId = new SelectList(companies, "CustomerId", "CompanyName");

                return View();
            }

        }

        /// <summary>
        ///     set up <authentication mode="Forms">
        ///     <forms loginUrl = "~/Customer/SignIn" ></ forms >
        ///     </ authentication > inside Web.config
        ///     and insert [Authorize] on top of method/view.
        /// </summary>
        /// <returns></returns>

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            // make a view with controller for let user know
            // you sign out and use in this return
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Account()
        {


            if (Request.Cookies["role"].Value != "customer")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DataLayer.Customer customer;
            using (var db = new NorthwndEntities())
            {
                customer = db.Customers.Find(UserAccount.GetUserId());
            }
            
            var customerEdit = Mapper.Map<CustomerEdit>(customer);
            
            return View(customerEdit);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Account(CustomerEdit customerEdit)
        {
            using (var db = new NorthwndEntities())
            {
                // Customer from data base - DataLayer.Customer
                DataLayer.Customer customer = db.Customers.Find(UserAccount.GetUserId());
                //customer.CompanyName = UpdatedCustomer.CompanyName;
                customer.Address = customerEdit.Address;
                customer.City = customerEdit.City;
                customer.ContactName = customerEdit.ContactName;
                customer.ContactTitle = customerEdit.ContactTitle;
                customer.Country = customerEdit.Country;
                customer.Email = customerEdit.Email;
                customer.Fax = customerEdit.Fax;
                customer.Phone = customerEdit.Phone;
                customer.PostalCode = customerEdit.PostalCode;
                customer.Region = customerEdit.Region;


                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}