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
    public class SzczegolyZamowieniaController : Controller
    {
        private SklepEntities db = new SklepEntities();

        // GET: SzczegolyZamowienia
        public ActionResult Index()
        {
            var szczegolyZamowienia = db.SzczegolyZamowienia.Include(s => s.Produkty).Include(s => s.Zamowienia);
            return View(szczegolyZamowienia.ToList());
        }

        // GET: SzczegolyZamowienia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SzczegolyZamowienia szczegolyZamowienia = db.SzczegolyZamowienia.Find(id);
            if (szczegolyZamowienia == null)
            {
                return HttpNotFound();
            }
            return View(szczegolyZamowienia);
        }

        // GET: SzczegolyZamowienia/Create
        public ActionResult Create()
        {
            ViewBag.ProduktId = new SelectList(db.Produkty, "ProduktId", "NazwaProduktu");
            ViewBag.ZamowienieId = new SelectList(db.Zamowienia, "ZamowienieId", "StatusZamowienia");
            return View();
        }

        // POST: SzczegolyZamowienia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SzczegolyZamId,ZamowienieId,ProduktId,Cena,Ilosc,Rabat")] SzczegolyZamowienia szczegolyZamowienia)
        {
            if (ModelState.IsValid)
            {
                db.SzczegolyZamowienia.Add(szczegolyZamowienia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProduktId = new SelectList(db.Produkty, "ProduktId", "NazwaProduktu", szczegolyZamowienia.ProduktId);
            ViewBag.ZamowienieId = new SelectList(db.Zamowienia, "ZamowienieId", "StatusZamowienia", szczegolyZamowienia.ZamowienieId);
            return View(szczegolyZamowienia);
        }

        // GET: SzczegolyZamowienia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SzczegolyZamowienia szczegolyZamowienia = db.SzczegolyZamowienia.Find(id);
            if (szczegolyZamowienia == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProduktId = new SelectList(db.Produkty, "ProduktId", "NazwaProduktu", szczegolyZamowienia.ProduktId);
            ViewBag.ZamowienieId = new SelectList(db.Zamowienia, "ZamowienieId", "StatusZamowienia", szczegolyZamowienia.ZamowienieId);
            return View(szczegolyZamowienia);
        }

        // POST: SzczegolyZamowienia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SzczegolyZamId,ZamowienieId,ProduktId,Cena,Ilosc,Rabat")] SzczegolyZamowienia szczegolyZamowienia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(szczegolyZamowienia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProduktId = new SelectList(db.Produkty, "ProduktId", "NazwaProduktu", szczegolyZamowienia.ProduktId);
            ViewBag.ZamowienieId = new SelectList(db.Zamowienia, "ZamowienieId", "StatusZamowienia", szczegolyZamowienia.ZamowienieId);
            return View(szczegolyZamowienia);
        }

        // GET: SzczegolyZamowienia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SzczegolyZamowienia szczegolyZamowienia = db.SzczegolyZamowienia.Find(id);
            if (szczegolyZamowienia == null)
            {
                return HttpNotFound();
            }
            return View(szczegolyZamowienia);
        }

        // POST: SzczegolyZamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SzczegolyZamowienia szczegolyZamowienia = db.SzczegolyZamowienia.Find(id);
            db.SzczegolyZamowienia.Remove(szczegolyZamowienia);
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
