using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GetRight.Models;
using Microsoft.AspNet.Identity;

namespace GetRight.Controllers
{
    public class RatingsController : Controller
    {
        private GetRight_Models db = new GetRight_Models();

        // GET: Ratings
        public ActionResult Index()
        {
            if (User.IsInRole("Dieter"))
            {
                // Determine DieterID from current user
                var userId = User.Identity.GetUserId();
                var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
                var dieterId = dieter.DieterId;

                // Return all appointments for the current user
                var ratings = db.Ratings.Where(r => r.DieterId == dieterId).ToList();
                return View(ratings);
            }
            else
            {
                return View(db.Ratings.ToList());
            }
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.TrainerId = new SelectList(db.Trainers, "TrainerId", "FirstName");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DieterId,TrainerId,rating1")] Rating rating)
        {
            // Determine DieterID from current user
            var userId = User.Identity.GetUserId();
            var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
            rating.DieterId = dieter.DieterId;

            // Store the Trainer's Name based on Id
            var trainer = db.Trainers.Where(t => t.TrainerId == rating.TrainerId).FirstOrDefault();
            rating.TrainerName = trainer.FirstName + " " + trainer.LastName;

            ModelState.Clear();
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainerId = new SelectList(db.Trainers, "TrainerId", "FirstName");
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DieterId,TrainerId,rating1")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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
