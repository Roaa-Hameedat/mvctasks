using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task3_5_2_.Models;

namespace task3_5_2_.Controllers
{
    public class task3Controller : Controller
    {
        private taskthreeEntities2 db = new taskthreeEntities2();

        // GET: task3
        public ActionResult Index(string searchString, string searchBy)
        {
            var task = from t in db.task3 select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchBy)
                {
                    case "First Name":
                        task = task.Where(s => s.First_Name.Contains(searchString));
                        break;
                    case "Last Name":
                        task = task.Where(s => s.Last_Name.Contains(searchString));
                        break;
                    case "Email":
                        task = task.Where(s => s.Email.Contains(searchString));
                        break;
                    default:
                        task = task.Where(s => s.First_Name.Contains(searchString) || s.Last_Name.Contains(searchString) || s.Jop_Title.Contains(searchString) || s.Age.ToString().Equals(searchString));
                        break;
                }
            }
            if (task.ToList().Count <= 0 || searchString == "")
            {
                ViewBag.SearchString = "Not Found";
            }
            return View(task.ToList());
        }

        // GET: task3/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task3 task3 = db.task3.Find(id);
            if (task3 == null)
            {
                return HttpNotFound();
            }
            return View(task3);
        }

        // GET: task3/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: task3/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(task3 task_CRUD, HttpPostedFileBase image, HttpPostedFileBase CV)

        {



            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //string fileName = Path.GetFileName(image.FileName);
                    string path = Server.MapPath("~/img/") + image.FileName;
                    image.SaveAs(path);
                    task_CRUD.Image = image.FileName;
                }

                if (CV != null)
                {
                    //string fileName = Path.GetFileName(cv.FileName);
                    string path = Server.MapPath("~/CV/") + CV.FileName;
                    CV.SaveAs(path);
                    task_CRUD.CV = CV.FileName;
                }
                db.task3.Add(task_CRUD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task_CRUD);

        }
        //public ActionResult Create([Bind(Include = "ID,First_Name,Last_Name,Email,Phon,Age,Jop_Title,Gender,Image,CV")] task3 task3)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.task3.Add(task3);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(task3);
        //}

        // GET: task3/Edit/5
        public FileResult Download(string CV)
        {
            string name = "../CV/" + CV;
            string path = Server.MapPath(name);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application.pdf", CV);

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task3 task3 = db.task3.Find(id);
            Session["image"] =task3.Image;
            Session["cv"] =task3.CV;
            if (task3 == null)
            {
                return HttpNotFound();
            }
            return View(task3);
        }

        // POST: task3/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(task3 task3, HttpPostedFileBase image, HttpPostedFileBase CV)
        {
            task3.Image = Session["image"].ToString();
            task3.CV = Session["cv"].ToString();
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //string fileName = Path.GetFileName(image.FileName);
                    string path = Server.MapPath("~/image/") + image.FileName;
                 image.SaveAs(path);
                    task3.Image = image.FileName;
                }


                if (CV != null)
                {
                    //string fileName = Path.GetFileName(cv.FileName);
                    string path = Server.MapPath("~/CV/") + CV.FileName;
                    CV.SaveAs(path);
                    task3.CV = ViewBag.cv;
                }
                db.Entry(task3).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task3);
        }

        // GET: task3/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task3 task3 = db.task3.Find(id);
            if (task3 == null)
            {
                return HttpNotFound();
            }
            return View(task3);
        }

        // POST: task3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            task3 task3 = db.task3.Find(id);
            db.task3.Remove(task3);
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
