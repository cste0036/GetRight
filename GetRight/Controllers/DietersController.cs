using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GetRight.Models;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace GetRight.Controllers
{
    [Authorize]
    public class DietersController : Controller
    {
        private GetRight_Models db = new GetRight_Models();

        // GET: Dieters
        [RequireHttps]
        [Authorize(Roles = "Admin,Dieter")]
        public ActionResult Index()
        {
            if (User.IsInRole("Dieter"))
            {
                var userId = User.Identity.GetUserId();
                var dieters = db.Dieters.Where(u => u.UserId == userId).ToList();
                return View(dieters);
            }
            else
            {
                return View(db.Dieters.ToList());
            }

        }

        // GET: Dieters/Details/5
        [RequireHttps]
        [Authorize(Roles = "Admin,Dieter")]
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
        [RequireHttps]
        [Authorize(Roles = "Admin,Dieter")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dieters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Dieter")]
        public ActionResult Create([Bind(Include = "DieterId,FirstName,LastName")] Dieter dieter)
        {
            dieter.UserId = User.Identity.GetUserId();
            //dieter.Email = User.Identity.GetUserName();

            // Apply Regex Check on User's Name to ensure no illegal characters are passed to the database
            if (!Regex.IsMatch(dieter.FirstName, @"^[a-zA-Z'.\s]{1,50}$") && !Regex.IsMatch(dieter.LastName, @"^[a-zA-Z'.\s]{1,50}$"))
            {
                ModelState.AddModelError("", "Name contained illegal characters.");
                return View(dieter);
            }

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
        [Authorize(Roles = "Admin,Dieter")]
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
        [Authorize(Roles = "Admin,Dieter")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
