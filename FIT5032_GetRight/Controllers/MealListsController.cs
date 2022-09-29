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
        public ActionResult Index()
        {
            // Determine DieterID from current user
            var userId = User.Identity.GetUserId();
            var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
            var dieterId = dieter.DieterId;

            var mealLists = db.MealLists.Where(m => m.DieterId == dieterId).ToList();
            return View(mealLists);

            //var mealLists = db.MealLists.Include(m => m.Dieter);
            //return View(mealLists.ToList());
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
