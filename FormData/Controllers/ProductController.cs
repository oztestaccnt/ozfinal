using System.Linq;
using System.Web.Mvc;
using FormData.DataLayer;

namespace FormData.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(int? id, string SearchString)
        {
            ViewBag.Filter = "Products";

            using (var db = new NorthwndEntities())
            {
                var products = db.Products.Where(p => !p.Discontinued);

                // apply searchstring
                if (!string.IsNullOrEmpty(SearchString))
                {
                    ViewBag.Filter += " Matching " + SearchString;
                    products = products.Where(p => p.ProductName.Contains(SearchString));
                }

                // apply id
                if (id != null)
                {
                    products = products.Where(p => p.ProductID == id);
                }

                // retrieve list of products
                return View(products.OrderBy(p => p.ProductName).ToList());
            }
        }

        public ActionResult ProductByCategory(int? id, string SearchString)
        {
            ViewBag.Filter = "Products";

            using (var db = new NorthwndEntities())
            {
                var products = db.Products
                    .Where(p => !p.Discontinued);

                // apply searchstring
                if (!string.IsNullOrEmpty(SearchString))
                {
                    ViewBag.Filter += " Matching " + SearchString;
                    products = products.Where(p => p.ProductName.Contains(SearchString));
                }

                // apply id
                if (id != null)
                {
                    ViewBag.Filter += " in Category " + db.Categories.Find(id)?.CategoryName;
                    products = products.Where(p => p.CategoryID == id);
                }

                // retrieve list of products
                return View("Index", products.OrderBy(p => p.ProductName).ToList());
            }
        }
        
        // localhost:2631/Product/FilterProducts/?PriceFilter=15
        // localhost:2631/Product/FilterProducts/?SearchString=en&PriceFilter=15
        public JsonResult FilterProducts(string SearchString, decimal? priceFilter = 0)
        {
            //if (priceFilter == null)
            //{
            //    Response.StatusCode = 400;
            //}
            using (var db = new NorthwndEntities())
            {
                var products = db.Products.Where(p => p.UnitPrice >= priceFilter && p.Discontinued == false)
                .OrderBy(pn => pn.ProductName)
                .Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.QuantityPerUnit,
                    p.UnitPrice,
                    p.UnitsInStock
                });
                if (!string.IsNullOrEmpty(SearchString))
                {
                    products.Where(p => p.ProductName.Contains(SearchString));
                }

                var productDTO = products.ToList();

                return Json(productDTO, JsonRequestBehavior.AllowGet);
            }

            //return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }

    
}