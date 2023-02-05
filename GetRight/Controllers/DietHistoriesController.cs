using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GetRight.Models;

namespace GetRight.Controllers
{
    public class DietHistoriesController : Controller
    {
        private GetRight_Models db = new GetRight_Models();

        // GET: DietHistories
        public ActionResult Index()
        {
            var dietHistories = db.DietHistories.Include(d => d.Dieter);
            return View(dietHistories.ToList());
        }

        // GET: DietHistories/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietHistory dietHistory = db.DietHistories.Find(id);
            if (dietHistory == null)
            {
                return HttpNotFound();
            }
            return View(dietHistory);
        }

        // GET: DietHistories/Create
        public ActionResult Create()
        {
            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName");
            return View();
        }

        // POST: DietHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordDate,Weight,DieterId")] DietHistory dietHistory)
        {
            if (ModelState.IsValid)
            {
                db.DietHistories.Add(dietHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName", dietHistory.DieterId);
            return View(dietHistory);
        }

        // GET: DietHistories/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietHistory dietHistory = db.DietHistories.Find(id);
            if (dietHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName", dietHistory.DieterId);
            return View(dietHistory);
        }

        // POST: DietHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordDate,Weight,DieterId")] DietHistory dietHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dietHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName", dietHistory.DieterId);
            return View(dietHistory);
        }

        // GET: DietHistories/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietHistory dietHistory = db.DietHistories.Find(id);
            if (dietHistory == null)
            {
                return HttpNotFound();
            }
            return View(dietHistory);
        }

        // POST: DietHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            DietHistory dietHistory = db.DietHistories.Find(id);
            db.DietHistories.Remove(dietHistory);
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
