using MasterAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterAdmin.Controllers
{
    public class CategoryController : Controller
    {
        MasterPieceDbEntities db = new MasterPieceDbEntities();

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Create()
        {
            GetViewBagData();
            return View();
        }
        public void GetViewBagData()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "Name");

        }

        [HttpPost]
        public ActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                //foreach (var file in Picture1)
                //{
                //    if (file != null || file.ContentLength > 0)
                //    {
                //        string ext = System.IO.Path.GetExtension(file.FileName);
                //        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                //        {
                //            file.SaveAs(Path.Combine(Server.MapPath("/Content/Images/large"), Guid.NewGuid() + Path.GetExtension(file.FileName)));

                //            var medImg= Images.ResizeImage(Image.FromFile(file.FileName), 250, 300);
                //            medImg.Save(Path.Combine(Server.MapPath("/Content/Images/medium"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                            

                //            var smImg = Images.ResizeImage(Image.FromFile(file.FileName), 45, 55);
                //            smImg.Save(Path.Combine(Server.MapPath("/Content/Images/small"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                        
                //        }
                //    }
                //    db.Products.Add(prod);
                //    db.SaveChanges();
                //    return RedirectToAction("Index", "Product");
                //}
                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            GetViewBagData();
            return View();
        }


        //Get Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Single(x => x.CategoryID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            GetViewBagData();
            return View("Edit", category);
        }

        //Post Edit
        [HttpPost]
        public ActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            GetViewBagData();
            return View(cat);
        }

        //Get Details
        public ActionResult Details(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //Get Delete
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);

        }

        //Post Delete Confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        
    }
}