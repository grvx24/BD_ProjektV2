using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD_Projekt_V2;
using System.Data.Entity.Validation;

namespace BD_Projekt_V2.Controllers
{
    public class PracownicyController : Controller
    {
        private SklepEntities db = new SklepEntities();

        // GET: Pracownicy
        public ActionResult Index()
        {
            return View(db.Pracownicy.ToList());
        }

        // GET: Pracownicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // GET: Pracownicy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pracownicy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PracownikId,Login,Haslo,Imie,Nazwisko,Uprawnienia,Tel_1,Tel_2,Fax,Email,WWW,Kraj,Region,Miasto,KodPocztowy")] Pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.AddPracownik(pracownicy.Login, pracownicy.Haslo, pracownicy.Imie, pracownicy.Nazwisko, pracownicy.Uprawnienia, pracownicy.Tel_1,
                        pracownicy.Tel_2, pracownicy.Fax, pracownicy.Email, pracownicy.WWW, pracownicy.Kraj, pracownicy.Region, pracownicy.Miasto,
                        pracownicy.KodPocztowy);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                    var fullMessage = string.Join("; ", errorMessages);
                    TempData["dbAlert"] = fullMessage;
                    return View(pracownicy);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    TempData["dbAlert"] = ex.InnerException.Message;
                    return View(pracownicy);
                }
                catch (Exception e)
                {
                    TempData["dbAlert"] = e.GetBaseException().Message;
                    return View(pracownicy);
                }

            }

            return View(pracownicy);
        }

        // GET: Pracownicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: Pracownicy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PracownikId,Login,Haslo,Imie,Nazwisko,Uprawnienia,Tel_1,Tel_2,Fax,Email,WWW,Kraj,Region,Miasto,KodPocztowy")] Pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ModifyPracownik(pracownicy.PracownikId,pracownicy.Login, pracownicy.Haslo, pracownicy.Imie, pracownicy.Nazwisko, pracownicy.Uprawnienia, pracownicy.Tel_1,
                        pracownicy.Tel_2, pracownicy.Fax, pracownicy.Email, pracownicy.WWW, pracownicy.Kraj, pracownicy.Region, pracownicy.Miasto,
                        pracownicy.KodPocztowy);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                    var fullMessage = string.Join("; ", errorMessages);
                    TempData["dbAlert"] = fullMessage;
                    return View(pracownicy);
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    TempData["dbAlert"] = ex.InnerException.Message;
                    return View(pracownicy);
                }
                catch (Exception e)
                {
                    TempData["dbAlert"] = e.GetBaseException().Message;
                    return View(pracownicy);
                }


            }
            return View(pracownicy);
        }

        // GET: Pracownicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: Pracownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pracownicy pracownicy = db.Pracownicy.Find(id);
            int returnValue=0;
            try
            {
                //db.Pracownicy.Remove(pracownicy);
                returnValue=db.DeletePracownik(id.ToString());
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var fullMessage = string.Join("; ", errorMessages);
                TempData["dbAlert"] = fullMessage;
                return View(pracownicy);
            }
            catch(System.Data.Entity.Core.EntityCommandExecutionException ex)
            {
                TempData["dbAlert"] = ex.InnerException.Message;
                return View(pracownicy);
            }
            catch(Exception e)
            {
                TempData["dbAlert"] = e.GetBaseException();
                return View(pracownicy);
            }

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
