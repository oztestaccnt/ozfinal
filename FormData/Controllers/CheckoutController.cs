using FormData.DataLayer;
using FormData.Models;
using FormData.Notification;
using FormData.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FormData.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Checkout()
        {

            DataLayer.Customer customer;
            using (var db = new NorthwndEntities())
            {
                //CartDTO cusInfo;
                List<CartDTO> myCr = new List<CartDTO>();
                try
                {
                    customer = db.Customers.Find(UserAccount.GetUserId());
                    foreach (var i in customer.Carts)
                    {
                        myCr.Add(new CartDTO
                        {
                            CustomerId = i.CustomerID,
                            ProductId = i.ProductID,
                            ProductName = i.Product.ProductName,
                            Price = i.Product.UnitPrice,
                            Quantity = i.Quantity,
                            CustomerName = i.Customer.ContactName,
                            CompanyName = i.Customer.CompanyName,
                            Total = i.Product.UnitPrice * (decimal?)i.Quantity,
                            Address = i.Customer.Address,
                            City = i.Customer.City,
                            PostalCode = i.Customer.PostalCode,
                            EmailAddress = i.Customer.Email
                            
                        });
                    }

                    return View(myCr);
                }
                catch (Exception e)
                {
                    return View($"You need to log out and log in again. /n Problem with your page./n {e}");
                }

            }
        }
        List<CartDTO> myCr = new List<CartDTO>();
        EmailSetup emailPreparation = new EmailSetup();

        [HttpPost]
        public ActionResult Confirmation(FormCollection form)
        {
            using (var db = new NorthwndEntities())
            {
                // Customer from data base - DataLayer.Customer
                DataLayer.Customer customer = db.Customers.Find(UserAccount.GetUserId());
                //customer.CompanyName = UpdatedCustomer.CompanyName;
                if (customer.Address == null)
                {
                    customer.Address = HttpContext.Request.Form["sAddress"];
                }
                if (customer.City == null)
                {
                    customer.City = HttpContext.Request.Form["sCity"];
                }
                if (customer.Email == null)
                {
                    customer.Email = HttpContext.Request.Form["sEmail"];
                }
                if (customer.PostalCode == null)
                {
                    customer.PostalCode = HttpContext.Request.Form["sPostalCode"];
                }

                db.SaveChanges();

                customer = db.Customers.Find(UserAccount.GetUserId());
                
                foreach (var i in customer.Carts)
                {
                    myCr.Add(new CartDTO
                    {
                        City = i.Customer.City,
                        Address = i.Customer.Address,
                        PostalCode = i.Customer.PostalCode,
                        EmailAddress = i.Customer.Email,
                        CustomerId = i.CustomerID,
                        ProductId = i.ProductID,
                        ProductName = i.Product.ProductName,
                        Price = i.Product.UnitPrice,
                        Quantity = i.Quantity,
                        CustomerName = i.Customer.ContactName,
                        CompanyName = i.Customer.CompanyName,
                        Total = i.Product.UnitPrice * (decimal?)i.Quantity
                        
                    });
                }
                
                TextEditor();
                if (emailPreparation.SendIt() == emailPreparation.Success)
                {
                    return View(myCr);
                }
                else
                {
                    return RedirectToAction("NotificationFalse", "Checkout");
                }
            } // end of using

        } // end of Confirmation method

        public void TextEditor()
        {
            emailPreparation.ToAddress = myCr[0].EmailAddress;
            emailPreparation.Subject = ($"Conformation for {myCr[0].CustomerName}'s order.");
            for (int i = 0; i < myCr.Count; i++)
            {
                emailPreparation.CompanyName = myCr[i].CompanyName;
                emailPreparation.CustomerName = myCr[i].CustomerName;
                emailPreparation.Address = myCr[i].Address;
                emailPreparation.City = myCr[i].City;
                emailPreparation.PostalCode = myCr[i].PostalCode;
                emailPreparation.EmailAddress = myCr[i].EmailAddress;
                break;
            }
            decimal? tot = 0;
            for (int i = 0; i < myCr.Count; i++)
            {
                emailPreparation.Items += ($"\n{myCr[i].ProductName} - {myCr[i].Quantity.ToString()} qty");
            }
            
            // for debugging
            System.Diagnostics.Debug.WriteLine(emailPreparation.Massage);
            foreach (var i in myCr)
            {
                tot += i.Total;
            }
            
            emailPreparation.Total = tot;
            string.Format("{0:n2}", tot);
            // for debugging
            System.Diagnostics.Debug.WriteLine(string.Format("{0:n2}", tot));

            emailPreparation.Massage = ($"{emailPreparation.CustomerName} \nThis is a conformation for your order." +
                $"\nYour order products are:\n{emailPreparation.Items}\nFinal price is: ${string.Format("{0:n2}", tot)}\n" +
                $"\nYour shipping address is:\n{emailPreparation.Address}\n" +
                $"{emailPreparation.City}, {emailPreparation.PostalCode}" +
                $"\n\n{emailPreparation.CustomerName} Thank you for your order.");
            // for debugging
            System.Diagnostics.Debug.WriteLine(emailPreparation.Massage);

        } // end of TextEditor method

        public ActionResult NotificationFalse()
        {
            return View();
        }
    } // end of CheckoutController class
}