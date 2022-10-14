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
            ViewBag.Attachment = new SelectList(db.Newsletters, "Id", "Name");
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
                    String subject = model.Subject;
                    String contents = model.Contents;
                    int attachment = model.Attachment;

                    // Get attachment path
                    var newsletter = db.Newsletters.Where(n => n.Id == attachment);
                    
                    var pamphlet = db.Newsletters.Find(attachment);
                    string serverPath = Server.MapPath("~/Uploads/");
                    String filePath = serverPath + pamphlet.Path;
                    //attachment = null;

                    EmailSender es = new EmailSender();
                    es.SendAsync(toEmail, subject, contents, filePath);

                    ViewBag.Result = "Email has been sent.";

                    ModelState.Clear();
                    ViewBag.Attachment = new SelectList(db.Newsletters, "Id", "Name");
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