using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task_13_2_.Models;

namespace task_13_2_.Controllers
{
    public class MajoursController : Controller
    {
        private ApitaskEntities db = new ApitaskEntities();

        // GET: Majours
        public ActionResult Index()
        {
            var majours = db.Majours.Include(m => m.Factory);
            return View(majours.ToList());
        }

        // GET: Majours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Majour majour = db.Majours.Find(id);
            if (majour == null)
            {
                return HttpNotFound();
            }
            return View(majour);
        }

        // GET: Majours/Create
        public ActionResult Create()
        {
            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName");
            return View();
        }

        // POST: Majours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MajourID,MajourName,FactoryID")] Majour majour)
        {
            if (ModelState.IsValid)
            {
                db.Majours.Add(majour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName", majour.FactoryID);
            return View(majour);
        }

        // GET: Majours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Majour majour = db.Majours.Find(id);
            if (majour == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName", majour.FactoryID);
            return View(majour);
        }

        // POST: Majours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajourID,MajourName,FactoryID")] Majour majour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName", majour.FactoryID);
            return View(majour);
        }

        // GET: Majours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Majour majour = db.Majours.Find(id);
            if (majour == null)
            {
                return HttpNotFound();
            }
            return View(majour);
        }

        // POST: Majours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Majour majour = db.Majours.Find(id);
            db.Majours.Remove(majour);
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
