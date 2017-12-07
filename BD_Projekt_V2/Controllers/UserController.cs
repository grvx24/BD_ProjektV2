using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BD_Projekt_V2.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Login,Haslo")] Klienci user)
        {
            using (SklepEntities db = new SklepEntities())
            {
                bool userFound = false;
                var obj = db.Klienci.Where(a => a.Login.Equals(user.Login) && a.Haslo.Equals(user.Haslo)).FirstOrDefault();
                if (obj != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, false);

                    var authTicket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(20),
                        false, "Klient");
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    userFound = true;
                    return RedirectToAction("Index", "Home");
                }
                var admin = db.Pracownicy.Where(a => a.Login.Equals(user.Login) && a.Haslo.Equals(user.Haslo)).FirstOrDefault();
                if (admin != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, false);

                    var authTicket = new FormsAuthenticationTicket(1, admin.Login, DateTime.Now, DateTime.Now.AddMinutes(20),
                        false, admin.Uprawnienia);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    userFound = true;
                    return RedirectToAction("Index", "Home");
                }
                if (!userFound)
                {
                    ModelState.AddModelError("", "Niepoprawny login lub hasło.");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //GET User/Register
        public ActionResult Register()
        {
            return View();
        }


        // POST: Klienci/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "KlientId,Login,Haslo,Imie,Nazwisko,Firma,NIP,REGON,Tel_1,Tel_2,Fax,Email,WWW,Kraj,Region,Miasto,KodPocztowy")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                using (SklepEntities db = new SklepEntities())
                {
                    db.Klienci.Add(klienci);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }

            return View(klienci);
        }

    }
}