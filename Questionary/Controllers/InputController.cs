using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Questionary.Context;
using Questionary.Models;

namespace Questionary.Controllers
{
  //[Authorize]
    public class InputController : Controller
    {
        private UserContext db = new UserContext();

        // GET: Input
   
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(db.Users.ToList());
            }
            else 
            {
                return RedirectToAction("Create");
            }
           
        }
        // GET: Input/Create
        public ActionResult Create()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,Age")] User user)
        {
            User _user = null;
            if (ModelState.IsValid)
            {
              
                using (UserContext db = new UserContext())
                {
                    _user = db.Users.FirstOrDefault(u => u.Name == user.Name && u.LastName == user.LastName && u.Age == user.Age);

                }
                if (_user == null) 
                {
                    using (UserContext db = new UserContext()) 
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        user = db.Users.Where(u => u.Name == user.Name && u.LastName == user.LastName && u.Age == user.Age).FirstOrDefault();
                    }
                    //
                }
                if (user != null) 
                {
                    FormsAuthentication.RedirectFromLoginPage(user.Name, true);
                    FormsAuthentication.SetAuthCookie(user.Name, true);
                    return RedirectToAction("Index");
                }
               
                
            }
            else
            {
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            }
            return View(user);
        }

   
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
