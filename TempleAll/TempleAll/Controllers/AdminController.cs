using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TempleAll.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        private TempledbEntities Historycontext = new TempledbEntities();
        private KnowledgedbEntities Knowledgecontext = new KnowledgedbEntities();
	private ActivitydbEntities Activitycontext = new ActivitydbEntities();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult History()
        {
            List<History> all = new List<History>();

            // Here MyDatabaseEntities is our datacontext
            using (TempledbEntities dc = new TempledbEntities())
            {
                all = dc.Histories.ToList();
            }
            return View(all);
        }

        public ViewResult AdminEditHistory(int id)
        {
            return View("AdminEditHistory", Historycontext.Histories.Find(id));
        }

        [HttpPost]
        public ActionResult AdminEditHistory(History IG)
        {

            //Apply Validation Here
            
                //File size must be
                if (IG.File.ContentLength > (2 * 1024 * 1024))
                {
                    ModelState.AddModelError("CustomerError", "รูปภาพต้องมีขนาดเล็กกว่า : 2 MB");
                    return View();
                }
                if (!(IG.File.ContentType == "image/jpeg" || IG.File.ContentType == "image/gif"))
                {
                    ModelState.AddModelError("CustomerError", "อนุญาติให้ใช้รูปภาพ : jpeg และ gif");
                    return View();
                }

                IG.FileName = IG.File.FileName;
                IG.ImageSize = IG.File.ContentLength;

                byte[] data = new byte[IG.File.ContentLength];
                IG.File.InputStream.Read(data, 0, IG.File.ContentLength);

                IG.ImageData = data;

                Historycontext.Entry(IG).State = EntityState.Modified;

                Historycontext.SaveChanges();  
            return RedirectToAction("History");

        }

         ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ViewResult AdminKnowledge() 
        {
            List<Knowledge> all = new List<Knowledge>();

            using (KnowledgedbEntities kdb = new KnowledgedbEntities())
            {
                all = kdb.Knowledges.ToList();
            }
            return View(all); 
        }

        public ViewResult AdminEditKnowledge(int id)
        {
            return View("AdminEditKnowledge", Knowledgecontext.Knowledges.Find(id));
        }

        [HttpPost]
        public ActionResult AdminEditKnowledge(Knowledge k)
        {
            Knowledgecontext.Entry(k).State = EntityState.Modified;
            Knowledgecontext.SaveChanges();

            return RedirectToAction("AdminKnowledge");
        }

        public ViewResult CreateKnowledge()
        {
            return View("CreateKnowledge", new Knowledge());
        }

        [HttpPost]
        public ActionResult CreateKnowledge(Knowledge k)
        {
            Knowledgecontext.Entry(k).State = EntityState.Added;

            Knowledgecontext.SaveChanges();
            return RedirectToAction("AdminKnowledge");
        }

        public ActionResult DeleteKnowledge(int id)
        {
            Knowledge k = Knowledgecontext.Knowledges.Find(id);
            if (k == null)
            {
                return HttpNotFound();
            }
            else
            {
                Knowledgecontext.Knowledges.Remove(k);
                Knowledgecontext.SaveChanges();
            }
            return RedirectToAction("AdminKnowledge");
        }

        public ViewResult AdminActivity()
        {
            List<Activity> all = new List<Activity>();

            using (ActivitydbEntities adb = new ActivitydbEntities())
            {
                all = adb.Activities.ToList();
            }
            return View(all);
        }

        public ViewResult AdminEditActivity(int id)
        {
            return View("AdminEditActivity", Activitycontext.Activities.Find(id));
        }

        [HttpPost]
        public ActionResult AdminEditActivity(Activity a)
        {
            Activitycontext.Entry(a).State = EntityState.Modified;
            Activitycontext.SaveChanges();

            return RedirectToAction("AdminActivity");
        }

        public ViewResult CreateActivity()
        {
            return View("CreateActivity", new Activity());
        }

        [HttpPost]
        public ActionResult CreateActivity(Activity a)
        {
            Activitycontext.Entry(a).State = EntityState.Added;
            Activitycontext.SaveChanges();

            return RedirectToAction("AdminActivity");
        }

        public ActionResult DeleteActivity(int id)
        {
            Activity a = Activitycontext.Activities.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {
                Activitycontext.Activities.Remove(a);
                Activitycontext.SaveChanges();
            }
            return RedirectToAction("AdminActivity");
        }

public ViewResult AdminContact()
        {
            List<ContactTopic> allTopic = new List<ContactTopic>();

            using (ContactTopicdbEntities ctdb = new ContactTopicdbEntities())
            {
                allTopic = ctdb.ContactTopics.ToList();
            }
            return View(allTopic);
        }

      /*  public ViewResult AdminTempleContact()
        {
            List<Contact> all = new List<Contact>();

            using (ContactdbEntities cdb = new ContactdbEntities())
            {
                all = cdb.Contacts.ToList();
            }
            return View(all);
        }
*/
        public ViewResult AdminEditContact()
        {
            return View("AdminEditContact", Contactcontext.Contacts.Find(1));
        }

        [HttpPost]
        public ActionResult AdminEditContact(Contact c)
        {
            Contactcontext.Entry(c).State = EntityState.Modified;
            Contactcontext.SaveChanges();

            return RedirectToAction("AdminContact");
        }

        public ViewResult ContactTopicDetails(int id)
        {
            ContactTopic ct;
            ct = ContactTopiccontext.ContactTopics.Where(c => c.ContactTopicID == id).FirstOrDefault<ContactTopic>();
            return View(ct);
	}
   }
}

