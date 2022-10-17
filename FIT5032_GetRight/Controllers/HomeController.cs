using FIT5032_GetRight.Models;
using FIT5032_GetRight.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_GetRight.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private FIT5032_GetRightModel db = new FIT5032_GetRightModel();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminTools()
        {
            ViewBag.Message = "Administrator Tools.";

            return View();
        }

        // GET: Send Emails
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult SendEmail()
        {
            // For all dieters in the system, retrieve their email addresses
            var dieters = db.Dieters.ToList();
            List<string> emails = new List<string>();

            // For each dieter add their emails to a list
            foreach (var dieter in dieters)
            {
               if ( dieter.Email != null)
                {
                    emails.Add(dieter.Email);
                }
                
            }

            ViewBag.Attachment = new SelectList(db.Newsletters, "Id", "Name");
            ViewBag.Emails = new SelectList(db.Dieters, "Email", "Email");
            return View();
        }

        // POST: Send Emails
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult SendEmail(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    //int emailsId = model.Emails;
                    //String emails = model.Emails;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    int attachmentId = model.Attachment;

                    toEmail = "callumstein.psn@gmail.com";

                    // Get attachment path
                    //var emails = db.Dieters.Where(d => d.DieterId == emailsId);
                    var newsletter = db.Newsletters.Where(n => n.Id == attachmentId);
                    
                    // Create the server path for the pdf to include
                    var pamphlet = db.Newsletters.Find(attachmentId);
                    string serverPath = Server.MapPath("~/Uploads/");
                    String filePath = serverPath + pamphlet.Path;

                    // Create the EmailSender object class and create a threaded task to call the send Email
                    EmailSender es = new EmailSender();

                    // For each email from the View Model call the Email send
                    //foreach (var email in emails)
                    //{
                    //String toEmail = email.Value;
                    //System.Threading.Tasks.Task task = es.SendAsync(toEmail, subject, contents, filePath);
                    es.SendAsync(toEmail, subject, contents, filePath);
                    //}

                    ViewBag.Result = "Emails has been sent.";

                    // Clear the model state and reload the ViewBag object to display the page.
                    ModelState.Clear();
                    ViewBag.Attachment = new SelectList(db.Newsletters, "Id", "Name");
                    ViewBag.Emails = new SelectList(db.Dieters, "Email", "Email");
                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

    }
}