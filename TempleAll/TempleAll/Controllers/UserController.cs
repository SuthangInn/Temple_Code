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

	public ViewResult History() 
        {
            List<History> all = new List<History>();
            using (TempledbEntities dc = new TempledbEntities())
            { all = dc.Histories.ToList();  }
            return View(all);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ViewResult Knowledge()
        {
            List<Knowledge> all = new List<Knowledge>();
            using (KnowledgedbEntities kdb = new KnowledgedbEntities())
            { all = kdb.Knowledges.ToList(); }
            return View(all); 
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ViewResult Activity()
        {
            List<Activity> all = new List<Activity>();
            using (ActivitydbEntities adb = new ActivitydbEntities())
            { all = adb.Activities.ToList(); }
            return View(all);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	
    }
}
