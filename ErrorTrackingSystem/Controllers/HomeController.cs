using ErrorTrackingSystem.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;




namespace ErrorTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        MistakeTrackingSystemEntities1 db = new MistakeTrackingSystemEntities1();
        
        public ActionResult Index()
        {
            //var list = db.ErrorInformation.ToList();
            //if (!string.IsNullOrEmpty(search))
            //{
            //    list = list.Where(x => x.Company.Contains(search) || x.CustomerName.Contains(search) || x.CustomerSurname.Contains(search)
            //    ||x.CustomerSurname.Contains(search) ||x.ErrorSummary.Contains(search)).ToList();
            //}
            var list = db.ErrorInformation.ToList();
            return View(list);
        }
      
        [HttpGet]
        public ActionResult CreateError()
        {
            List<SelectListItem> inf = (from i in db.ErrorTypes.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.ErrorTypeName,
                                               Value = i.ErrorTypeId.ToString()
                                           }).ToList();
            ViewBag.info = inf;
            return View();
        }

        [HttpPost]
        public ActionResult CreateError(ErrorInformation data,HttpPostedFileBase File)
        {
            if(!ModelState.IsValid)
            {
                return View("CreateError");
            }
            string path = Path.Combine("~/Image" + File.FileName);
            File.SaveAs(Server.MapPath(path));
            data.Image = File.FileName.ToString();

            //var type = db.ErrorTypes.Where(x => x.ErrorTypeId == errorInformation.ErrorTypes.ErrorTypeId).FirstOrDefault();
            //errorInformation.ErrorTypes = type;
            db.ErrorInformation.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
          
        }
        public ActionResult DeleteError(int id)
        {
            var Information = db.ErrorInformation.Find(id);
            db.ErrorInformation.Remove(Information);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetError(int id)
        {
            var update = db.ErrorInformation.Where(x => x.ErrorId == id).FirstOrDefault();
            List<SelectListItem> inf = (from i in db.ErrorTypes.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.ErrorTypeName,
                                            Value = i.ErrorTypeId.ToString()
                                        }).ToList();
            ViewBag.info = inf;


            return View(update);

          
        }
        [HttpPost]
        public ActionResult GetError(ErrorInformation data,HttpPostedFileBase File)
        {

            var update = db.ErrorInformation.Find(data.ErrorId);
            if (File==null)
            {
                //update.ErrorId = data.ErrorId;
                update.Company = data.Company;
                update.CustomerName = data.CustomerName;
                update.CustomerSurname = data.CustomerSurname;
                update.ErrorTypeId = data.ErrorTypeId;
                update.ErrorSummary = data.ErrorSummary;
                update.ErrorDetails = data.ErrorDetails;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                //update.ErrorId = data.ErrorId;
                update.Image = File.FileName.ToString();
                
                update.Company = data.Company;
                update.CustomerName = data.CustomerName;
                update.CustomerSurname = data.CustomerSurname;
                update.ErrorTypeId = data.ErrorTypeId;
                update.ErrorSummary = data.ErrorSummary;
                update.ErrorDetails = data.ErrorDetails;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
           
        }
        public ActionResult GetErrorDetails(int id)
        {
            var d = db.ErrorInformation.Where(x=>x.ErrorId==id).FirstOrDefault();
            return View(d);
        }
        
    }
}