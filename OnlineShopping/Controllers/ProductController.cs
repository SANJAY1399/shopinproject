using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        shoppingEntities db = new shoppingEntities();
        List<AddCart> li = new List<AddCart>();
        // GET: Product
        public ActionResult Index()
        {


            var query = db.clothes.ToList();

            return View(query);
        }



        [Authorize(Roles ="Admin")]
        public ActionResult AddProduct()
        {
            List<Category> list1 = db.Categories.ToList();
            ViewBag.CatList = new SelectList(list1, "CategoryID", "CategoryName");

            List<Material> list2 = db.Materials.ToList();
            ViewBag.MatList = new SelectList(list2, "MaterialID", "MaterialName");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(cloth c, HttpPostedFileBase Image)
        {
            List<Category> list1 = db.Categories.ToList();
            ViewBag.CatList = new SelectList(list1, "CategoryID", "CategoryName");

            List<Material> list2 = db.Materials.ToList();
            ViewBag.MatList = new SelectList(list2, "MaterialID", "MaterialName");

            if (ModelState.IsValid)
            {

                cloth clo = new cloth();
                clo.clothName = c.clothName;
                clo.Price = c.Price;
                clo.CategoryID = c.CategoryID;
                clo.MaterialID = c.MaterialID;
                clo.Image = Image.FileName.ToString();
                var folder = Server.MapPath("~/Content/Images/");
                Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));
                db.clothes.Add(clo);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Product did not upload");
            }
            return View();
        }

        public ActionResult Categories()
        {
            List<Category> c = db.Categories.ToList();
            return View(c);
        }

        public ActionResult CatSelect(int? id)
        {
            var klist = db.clothes.Where(x => x.CategoryID == id).ToList();
            return View(klist);
        }


        //public ActionResult Select(int ? cid, int ? mid)
        //{
        //    var klist = db.clothes.Where(x => x.CategoryID == cid && x.MaterialID == mid).ToList();
        //    return View(klist);
        //}

        public ActionResult MenMats()
        {
            List<Material> m = db.Materials.ToList();
            return View(m);
        }

        public ActionResult MmatSelect(int? mmid)
        {
            var klist = db.menprods.Where(x => x.MaterialID == mmid).ToList();
            return View(klist);
        }

        public ActionResult KidMats()
        {
            List<Material> m = db.Materials.ToList();
            return View(m);
        }

        public ActionResult KmatSelect(int? kmid)
        {
            var list = db.kidprods.Where(x => x.MaterialID == kmid).ToList();
            return View(list);
        }

        public ActionResult WomenMats()
        {
            List<Material> m = db.Materials.ToList();
            return View(m);
        }

        public ActionResult WmatSelect(int? Wmid)
        {
            var list = db.womenprods.Where(x => x.MaterialID == Wmid).ToList();
            return View(list);
        }

        public ActionResult search(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {

                return View(db.clothes.Where(x => x.clothName.Contains(searchString)).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
       
        public ActionResult ClothSelect(int ? id)
        {

            //cart c = new cart();
            cart cr = new cart();
            //cloth cl = new cloth();
            using (var context = new shoppingEntities())
            {
                bool isvalid = context.clothes.Any(x => x.ClothID == id);
                if (isvalid)
                {
                    var cols = context.clothes.Where(x => x.ClothID == id);
                    foreach (cloth c in cols)
                    {
                        cr.ClothID = c.ClothID;
                        cr.clothName = c.clothName;
                        cr.Price = c.Price;
                        cr.Image = c.Image;

                    }
                    context.carts.Add(cr);
                    context.SaveChanges();


                }



            }
            List<cart> car = db.carts.ToList();
            return View(car);
            //return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            //return new EmptyResult();

        }
        
        public ActionResult Delete(int ? id)
        {
            shoppingEntities db = new shoppingEntities();

            //db.carts.Remove(db.carts.FirstOrDefault(x => x.ClothID.Equals(id)));
            usercart c = db.usercarts.FirstOrDefault(x => x.clothID == id);
            db.usercarts.Remove(c);
            db.SaveChanges();
            return RedirectToAction("CartView","Product");
        }
        [Authorize]
        public ActionResult CartView(int ? id)
        {
            string uname = Request.Cookies.Get("uId").Value;

            //cart c = new cart();
            usercart cr = new usercart();
            //cloth cl = new cloth();
            using (var context = new shoppingEntities())
            {
               
                

                //var basket = context.Users.Where(x => x.userID == Convert.ToInt32(userId)).ToList();

                bool isvalid = context.clothes.Any(x => x.ClothID == id);
                if (isvalid)
                {
                    
                    var cols = context.clothes.Where(x => x.ClothID == id);
                    foreach (cloth c in cols)
                    {
                        cr.username = uname;
                        cr.clothID = c.ClothID;
                        cr.clothName = c.clothName;
                        cr.Price = c.Price;
                        cr.Image = c.Image;

                    }
                    context.usercarts.Add(cr);
                    context.SaveChanges();


                }



            }

            List<usercart> car = db.usercarts.Where(x => x.username == uname).ToList();
            
            return View(car);
        }
    }
}