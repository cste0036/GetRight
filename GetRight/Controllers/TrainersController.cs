using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using GetRight.Models;
using Microsoft.AspNet.Identity;

namespace GetRight.Controllers
{
    [RequireHttps]
    [Authorize]
    public class TrainersController : Controller
    {
        private GetRight_Models db = new GetRight_Models();

        // GET: Trainers
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Trainer"))
            {
                var userId = User.Identity.GetUserId();
                var trainersId = db.Trainers.Where(t => t.UserId == userId).ToList();
                var trainers = db.Trainers.Include(t => t.Gym);

                return View(trainersId);
            }
            else
            {
                return View(db.Trainers.ToList());
            }
        }

        // GET: Trainers/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the Trainer and their Average Rating
            Trainer trainer = db.Trainers.Find(id);
            var ratings = db.Ratings.Where(t => t.TrainerId == id).ToList();
            var avg = ratings.Average(x => x.rating1).ToString("0.00");

            if (trainer == null)
            {
                return HttpNotFound();
            }
            // Return the average rating and Trainer details to View
            ViewBag.Average = avg;
            return View(trainer);
        }

        // GET: Trainers/Create
        [Authorize(Roles = "Admin,Trainer")]
        public ActionResult Create()
        {
            ViewBag.GymId = new SelectList(db.Gyms, "GymId", "GymName");
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId,FirstName,LastName,Tags,Description,GymId")] Trainer trainer)
        {
            trainer.UserId = User.Identity.GetUserId();
            ModelState.Clear();

            TryValidateModel(trainer);

            // Apply Regex Check on User's Name to ensure no illegal characters are passed to the database
            if (!Regex.IsMatch(trainer.FirstName, @"^[a-zA-Z'.\s]{1,50}$") && !Regex.IsMatch(trainer.LastName, @"^[a-zA-Z'.\s]{1,50}$"))
            {
                ModelState.AddModelError("", "Name contained illegal characters.");
                return View(trainer);
            }

            // Apply Regex Check on Description and Tags to ensure no illegal characters are passed to the database
            if (!Regex.IsMatch(trainer.Description, @"^[a-zA-Z'.\s]{1,100}$") && !Regex.IsMatch(trainer.Tags, @"^[a-zA-Z'.\s]{1,100}$"))
            {
                ModelState.AddModelError("", "Description/Tags contained illegal characters.");
                return View(trainer);
            }

            if (ModelState.IsValid)
            {
                db.Trainers.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GymId = new SelectList(db.Gyms, "GymId", "GymName", trainer.GymId);
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        [Authorize(Roles = "Admin,Trainer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            ViewBag.GymId = new SelectList(db.Gyms, "GymId", "GymName", trainer.GymId);
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Trainer")]
        public ActionResult Edit([Bind(Include = "TrainerId,FirstName,LastName,UserId,Tags,Description,GymId")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GymId = new SelectList(db.Gyms, "GymId", "GymName", trainer.GymId);
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = db.Trainers.Find(id);
            db.Trainers.Remove(trainer);
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
