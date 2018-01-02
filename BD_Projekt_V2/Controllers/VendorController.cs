using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BD_Projekt_V2.Controllers
{
    [Authorize(Roles ="Sprzedawca")]
    public class VendorController : Controller
    {

        SklepEntities db = new SklepEntities();
        public ActionResult Index()
        {
            var vendorId = (from p in db.Pracownicy where p.Login == User.Identity.Name select p.PracownikId).FirstOrDefault();

            var orders = from z in db.Zamowienia where z.PracownikId == vendorId select z;


            return View(orders.ToList());
        }

        [HttpPost]
        public ActionResult ChangeOrder(int id)
        {
            try
            {
                var vendorId = (from p in db.Pracownicy where p.Login == User.Identity.Name select p.PracownikId).FirstOrDefault();

                var orders = from z in db.Zamowienia where z.PracownikId == vendorId select z;

                var orderToModify = orders.Where(z => z.ZamowienieId == id).FirstOrDefault();
                orderToModify.StatusZamowienia = "Zrealizowane";

                db.Entry(orderToModify).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            catch(Exception e)
            {
                TempData["Error"] = e.GetBaseException().Message;
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                var vendorId = (from p in db.Pracownicy where p.Login == User.Identity.Name select p.PracownikId).FirstOrDefault();

                var orders = from z in db.Zamowienia where z.PracownikId == vendorId select z;

                var orderToDelete = orders.Where(z => z.ZamowienieId == id).FirstOrDefault();

                db.Zamowienia.Remove(orderToDelete);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                TempData["Error"] = e.GetBaseException().Message;
            }


            return RedirectToAction("Index");
        }

    }
}