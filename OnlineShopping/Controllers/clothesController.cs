using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class clothesController : Controller
    {
        private shoppingEntities db = new shoppingEntities();

        // GET: clothes
        public ActionResult Index()
        {
            var clothes = db.clothes.Include(c => c.Category).Include(c => c.Material);
            return View(clothes.ToList());
        }

        // GET: clothes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cloth cloth = db.clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }

        // GET: clothes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName");
            return View();
        }

        // POST: clothes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClothID,CategoryID,MaterialID,clothName,Price,Image")] cloth cloth)
        {
            if (ModelState.IsValid)
            {
                db.clothes.Add(cloth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", cloth.CategoryID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", cloth.MaterialID);
            return View(cloth);
        }

        // GET: clothes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cloth cloth = db.clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", cloth.CategoryID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", cloth.MaterialID);
            return View(cloth);
        }

        // POST: clothes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClothID,CategoryID,MaterialID,clothName,Price,Image")] cloth cloth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cloth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", cloth.CategoryID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", cloth.MaterialID);
            return View(cloth);
        }

        // GET: clothes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cloth cloth = db.clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }

        // POST: clothes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cloth cloth = db.clothes.Find(id);
            db.clothes.Remove(cloth);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
