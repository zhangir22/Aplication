using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Account.Context;
using Account.Models;

namespace Account.Controllers
{
    public class InputController : Controller
    {
        private UserContext db = new UserContext();

        public ActionResult List() 
        {
            return View(db.Users.ToList());
        }
        public ActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user) 
        {
            if (ModelState.IsValid)
            {
                var temp = db.Users.FirstOrDefault(u => u.Nickname == user.Nickname
                && u.Email == user.Email
                && u.Password == user.Password);
                if (temp != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.Nickname, true);
                    FormsAuthentication.SetAuthCookie(user.Nickname, true);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Такого пользователя нет");
                    return View("Create");
                }
            }
            else 
            {
                return View("Create");
            }
        }
      
        public ActionResult Delete(int id) 
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Logout();
        }
        // GET: Input/Details/5
        [Authorize]
        public ActionResult Details()
        {
            if (User.Identity.Name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.FirstOrDefault(u => u.Nickname == User.Identity.Name);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Input/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Input/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nickname,Email,Password,ConfirmPassword")] RegisterModel user)
        {
            User _user = null;
            User newUser = new User();
            if (ModelState.IsValid)
            {
                using (UserContext db = new UserContext()) 
                {
                    _user = db.Users.FirstOrDefault(u => u.Nickname == user.Nickname && u.Email == user.Email);
                }
                if (_user == null) 
                {
                    if (user.Password == user.ConfirmPassword)
                    {

                        newUser.Id = user.Id;
                        newUser.Nickname = user.Nickname;
                        newUser.Password = user.Password;
                        newUser.Email = user.Email;
                        using (UserContext db = new UserContext())
                        {
                            db.Users.Add(newUser);
                            db.SaveChanges();
                            newUser = db.Users.Where(u => u.Nickname == user.Nickname && u.Email == user.Email).FirstOrDefault();
                        }
                        if (newUser != null)
                        {
                            FormsAuthentication.RedirectFromLoginPage(newUser.Nickname, true);
                            FormsAuthentication.SetAuthCookie(newUser.Nickname, true);
                            return RedirectToAction("Details");

                        }
                    }
                    else 
                    {
                        ModelState.AddModelError("", "Ошибка при регистрации");
                    }
                }
              
            }  
             else
             {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
             }
                return View(user);

            
        }

        // GET: Input/Edit/5
       
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult ChangeName() 
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [AllowAnonymous]
        public ActionResult ChangeName(NicknameChange newName) 
        {
            User _user = null;
            if (ModelState.IsValid) 
            {
                _user = db.Users.FirstOrDefault(u => u.Nickname == newName.OldName);
                if (_user != null)
                {
                    _user.Nickname = newName.NewName;
                    db.Entry(_user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином нету");

                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nickname,Email,Password,ConfirmPassword")] RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                User _user = null;
                if (user.Password == user.ConfirmPassword)
                {
                    _user = db.Users.FirstOrDefault(u => u.Nickname == user.Nickname );
                    _user.Password = user.Password;
                    db.Entry(_user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else 
                {
                    ModelState.AddModelError("", "Пароли не совпадют");

                }

            }
            return View(user);
        }

        // GET: Input/Delete/5
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Input");
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
