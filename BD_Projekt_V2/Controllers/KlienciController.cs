using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD_Projekt_V2;

namespace BD_Projekt_V2.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KlienciController : Controller
    {
        private SklepEntities db = new SklepEntities();

        // GET: Klienci
        public ActionResult Index()
        {
            return View(db.Klienci.ToList());
        }

        // GET: Klienci/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // GET: Klienci/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klienci/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KlientId,Login,Haslo,Imie,Nazwisko,Firma,NIP,REGON,Tel_1,Tel_2,Fax,Email,WWW,Kraj,Region,Miasto,KodPocztowy")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.Klienci.Add(klienci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klienci);
        }

        // GET: Klienci/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Klienci/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KlientId,Login,Haslo,Imie,Nazwisko,Firma,NIP,REGON,Tel_1,Tel_2,Fax,Email,WWW,Kraj,Region,Miasto,KodPocztowy")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klienci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klienci);
        }

        // GET: Klienci/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = db.Klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Klienci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klienci klienci = db.Klienci.Find(id);
            db.Klienci.Remove(klienci);
            db.SaveChanges();
            return RedirectToAction("Index");
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
