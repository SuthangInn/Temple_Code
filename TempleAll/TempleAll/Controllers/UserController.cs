using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TempleAll.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        ContactTopicdbEntities contactTopiccontext = new ContactTopicdbEntities();
        ContactdbEntities contactcontext = new ContactdbEntities();
        AskdbEntities askcontext = new AskdbEntities();

        public ActionResult Index()
        {
            return View();
        }

        

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ViewResult ContactTopic()
        {
            List<Contact> all = new List<Contact>();
            using (ContactdbEntities cdb = new ContactdbEntities())
            { all = cdb.Contacts.ToList(); }
            return View(all);
        }

        public ViewResult CreateContactTopic()
        {
            return View("CreateContactTopic", new ContactTopic());
        }

        [HttpPost]
        public ActionResult CreateContactTopic(ContactTopic c)
        {
            contactTopiccontext.Entry(c).State = EntityState.Added;

            contactTopiccontext.SaveChanges();
            return RedirectToAction("ContactTopic");
        }
	
    }
}
