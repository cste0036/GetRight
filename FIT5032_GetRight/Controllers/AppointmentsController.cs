using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DDay.iCal;
using FIT5032_GetRight.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_GetRight.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private FIT5032_GetRightModel db = new FIT5032_GetRightModel();

        // GET: Appointments
        [Authorize]
        public ActionResult Index()
        {
            // If the user is a Dieter return the relevant appointments
            if (User.IsInRole("Dieter"))
            {
                // Determine DieterID from current user
                var userId = User.Identity.GetUserId();
                var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
                var dieterId = dieter.DieterId;

                // Return all appointments for the current user
                var appointments = db.Appointments.Where(a => a.DieterId == dieterId).ToList();

                return View(appointments);
            }
            // If the user is a Trainer return the relevant appointments
            else if (User.IsInRole("Trainer"))
            {
                // Determine TrainerID from current user
                var userId = User.Identity.GetUserId();
                var trainer = db.Trainers.Where(d => d.UserId == userId).FirstOrDefault();
                var trainerId = trainer.TrainerId;

                // Return all appointments for the current user
                var appointments = db.Appointments.Where(a => a.TrainerId == trainerId).ToList();
                return View(appointments);
            }
            // Else if the User is an admin return all appointments in system
            else
            {
                return View(db.Appointments.ToList());
            }
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create?date=YYYY-MM-DD
        public ActionResult Create(String date)
        {
            if (null == date)
                return RedirectToAction("Index");

            Appointment a = new Appointment();
            DateTime convertedDate = DateTime.Parse(date);
            a.AppDate = convertedDate;
            ViewBag.TrainerId = new SelectList(db.Trainers, "TrainerId", "FirstName");
            return View(a);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,AppDate,Length,TrainerId")] Appointment appointment)
        {
            // Determine DieterID from current user
            var userId = User.Identity.GetUserId();
            var dieter = db.Dieters.Where(d => d.UserId == userId).FirstOrDefault();
            appointment.DieterId = dieter.DieterId;

            ModelState.Clear();
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Trainers = new SelectList(db.Trainers, "TrainerId", "FirstName", appointment.TrainerId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,AppDate,Length,DieterId,TrainerId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
