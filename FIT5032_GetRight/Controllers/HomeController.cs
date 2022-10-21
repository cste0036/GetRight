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
            // Get a list of all Newsletter attachments in the system
            ViewBag.Attachment = new SelectList(db.Newsletters, "Id", "Name");
            return View(new SendEmailViewModel());
        }

        // POST: Send Bulk Emails
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult SendEmail(SendEmailViewModel model)
        {
                        
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    int attachmentId = model.Attachment;

                    // Get attachment path
                    //var emails = db.Dieters.Where(d => d.DieterId == emailsId);
                    var newsletter = db.Newsletters.Where(n => n.Id == attachmentId);
                    
                    // Create the server path for the pdf to include
                    var pamphlet = db.Newsletters.Find(attachmentId);
                    string serverPath = Server.MapPath("~/Uploads/");
                    String filePath = serverPath + pamphlet.Path;

                    // Create the EmailSender object class and create a threaded task to call the send Email
                    EmailSender es = new EmailSender();

                    // For each Dieter account send an email
                    var dieters = db.Dieters.ToList();
                    foreach (var dieter in dieters)
                    {
                        if (dieter.Email != null)
                        {
                            es.SendAsync(dieter.Email, subject, contents, filePath);
                        }
                    }
    
                    // Show the results of the action
                    ViewBag.Result = "Emails have been sent.";

                    // Clear the model state and reload the ViewBag object to display the page.
                    ModelState.Clear();
                    ViewBag.Attachment = new SelectList(db.Newsletters, "Id", "Name");
                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }
            ViewBag.Attachment = new SelectList(db.Newsletters, "Id", "Name");
            return View(new SendEmailViewModel());
        }

    }
}