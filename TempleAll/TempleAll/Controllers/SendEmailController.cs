using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace SendEmail.Controllers
{
    public class SendEmailController : Controller
    {
        //
        // GET: /SendEmail/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(SendEmail.Models.EmailModel _objModelMail)
        {
            
            
                if (ModelState.IsValid)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(_objModelMail.To);
                    mail.From = new MailAddress(_objModelMail.From);
                    mail.Subject = _objModelMail.Subject;
                    string Body = _objModelMail.Body;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("se552115076@vr.camt.info", "552115076");// Enter seders User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return View("Index", _objModelMail);
                }
            
           
            else
            {
                return View();
            }
        }
    }
}