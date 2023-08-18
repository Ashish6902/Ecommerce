﻿using Ecommerce.Models;
using Ecommerce.Models.AdminUiImage;
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
        [Authorize(Roles ="User")]
        public ActionResult Cart()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult Cart(int id )
        {

            return View();
        }
    }
}