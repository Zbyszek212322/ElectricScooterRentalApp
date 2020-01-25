using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScooterRentalApplication.Models;

namespace ScooterRentalApplication.Controllers
{
    public class ScootersController : Controller
    {
        private ScooterRentalEntities db = new ScooterRentalEntities();





        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Add(Scooter image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            image.PicturePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            image.ImageFile.SaveAs(fileName);
            using (ScooterRentalEntities db = new ScooterRentalEntities())
            {
                db.Scooter.Add(image);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            return View();
        }




        // GET: Scooters
        public ActionResult Index()
        {
            return View(db.Scooter.ToList());
        }

        // GET: Scooters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scooter scooter = db.Scooter.Find(id);
            if (scooter == null)
            {
                return HttpNotFound();
            }
            return View(scooter);
        }

        // GET: Scooters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Scooters/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScooterID,Brand,Model,Description,PicturePath,PricePerDay")] Scooter scooter)
        {
            if (ModelState.IsValid)
            {
                db.Scooter.Add(scooter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scooter);
        }

        // GET: Scooters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scooter scooter = db.Scooter.Find(id);
            if (scooter == null)
            {
                return HttpNotFound();
            }
            return View(scooter);
        }

        // POST: Scooters/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScooterID,Brand,Model,Description,PicturePath,PricePerDay")] Scooter scooter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scooter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scooter);
        }

        // GET: Scooters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scooter scooter = db.Scooter.Find(id);
            if (scooter == null)
            {
                return HttpNotFound();
            }
            return View(scooter);
        }

        // POST: Scooters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Scooter scooter = db.Scooter.Find(id);
            db.Scooter.Remove(scooter);
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
