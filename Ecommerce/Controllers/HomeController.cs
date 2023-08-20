using Ecommerce.Models;
using Ecommerce.Models.AdminUiImage;
using Ecommerce.Models.Cart;
using Ecommerce.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            imgDBcontext dbcontext = new imgDBcontext();
            List<ImagesUI> obj = dbcontext.GetData();
            return View(obj);
        }
      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Products() 
        {
            ProductDBcontext dbcontext = new ProductDBcontext();
            List<Product> obj = dbcontext.GetAllProductsData();
            return View(obj);

        }
        public ActionResult AddtoCart(int id)
        {
            int sellerId = (int)Session["UserId"];
            CartDbcontext dBcontext = new CartDbcontext();
            dBcontext.CreateCartData(id,sellerId);
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteCartData(int id)
        {
            CartDbcontext dBcontext = new CartDbcontext();
            dBcontext.DeleteCartData(id);
            return RedirectToAction("Cart");
        }
        [Authorize(Roles ="User")]
        public ActionResult Cart()
        {
            int sellerId = (int)Session["UserId"];
            CartDbcontext dBcontext = new CartDbcontext();
            var row = dBcontext.GetCartData(sellerId);
            return View(row);
        }
        
    }
}