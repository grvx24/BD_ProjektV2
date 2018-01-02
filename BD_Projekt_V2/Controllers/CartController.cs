using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BD_Projekt_V2.Controllers
{
    [Authorize(Roles ="Klient")]
    public class CartController : Controller
    {
        private SklepEntities db = new SklepEntities();

        public ActionResult Index()
        {
            var userId = (from u in db.Klienci where u.Login == User.Identity.Name select u.KlientId).FirstOrDefault();

            var result = from p in db.KoszykView where p.KlientId == userId select p;
            return View(result.ToList());
        }


        public ActionResult Delete(int id)
        {
            var user = db.Klienci.FirstOrDefault(k => k.Login == User.Identity.Name);

            var itemToDelete = (from i in db.Koszyk_Przedmiot where i.KlientId == user.KlientId select i).FirstOrDefault();

            db.Koszyk_Przedmiot.Remove(itemToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult ConfirmOrder()
        {
            var userId = (from u in db.Klienci where u.Login == User.Identity.Name select u.KlientId).FirstOrDefault();

            var result = from p in db.KoszykView where p.KlientId == userId select p;
            return View(result.ToList());
        }


        public ActionResult CreateOrder()
        {
            try
            {
                var userId = (from u in db.Klienci where u.Login == User.Identity.Name select u.KlientId).FirstOrDefault();

                var cart = db.KoszykView.Where(k => k.KlientId == userId);
                if(!cart.Any())
                {
                    TempData["Order"] = "Brak elementów w koszyku";
                    return RedirectToAction("Index");
                }
                db.AddZamowienie(userId);
                db.SaveChanges();
                var orderId = db.Zamowienia.FirstOrDefault(k => k.KlientId == userId).ZamowienieId;

                db.MoveItemsFromCartToOrder(userId, orderId);
                db.SaveChanges();

                TempData["Order"] = "Zamówienie zostałe złożone";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Error"] = e.GetBaseException().Message;
                return RedirectToAction("Index");
            }
        }


    }
}