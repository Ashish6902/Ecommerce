using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        { 
            return View(); 
        }
        //Display Category 
        public ActionResult Category()
        {
            CategoryDBcontext dbcontext = new CategoryDBcontext();
            List<Category> obj = dbcontext.GetData();
            return View(obj);
        }
        // Display Create Category
        public ActionResult Create()
        {

            return View();
        }
        //Add Category when click on submit
        [HttpPost]
        public ActionResult Create(Category cat)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    CategoryDBcontext dBcontext = new CategoryDBcontext();
                    bool check = dBcontext.createData(cat);
                    if (check == true)
                    {
                        TempData["InsertMessage"] = "Data Has been Insrted";
                        ModelState.Clear();
                        return RedirectToAction("Category");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        //to get edit view 
        public ActionResult Edit(int id)
        {
            CategoryDBcontext dBcontext = new CategoryDBcontext();
            var row = dBcontext.GetData().Find(model => model.ID == id);
            return View(row);
        }
        //to edit in view 
        [HttpPost]
        public ActionResult Edit(int id, Category stu)
        {

            if (ModelState.IsValid == true)
            {
                CategoryDBcontext dBcontext = new CategoryDBcontext();
                bool check = dBcontext.UpdateData(stu);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data Has been updated";
                    ModelState.Clear();
                    return RedirectToAction("Category");
                }
            }
            return View();
        }
        //to get delete view
        public ActionResult Delete(int id)
        {
            CategoryDBcontext dBcontext = new CategoryDBcontext();
            var row = dBcontext.GetData().Find(model => model.ID == id);
            return View(row);
        }
        //when click on delete

        [HttpPost]
        public ActionResult Delete(int id, Category cat)
        {


            CategoryDBcontext dBcontext = new CategoryDBcontext();
            bool check = dBcontext.DeleteData(cat);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data Has been updated";
                ModelState.Clear();
                return RedirectToAction("Category");
            }

            return View();
        }
    }
}