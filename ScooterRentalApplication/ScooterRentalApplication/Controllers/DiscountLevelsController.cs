using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScooterRentalApplication.Models;

namespace ScooterRentalApplication.Controllers
{
    public class DiscountLevelsController : Controller
    {
        private ScooterRentalEntities db = new ScooterRentalEntities();

        // GET: DiscountLevels
        public ActionResult Index()
        {
            return View(db.DiscountLevel.ToList());
        }

        // GET: DiscountLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountLevel discountLevel = db.DiscountLevel.Find(id);
            if (discountLevel == null)
            {
                return HttpNotFound();
            }
            return View(discountLevel);
        }

        // GET: DiscountLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscountLevels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiscountLevelID,DiscountCode,DiscountValue")] DiscountLevel discountLevel)
        {
            if (ModelState.IsValid)
            {
                db.DiscountLevel.Add(discountLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discountLevel);
        }

        // GET: DiscountLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountLevel discountLevel = db.DiscountLevel.Find(id);
            if (discountLevel == null)
            {
                return HttpNotFound();
            }
            return View(discountLevel);
        }

        // POST: DiscountLevels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountLevelID,DiscountCode,DiscountValue")] DiscountLevel discountLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discountLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discountLevel);
        }

        // GET: DiscountLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountLevel discountLevel = db.DiscountLevel.Find(id);
            if (discountLevel == null)
            {
                return HttpNotFound();
            }
            return View(discountLevel);
        }

        // POST: DiscountLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountLevel discountLevel = db.DiscountLevel.Find(id);
            db.DiscountLevel.Remove(discountLevel);
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
