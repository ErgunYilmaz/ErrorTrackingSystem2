﻿using ErrorTrackingSystem.Models.Entity;
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
        MistakeTrackingSystemEntities2 db = new MistakeTrackingSystemEntities2();
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
                Session["EMail"] = information.Email.ToString();
                Session["Name"] = information.Name.ToString();
                Session["Surname"] = information.Surname.ToString();
                return RedirectToAction("Index", "Home");
               
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