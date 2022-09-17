using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_GetRight.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_GetRight.Controllers
{
    public class DietersController : Controller
    {
        private FIT5032_GetRightModel db = new FIT5032_GetRightModel();

        // GET: Dieters
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var dieters = db.Dieters.Where(d => d.UserId == userId).ToList();
            return View(db.Dieters.ToList());
        }

        // GET: Dieters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dieter dieter = db.Dieters.Find(id);
            if (dieter == null)
            {
                return HttpNotFound();
            }
            return View(dieter);
        }

        // GET: Dieters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dieters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "DieterId,FirstName,LastName")] Dieter dieter)
        {
            dieter.UserId = User.Identity.GetUserId();

            ModelState.Clear();
            TryValidateModel(dieter);

            if (ModelState.IsValid)
            {
                db.Dieters.Add(dieter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dieter);
        }

        // GET: Dieters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dieter dieter = db.Dieters.Find(id);
            if (dieter == null)
            {
                return HttpNotFound();
            }
            return View(dieter);
        }

        // POST: Dieters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DieterId,FirstName,LastName,UserId")] Dieter dieter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dieter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dieter);
        }

        // GET: Dieters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dieter dieter = db.Dieters.Find(id);
            if (dieter == null)
            {
                return HttpNotFound();
            }
            return View(dieter);
        }

        // POST: Dieters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dieter dieter = db.Dieters.Find(id);
            db.Dieters.Remove(dieter);
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
