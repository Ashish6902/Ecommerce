using Ecommerce.Models;
using Ecommerce.Models.AdminUiImage;
using Ecommerce.Models.user;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // FOR CATEGORY ////////////////////////////////////////////////////////////////////////////////////////////////////

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


        //to get edit Category
        public ActionResult Edit(int id)
        {
            CategoryDBcontext dBcontext = new CategoryDBcontext();
            var row = dBcontext.GetData().Find(model => model.ID == id);
            return View(row);
        }

        //when click on submit button of edit 
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

        //to get delete Category
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
        //to display product



        //FOR PRODUCTS ////////////////////////////////////////////////////////////////////////////////////////////// 

        // to display products to admin
        public ActionResult Products()
        {

            return View();
        }


        // FOR USER //////////////////////////////////////////////////////////////////////////////////////////////////

        //to display users
        public ActionResult Users()
        {
            userDBcontext dbcontext = new userDBcontext();
            List<User> obj = dbcontext.GetUserData();
            return View(obj);

        }
        public ActionResult DeleteUser()
        {

            return View();
        } //  incomplete 

        // FOR SELLER //////////////////////////////////////////////////////////////////////////////////////////////////

        //to display sellers
        public ActionResult Sellers()
        {
            return View();
        }

        // FOR SLIDERS IMAGE ////////////////////////////////////////////////////////////////////////////////////////////

        //to edit ui
        public ActionResult ImagesOfUI()
        {
            imgDBcontext dbcontext = new imgDBcontext();
            List<ImagesUI> obj = dbcontext.GetData();
            return View(obj);
        }
        // to upload images
        /*public ActionResult UploadImage(HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                byte[] imageBytes;
                using (BinaryReader reader = new BinaryReader(imageFile.InputStream))
                {
                    imageBytes = reader.ReadBytes((int)imageFile.InputStream.Length);
                }
                imgDBcontext dbcontext = new imgDBcontext();
                ImagesUI imgmod = new ImagesUI();
                imgmod.ImageData = imageBytes;
                dbcontext.createData(imgmod);
                return View("ImagesOfUI");
            }
            return RedirectToAction("Index");
        }*/ //this is to add image 
        // to edit image
        public ActionResult EditImage(int id)
        {
            return View();
        }
        // when click on edit 
        [HttpPost]
        public ActionResult EditImage(int id, HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                byte[] imageBytes;
                using (BinaryReader reader = new BinaryReader(imageFile.InputStream))
                {
                    imageBytes = reader.ReadBytes((int)imageFile.InputStream.Length);
                }
                imgDBcontext dbcontext = new imgDBcontext();
                ImagesUI imgmod = new ImagesUI();
                imgmod.ImageId = id;
                imgmod.ImageData = imageBytes;
                dbcontext.UpdateData(imgmod);
                return View("index");
            }
            return View();
        }

    }
}