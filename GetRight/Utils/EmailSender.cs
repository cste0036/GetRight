using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GetRight.Utils
{
    public class EmailSender
    {
        // FOR TESTING PURPOSES
        private const String API_KEY = "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";

        public async Task SendAsync(String toEmail, String subject, String contents, String filePath = "")
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("callumstein.psn@gmail.com", "FIT5032 GetRight Newsletter");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Email Attachment
            if (filePath != "")
            {

                using (var fileStream = File.OpenRead(filePath))
                {
                    await msg.AddAttachmentAsync("newsletter.pdf", fileStream);

                }
            }
            var response = await client.SendEmailAsync(msg);

        }

    }
}