using ErrorTrackingSystem.Models.Entity;
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
        [Authorize]
        public ActionResult Index()
        {
            var errorInformation = db.ErrorInformation.ToList();
            //IEnumerable<SelectListItem> company = (from i in db.Customer.ToList()
            //                                     select new SelectListItem
            //                                     {
            //                                         Text = i.Company,
            //                                         Value = i.CustomerId.ToString()
            //                                     }).ToList();
            //ViewBag.com = company;

            IEnumerable<SelectListItem> value = (from i in db.Customer.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.CustomerName,
                                                     Value = i.CustomerId.ToString()
                                                 }).ToList();
            ViewBag.val = value;
            IEnumerable<SelectListItem> inf = (from i in db.ErrorTypes
                                               select new SelectListItem
                                               {
                                                   Text = i.ErrorTypeExplanation,
                                                   Value = i.ErrorTypeId.ToString()
                                               }).ToList();
            ViewBag.info=inf;
            return View(errorInformation);
        }
        //public JsonResult GetSearchValue(string search)
        //{
        //    List<Customer> allsearch = db.Customer.Where(x => x.CustomerName.Contains(search)).Select(x => new Customer
        //    {
        //        CustomerId = x.CustomerId,
        //        CustomerName = x.CustomerName
        //    }).ToList();
        //    return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}
        [HttpGet]
        public ActionResult CreateError()
        {
            //ViewBag.info = db.ErrorTypes.Select(x => new SelectListItem { Text = x.ErrorTypeName, Value = x.ErrorTypeId.ToString() }).ToList();
            //var errortypelist = db.ErrorTypes.ToList();
            //ViewBag.info = new SelectList(errortypelist, "ErrorTypeId", "ErrorTypeName");
            //IEnumerable<SelectListItem> company = (from i in db.Customer.ToList()
            //                                       select new SelectListItem
            //                                       {
            //                                           Text = i.Company,
            //                                           Value = i.CustomerId.ToString()
            //                                       }).ToList();
            //ViewBag.com = company;
            IEnumerable<SelectListItem> deger = (from i in db.Customer.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.CustomerName,
                                                     Value = i.CustomerId.ToString()
                                                 }).ToList();
            ViewBag.degerler = deger;
            IEnumerable<SelectListItem> inf = (from i in db.ErrorTypes.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.ErrorTypeExplanation,
                                            Value = i.ErrorTypeId.ToString()
                                        }).ToList();
            ViewBag.info= inf;
        

            return View();
        }

        [HttpPost]
        public ActionResult CreateError(ErrorInformation errorInformation, HttpPostedFileBase File)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateError");
            }
            if (File != null )
            {

                string image = Path.GetFileName(File.FileName);
                var path = Path.Combine(Server.MapPath("~/Image"), image);
                File.SaveAs(path);               
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
        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            db.Customer.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateError(int id)

        {
            var item = db.ErrorInformation.Find(id);
            IEnumerable<SelectListItem> inf = (from i in db.ErrorTypes.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.ErrorTypeExplanation,
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
            item.Company = errorInformation.Company;
            item.Customer.CustomerName = errorInformation.Customer.CustomerName;
            item.City.CityName = errorInformation.City.CityName;
            item.ErrorSummary = errorInformation.ErrorSummary;
            item.ErrorTId = errorInformation.ErrorTId;
            item.ErrorDetails = errorInformation.ErrorDetails;
            item.Image = errorInformation.Image;
            item.ErrorSolution = errorInformation.ErrorSolution;
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
    



