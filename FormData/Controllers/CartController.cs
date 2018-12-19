using FormData.DataLayer;
using FormData.Models;
using FormData.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormData.Controllers
{
    public class CartController : Controller
    {   
        public ActionResult MyCart()
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
                        myCr.Add(new CartDTO{ CustomerId = i.CustomerID,
                            ProductId = i.ProductID,
                            ProductName = i.Product.ProductName,
                            Price = i.Product.UnitPrice,
                            Quantity = i.Quantity,
                            CustomerName = i.Customer.ContactName,
                            CompanyName = i.Customer.CompanyName,
                            Total = i.Product.UnitPrice * (decimal?)i.Quantity });
                    }

                    return View(myCr);
                }
                catch (Exception e)
                {
                    return View($"You need to log out and log in again. /n Problem with your page./n {e}");
                }
                
            }

        }

        [HttpPost]
        public JsonResult AddToCart(CartDTO cartDTO)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            Cart sc = new Cart();
            sc.ProductID = cartDTO.ProductId;
            sc.CustomerID = cartDTO.CustomerId;
            sc.Quantity = cartDTO.Quantity;

            // save changes

            using (var db = new NorthwndEntities())
            {
                if (db.Carts.Any(c=> c.ProductID == sc.ProductID && c.CustomerID == sc.CustomerID))
                {
                    // DON'T DO IT THIS WHAY.
                    // best way to do it it's with injections
                    // iNterface injections.
                    // create an iNterface of the Cart class and inject it were it need it.
                    Cart cart =  db.Carts.FirstOrDefault(c => c.ProductID == sc.ProductID);
                    cart.Quantity += sc.Quantity;
                    sc = new Cart()
                    {
                        // DON'T DO IT THIS WHAY.
                        // best way to do it it's with injections
                        // iNterface injections.
                        // create an iNterface of the Cart class and inject it were it need it.
                        CartID = cart.CartID,
                        ProductID = cart.ProductID,
                        CustomerID = cart.CustomerID,
                        Quantity = cart.Quantity
                    };
                }
                else
                {
                    db.Carts.Add(sc);
                }



                db.SaveChanges();
            }

            return Json(sc, JsonRequestBehavior.AllowGet);
        }
    }
}