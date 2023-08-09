using Ecommerce.Models;
using Ecommerce.Models.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }
        //to get view 
        public ActionResult Products()
        {
            ProductDBcontext dbcontext = new ProductDBcontext();
            List<Product> obj = dbcontext.GetData();
            return View(obj);
        }
        // Display Create Category
        public ActionResult Create()
        {
            CategoryDBcontext dbcontext = new CategoryDBcontext();
            List<Category> categories = dbcontext.listdata();
            ViewBag.Categories = new SelectList(categories, "Name", "Name");
            return View();
        }
        //Add Category when click on submit
        [HttpPost]
        public ActionResult Create( Product pro)
        {
            
            if (ModelState.IsValid == true)
            {
                ProductDBcontext dBcontext = new ProductDBcontext();
                bool check = dBcontext.createData(pro);
                if (check)
                {
                    TempData["InsertMessage"] = "Data Has been Inserted";
                    ModelState.Clear();
                    return RedirectToAction("products");
                }
            }

            return View();
        }
        //to get edit view 
        public ActionResult Edit(int id)
        {
            CategoryDBcontext dbcontext = new CategoryDBcontext();
            List<Category> categories = dbcontext.listdata();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ProductDBcontext dBcontext = new ProductDBcontext();
            var row = dBcontext.GetData().Find(model => model.Id == id);
            return View(row);
        }
        //to edit in view 
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {

            if (ModelState.IsValid == true)
            {
                ProductDBcontext dBcontext = new ProductDBcontext();
                bool check = dBcontext.UpdateData(pro);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data Has been updated";
                    ModelState.Clear();
                    return RedirectToAction("products");
                }
            }
            return View();
        }
        //to get delete view
        public ActionResult Delete(int id)
        {
            ProductDBcontext dBcontext = new ProductDBcontext();
            var row = dBcontext.GetData().Find(model => model.Id == id);
            return View(row);
        }
        //when click on delete

        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {


            ProductDBcontext dBcontext = new ProductDBcontext();
            bool check = dBcontext.DeleteData(pro);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data Has been updated";
                ModelState.Clear();
                return RedirectToAction("products");
            }

            return View();
        }
    }
}