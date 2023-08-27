using Ecommerce.Models;
using Ecommerce.Models.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Seller")]
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
            int userId = (int)Session["UserId"];
            ProductDBcontext dbcontext = new ProductDBcontext();
            List<Product> obj = dbcontext.GetData(userId);
            return View(obj);
        }
        // Display Create Product
        public ActionResult Create()
        {
            CategoryDBcontext dbcontext = new CategoryDBcontext();
            List<Category> categories = dbcontext.listdata();
            ViewBag.Categories = new SelectList(categories, "Name", "Name");
            return View();
        }
        //Add Product when click on submit
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            byte[] imageData = null;

            if (pro.ImageData != null && pro.ImageData.ContentLength > 0)
            {
                // Convert the image file to a byte array
                using (var binaryReader = new BinaryReader(pro.ImageData.InputStream))
                {
                    imageData = binaryReader.ReadBytes(pro.ImageData.ContentLength);
                }
            }

            // Set the binary image data to the corresponding property
            pro.ImageDataBytes = imageData;

            if (ModelState.IsValid)
            {
                int sellerId = (int)Session["UserId"];
                ProductDBcontext dBcontext = new ProductDBcontext();
                bool check = dBcontext.CreateData(pro,sellerId);

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
            int userId = (int)Session["UserId"];
            ProductDBcontext dBcontext = new ProductDBcontext();
            var row = dBcontext.GetData(userId).Find(model => model.Id == id);
            return View(row);
        }
        //to edit in view 
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            int userId = (int)Session["UserId"];
            pro.Id = id;
            if (ModelState.IsValid == true)
            {
                ProductDBcontext dBcontext = new ProductDBcontext();
                bool check = dBcontext.UpdateData(pro,userId);
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
            int userId = (int)Session["UserId"];
            ProductDBcontext dBcontext = new ProductDBcontext();
            var row = dBcontext.GetData(userId).Find(model => model.Id == id);
            return View(row);
        }
        //when click on delete

        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            int userId = (int)Session["UserId"];
            ProductDBcontext dBcontext = new ProductDBcontext();
            bool check = dBcontext.DeleteData(pro, userId);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data Has been Deleted";
                ModelState.Clear();
                return RedirectToAction("products");
            }

            return View();
        }
    }
}