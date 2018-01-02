using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BD_Projekt_V2.Controllers
{
    public class UserController : Controller
    {
        private SklepEntities db = new SklepEntities();

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
                    FormsAuthentication.SetAuthCookie(admin.Login, false);

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


        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "KlientId,Login,Haslo,Imie,Nazwisko,Firma,NIP,REGON,Tel_1,Tel_2,Fax,Email,WWW,Kraj,Region,Miasto,KodPocztowy")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                using (SklepEntities db = new SklepEntities())
                {
                    try
                    {
                        db.AddKlient(klienci.Login,klienci.Haslo,klienci.Imie,klienci.Nazwisko,
                            klienci.Firma,klienci.Tel_1,klienci.Tel_2,klienci.Fax,klienci.Email,klienci.WWW,klienci.Kraj,klienci.Region,
                            klienci.Miasto,klienci.KodPocztowy);
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }catch (SqlException ex)
                    {
                        TempData["dbAlert"] = ex.GetBaseException().Message;
                        return View(klienci);
                    }catch(Exception e)
                    {
                        TempData["dbAlert"] = e.GetBaseException().Message;
                        return View(klienci);
                    }

                }
            }

            return View(klienci);
        }

        [Authorize(Roles="Klient")]
        public ActionResult UserInfo()
        {
            string login = User.Identity.Name;

            Klienci klient = (from k in db.Klienci where k.Login == login select k).FirstOrDefault();

            return View(klient);
        }


        [HttpGet]
        public ActionResult EditUserInfo()
        {
            string login = User.Identity.Name;

            Klienci klient = (from k in db.Klienci where k.Login == login select k).FirstOrDefault();
            //widok na edycje

            return View(klient);
        }

        [HttpPost]
        public ActionResult EditUserInfo([Bind(Include ="KlientId,Login,Haslo,Imie,Nazwisko,Firma,NIP,REGON,Tel_1,Tel_2,Fax,Email,WWW,Kraj,Region,Miasto,KodPocztowy")] Klienci klient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(klient).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UserInfo");
                }

                return View(klient);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.GetBaseException().Message;
                return View(klient);
            }

        }

        public ActionResult UserOrders()
        {
            string login = User.Identity.Name;
            var klient = (from k in db.Klienci where k.Login == login select k.KlientId).FirstOrDefault();

            var orders = from o in db.Zamowienia where o.KlientId == klient select o;
            //historia zamowien, anulowanie zamowien niezrealizowanych

            return View(orders);
        }

        [HttpPost]
        public ActionResult DeleteOrder(int Id)
        {
            try
            {
                Zamowienia zamowienia = db.Zamowienia.Find(Id);
                string login = User.Identity.Name;
                var klient = (from k in db.Klienci where k.Login == login select k.KlientId).FirstOrDefault();
                if (zamowienia.KlientId == klient)
                {
                    db.Zamowienia.Remove(zamowienia);
                    db.SaveChanges();
                }

                return RedirectToAction("UserOrders");
                
            }
            catch (Exception e)
            {
                TempData["Error"] = e.GetBaseException().Message;
                return RedirectToAction("UserOrders");
            }
        }
        [HttpPost]
        public ActionResult OrderDetails(int? Id)
        {
            var result = db.SzczegolyZamowieniaView.Where(z => z.ZamowienieId == Id);


            return View(result.ToList());
        }

    }
}