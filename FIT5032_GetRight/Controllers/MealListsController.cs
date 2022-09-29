using FIT5032_GetRight.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FIT5032_GetRight.Controllers
{
    [Authorize(Roles = "Dieter")]
    public class MealListsController : Controller
    {
        private FIT5032_GetRightModel db = new FIT5032_GetRightModel();

        // GET: MealLists
        public ActionResult Index(DateTime? date = null)
        {
            // If the date value has not been set, create a new DateTime object for Today
            if (!date.HasValue)
            {
                DateTime today = DateTime.Now;
                today = today.Date;

                // Determine DieterID from current user
                var userId = User.Identity.GetUserId();
                var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
                var dieterId = dieter.DieterId;

                // Return all meals for the current user for the selected date (default today's date)
                var mealLists = db.MealLists.Where(m => m.DieterId == dieterId && m.MealDate == today).ToList();
                return View(mealLists);
            }
            else
            {
                // Determine DieterID from current user and pass the date value from the form
                var userId = User.Identity.GetUserId();
                var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
                var dieterId = dieter.DieterId;

                // Return all meals for the current user for the selected date (default today's date)
                var mealLists = db.MealLists.Where(m => m.DieterId == dieterId && m.MealDate == date).ToList();
                return View(mealLists);
            }
            
        }

        // GET: MealLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealList mealList = db.MealLists.Find(id);
            if (mealList == null)
            {
                return HttpNotFound();
            }
            return View(mealList);
        }

        // GET: MealLists/Create
        public ActionResult Create()
        {
            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName");
            return View();
        }

        // POST: MealLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MealId,MealDate,MealName,KiloJoule")] MealList mealList)
        {
            var userId = User.Identity.GetUserId();
            var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
            var dieterId = dieter.DieterId;

            mealList.DieterId = dieterId;
            TryValidateModel(mealList);

            if (ModelState.IsValid)
            {
                db.MealLists.Add(mealList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName", mealList.DieterId);
            return View(mealList);
        }

        // GET: MealLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealList mealList = db.MealLists.Find(id);
            if (mealList == null)
            {
                return HttpNotFound();
            }
            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName", mealList.DieterId);
            return View(mealList);
        }

        // POST: MealLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MealId,MealDate,MealName,KiloJoule,DieterId")] MealList mealList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mealList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DieterId = new SelectList(db.Dieters, "DieterId", "FirstName", mealList.DieterId);
            return View(mealList);
        }

        // GET: MealLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealList mealList = db.MealLists.Find(id);
            if (mealList == null)
            {
                return HttpNotFound();
            }
            return View(mealList);
        }

        // POST: MealLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MealList mealList = db.MealLists.Find(id);
            db.MealLists.Remove(mealList);
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
