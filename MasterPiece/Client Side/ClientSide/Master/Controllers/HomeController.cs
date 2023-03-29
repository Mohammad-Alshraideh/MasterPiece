using MasterPieceDb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace MasterPieceDb.Controllers
{
    public class HomeController : Controller
    {
        MasterPieceDbEntities db = new MasterPieceDbEntities();

        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewBag.MenProduct = db.Products.Where(x => x.Category.Name.Equals("Men")).ToList();
            ViewBag.WomenProduct = db.Products.Where(x => x.Category.Name.Equals("Women")).ToList();
            ViewBag.SportsProduct = db.Products.Where(x => x.Category.Name.Equals("Sports")).ToList();
            ViewBag.ElectronicsProduct = db.Products.Where(x => x.Category.Name.Equals("Phones")).ToList();
            ViewBag.Slider = db.genMainSliders.ToList();
            ViewBag.TopRatedProducts = TopSoldProducts();
            ViewBag.PromoRight = db.genPromoRights.ToList();

            this.GetDefaultData();

            return View(db.Products.OrderBy(p => p.Name).ToPagedList(page ?? 1, 7));
        }
        public List<TopSoldProduct> TopSoldProducts()
        {
            var prodList = (from prod in db.OrderDetails
                            select new { prod.ProductID, prod.Quantity } into p
                            group p by p.ProductID into g
                            select new
                            {
                                pID = g.Key,
                                sold = g.Sum(x => x.Quantity)
                            }).OrderByDescending(y => y.sold).Take(3).ToList();



            List<TopSoldProduct> topSoldProds = new List<TopSoldProduct>();

            for (int i = 0; i < 3; i++)
            {
                topSoldProds.Add(new TopSoldProduct()
                {
                    product = db.Products.Find(prodList[i].pID),
                    CountSold = Convert.ToInt32(prodList[i].sold)
                });
            }
            return topSoldProds;
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

    }
}