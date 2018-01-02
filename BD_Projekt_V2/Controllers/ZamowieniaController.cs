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
    public class ZamowieniaController : Controller
    {
        private SklepEntities db = new SklepEntities();

        // GET: Zamowienia
        public ActionResult Index()
        {
            var zamowienia = db.Zamowienia.Include(z => z.Klienci).Include(z => z.Pracownicy);
            return View(zamowienia.ToList());
        }

        // GET: Zamowienia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            if (zamowienia == null)
            {
                return HttpNotFound();
            }
            return View(zamowienia);
        }

        // GET: Zamowienia/Create
        public ActionResult Create()
        {
            ViewBag.KlientId = new SelectList(db.Klienci, "KlientId", "Login");
            ViewBag.PracownikId = new SelectList(db.Pracownicy, "PracownikId", "Login");
            return View();
        }

        // POST: Zamowienia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZamowienieId,KlientId,PracownikId,DataZamowienia,StatusZamowienia")] Zamowienia zamowienia)
        {
            if (ModelState.IsValid)
            {
                db.Zamowienia.Add(zamowienia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlientId = new SelectList(db.Klienci, "KlientId", "Login", zamowienia.KlientId);
            ViewBag.PracownikId = new SelectList(db.Pracownicy, "PracownikId", "Login", zamowienia.PracownikId);
            return View(zamowienia);
        }

        // GET: Zamowienia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            if (zamowienia == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlientId = new SelectList(db.Klienci, "KlientId", "Login", zamowienia.KlientId);
            ViewBag.PracownikId = new SelectList(db.Pracownicy, "PracownikId", "Login", zamowienia.PracownikId);
            return View(zamowienia);
        }

        // POST: Zamowienia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZamowienieId,KlientId,PracownikId,DataZamowienia,StatusZamowienia")] Zamowienia zamowienia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zamowienia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlientId = new SelectList(db.Klienci, "KlientId", "Login", zamowienia.KlientId);
            ViewBag.PracownikId = new SelectList(db.Pracownicy, "PracownikId", "Login", zamowienia.PracownikId);
            return View(zamowienia);
        }

        // GET: Zamowienia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            if (zamowienia == null)
            {
                return HttpNotFound();
            }
            return View(zamowienia);
        }

        // POST: Zamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zamowienia zamowienia = db.Zamowienia.Find(id);
            db.Zamowienia.Remove(zamowienia);
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
