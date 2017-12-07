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
    public class ProduktyController : Controller
    {
        private SklepEntities db = new SklepEntities();

        // GET: Produkty
        public ActionResult Index()
        {
            var produkty = db.Produkty.Include(p => p.Dostawcy).Include(p => p.Kategoria);
            return View(produkty.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Name)
        {
            if (Name == "")
            {
                var produkty = db.Produkty.Include(p => p.Dostawcy).Include(p => p.Kategoria);
                return View(produkty.ToList());
            }
            else
            {
                var produkty = from p in db.Produkty
                               join d in db.Dostawcy
                                on p.DostawcaId equals d.DostawcaId
                               join k in db.Kategoria on p.KategoriaId equals k.KategoriaId
                               where p.NazwaProduktu.Contains(Name)
                               select p;

                return View(produkty.ToList());
            }



        }

        // GET: Produkty/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return HttpNotFound();
            }
            return View(produkty);
        }

        // GET: Produkty/Create
        public ActionResult Create()
        {
            ViewBag.DostawcaId = new SelectList(db.Dostawcy, "DostawcaId", "NazwaFirmy");
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "KategoriaId", "NazwaKategorii");
            return View();
        }

        // POST: Produkty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProduktId,KategoriaId,DostawcaId," +
            "NazwaProduktu,IloscNaJednostke,CenaJednostkowa,LiczbaProduktow,Opis")] Produkty produkty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Produkty.Add(produkty);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["errorAlert"] = "<script>alert('Wystąpił błąd podczas wprowadzania danych');</script>";
                    TempData["dbAlert"] = e.GetBaseException().Message;
                    return RedirectToAction("Create");
                }
            }

            ViewBag.DostawcaId = new SelectList(db.Dostawcy, "DostawcaId", "NazwaFirmy", produkty.DostawcaId);
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "KategoriaId", "NazwaKategorii", produkty.KategoriaId);
            return View(produkty);
        }

        // GET: Produkty/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return HttpNotFound();
            }
            ViewBag.DostawcaId = new SelectList(db.Dostawcy, "DostawcaId", "NazwaFirmy", produkty.DostawcaId);
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "KategoriaId", "NazwaKategorii", produkty.KategoriaId);
            return View(produkty);
        }

        // POST: Produkty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProduktId,KategoriaId,DostawcaId," +
            "NazwaProduktu,IloscNaJednostke,CenaJednostkowa,LiczbaProduktow,Opis")] Produkty produkty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(produkty).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["errorAlert"] = "<script>alert('Wystąpił błąd podczas wprowadzania danych" +
                                ", wprowadzono wartości ujemne lub podana nazwa produktu już istnieje.');</script>";
                    return RedirectToAction("Edit");
                }

            }
            ViewBag.DostawcaId = new SelectList(db.Dostawcy, "DostawcaId", "NazwaFirmy", produkty.DostawcaId);
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "KategoriaId", "NazwaKategorii", produkty.KategoriaId);
            return View(produkty);
        }

        // GET: Produkty/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return HttpNotFound();
            }
            return View(produkty);
        }

        // POST: Produkty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produkty produkty = db.Produkty.Find(id);
            db.Produkty.Remove(produkty);
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
