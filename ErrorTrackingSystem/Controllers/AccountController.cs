using ErrorTrackingSystem.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ErrorTrackingSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MistakeTrackingSystemEntities1 db = new MistakeTrackingSystemEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user) 
        {
            var information = db.User.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (information!=null)
            {
                FormsAuthentication.SetAuthCookie(information.Email, false);
                Session["Mail"] = information.Email.ToString();
                //Session["Ad"] = information.Name.ToString();
                //Session["Soyad"] = information.Surname.ToString();
            }
            else
            {
                ViewBag.error = "Kullanıcı adı veya şifre hatalı";
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login", "Account");
        }
    }
}