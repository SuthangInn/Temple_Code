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

        public ActionResult AdminEditHistory(int id)
        {
            return View("AdminEditHistory",context.Histories.Find(id));
        }

        private TempledbEntities context = new TempledbEntities();
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


                //using (TempledbEntities dc = new TempledbEntities())
                //{
                //if (IG.HistoryID == 0)
                //{ 
                //context.Histories.Add(IG);
                //}
                //else
                //{
                    context.Entry(IG).State = EntityState.Modified;
                //}
                //}
            
                context.SaveChanges();  
            return RedirectToAction("History");

            }

        }
    }

