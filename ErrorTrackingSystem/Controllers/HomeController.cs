﻿using ErrorTrackingSystem.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ErrorTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        MistakeTrackingSystemEntities2 db = new MistakeTrackingSystemEntities2();

        public ActionResult Index()
        {

            var errorInformation = db.ErrorInformation.ToList();
            IEnumerable<SelectListItem> inf = (from i in db.ErrorTypes.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.ErrorTypeName,
                                                   Value = i.ErrorTypeId.ToString()
                                               }).ToList();
            ViewBag.info = inf;          
            return View(errorInformation);
        }

        [HttpGet]
        public ActionResult CreateError()
        {
            //ViewBag.info = db.ErrorTypes.Select(x => new SelectListItem { Text = x.ErrorTypeName, Value = x.ErrorTypeId.ToString() }).ToList();
            //var errortypelist = db.ErrorTypes.ToList();
            //ViewBag.info = new SelectList(errortypelist, "ErrorTypeId", "ErrorTypeName");
          
            IEnumerable<SelectListItem> inf = (from i in db.ErrorTypes.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.ErrorTypeName,
                                            Value = i.ErrorTypeId.ToString()
                                        }).ToList();
            ViewBag.info = inf;
            return View();
        }

        [HttpPost]
        public ActionResult CreateError(ErrorInformation errorInformation, HttpPostedFileBase File)
        {

            if (!ModelState.IsValid)
            {

                return View("CreateError");
            }
            if (File != null)
            {

                string image = Path.GetFileName(File.FileName);
                var path = Path.Combine(Server.MapPath("~/Image"), image);
                File.SaveAs(path);
                //string path = Path.Combine("~/Image" + File.FileName);
                //File.SaveAs(Server.MapPath(path)); 
                errorInformation.Image = File.FileName.ToString();
            }
            db.ErrorInformation.Add(errorInformation);
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
        public ActionResult UpdateError(int id)
        {
            var item = db.ErrorInformation.Find(id);
            List<SelectListItem> inf = (from i in db.ErrorTypes.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.ErrorTypeName,
                                            Value = i.ErrorTypeId.ToString()
                                        }).ToList();
            ViewBag.info = inf;
            return View("UpdateError",item);
        }
        [HttpPost]
        public ActionResult UpdateError(ErrorInformation errorInformation, HttpPostedFileBase File)
        {
            var item = db.ErrorInformation.Where(x => x.ErrorId == errorInformation.ErrorId).FirstOrDefault();
            if (File != null)
            {

                string image = Path.GetFileName(File.FileName);
                var path = Path.Combine(Server.MapPath("~/Image"), image);
                File.SaveAs(path);
                //string path = Path.Combine("~/Image" + File.FileName);
                //File.SaveAs(Server.MapPath(path)); 
                errorInformation.Image = File.FileName.ToString();
            }

            item.ErrorId = errorInformation.ErrorId;
            item.Customer.Company = errorInformation.Customer.Company;
            item.Customer.CustomerInformation = errorInformation.Customer.CustomerInformation;
            item.City.CityName = errorInformation.City.CityName;
            item.ErrorSummary = errorInformation.ErrorSummary;
            item.ErrorTId = errorInformation.ErrorTId;
            item.ErrorDetails = errorInformation.ErrorDetails;
            item.Image = errorInformation.Image;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetErrorDetails(int id)
        {
            var d = db.ErrorInformation.Where(x => x.ErrorId == id).FirstOrDefault();
            return View(d);
        }
    }
}
    



