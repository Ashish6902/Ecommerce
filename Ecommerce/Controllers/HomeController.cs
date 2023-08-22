using Ecommerce.Models;
using Ecommerce.Models.AdminUiImage;
using Ecommerce.Models.Cart;
using Ecommerce.Models.Product;
using Ecommerce.Models.user;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            CategoryDBcontext Dbcontext = new CategoryDBcontext();
            List<Category> categories = Dbcontext.listdata();
            ViewBag.Categories = new SelectList(categories, "Name", "Name");
            ProductDBcontext dbcontext = new ProductDBcontext();
            List<Product> obj = dbcontext.GetAllProductsData();
            return View(obj);

        }
        [Authorize(Roles = "User")]
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
        public ActionResult Buy()
        {
            return View();
        }
        [Authorize(Roles="User")]
        public ActionResult UserDetails()
        {
            int sellerId = (int)Session["UserId"];
            userDBcontext dBcontext = new userDBcontext();
            var row = dBcontext.GetUserData().Find(model => model.Id == sellerId);
            return View(row);
        }
        [HttpPost]
        public ActionResult UserDetails(User use)
        {
            if (ModelState.IsValid == true)
            {
                userDBcontext dBcontext = new userDBcontext();
                bool check = dBcontext.UpdateUserData(use);
                if (check == true)
                {
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Search(string Category)
        {
            ProductDBcontext dbcontext = new ProductDBcontext();
            List<Product> obj = dbcontext.SearchData(Category);
            return View(obj);
        }
        public ActionResult SortByPrice()
        {
            CategoryDBcontext Dbcontext = new CategoryDBcontext();
            List<Category> categories = Dbcontext.listdata();
            ViewBag.Categories = new SelectList(categories, "Name", "Name");
            ProductDBcontext dbcontext = new ProductDBcontext();
            List<Product> obj = dbcontext.GetAllProductsData();
            return View(obj);

        }
    }
}