using ErrorTrackingSystem.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ErrorTrackingSystem.Controllers
{
    public class ErrorTypeController : Controller
    {
        // GET: ErrorType
        MistakeTrackingSystemEntities1 db = new MistakeTrackingSystemEntities1();
        public ActionResult Index()
        {
           
            return View(db.ErrorTypes.Where(x=>x.Status==true).ToList());
        }
        public ActionResult Delete(int id)
        {
            var ertype = db.ErrorTypes.Where(x=>x.ErrorTypeId==id).FirstOrDefault();
            db.ErrorTypes.Remove(ertype);
            ertype.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(ErrorTypes errorTypes)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            db.ErrorTypes.Add(errorTypes);
            errorTypes.Status = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    } 
}