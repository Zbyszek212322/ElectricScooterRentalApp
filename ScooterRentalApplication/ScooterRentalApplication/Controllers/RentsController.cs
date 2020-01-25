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
    public class RentsController : Controller
    {
        private ScooterRentalEntities db = new ScooterRentalEntities();

        // GET: Rents
        public ActionResult Index()
        {
            var rent = db.Rent.Include(r => r.Customer).Include(r => r.Scooter);
            return View(rent.ToList());
        }

        // GET: Rents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rent.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // GET: Rents/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName");
            ViewBag.ScooterID = new SelectList(db.Scooter, "ScooterID", "Brand");
            return View();
        }

        // POST: Rents/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentID,CustomerID,ScooterID,DateRented,IsReturned,DateOfReturn")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Rent.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", rent.CustomerID);
            ViewBag.ScooterID = new SelectList(db.Scooter, "ScooterID", "Brand", rent.ScooterID);
            return View(rent);
        }

        // GET: Rents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rent.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", rent.CustomerID);
            ViewBag.ScooterID = new SelectList(db.Scooter, "ScooterID", "Brand", rent.ScooterID);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentID,CustomerID,ScooterID,DateRented,IsReturned,DateOfReturn")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", rent.CustomerID);
            ViewBag.ScooterID = new SelectList(db.Scooter, "ScooterID", "Brand", rent.ScooterID);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rent.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rent rent = db.Rent.Find(id);
            db.Rent.Remove(rent);
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
