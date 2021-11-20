using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using OnlineShopping.Models;
namespace OnlineShopping.Controllers
{
    public class BuyController : Controller
    {
        // GET: Buy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(accdet model)
        {
            var context = new shoppingEntities();

            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    context.accdets.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("ThankYouPage");
                }

            
            ModelState.AddModelError("", "Invalid Details");
            return View();
            }
            else
            {
                ViewBag.ErrMessage = "Error: captcha is not valid.";
                return View();
            }
        }

        public ActionResult ThankYouPage()
        {
            shoppingEntities db = new shoppingEntities();

            var acc = db.accdets.OrderByDescending(x => x.ID).FirstOrDefault();
            
           

            return View(acc);
        }
    }

}
