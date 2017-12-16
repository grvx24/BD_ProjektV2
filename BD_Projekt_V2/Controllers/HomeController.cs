using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BD_Projekt_V2.Controllers
{
    public class HomeController : Controller
    {
        private SklepEntities db = new SklepEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Categories()
        {
            ViewBag.Message = "Categories.";

            return View(db.Kategoria.ToList());
        }

        public ActionResult Products(int id)
        {
            ViewBag.Message = "Products";

            var products = from p in db.Produkty where p.KategoriaId.Equals(id) select p;

            return View(products.ToList());
        }
        [HttpPost]
        public ActionResult Products(string Name)
        {
            ViewBag.Message = "Products";

            var products = from p in db.Produkty
                           join k in db.Kategoria on p.KategoriaId equals k.KategoriaId
                           join d in db.Dostawcy on p.DostawcaId equals d.DostawcaId
                           where p.NazwaProduktu.Contains(Name) || k.NazwaKategorii.Contains(Name) || d.NazwaFirmy.Contains(Name)
                           select p;


            return View(products.ToList());
        }

        public ActionResult AddToCart(int Id)
        {
            var user = db.Klienci.FirstOrDefault(l => l.Login == User.Identity.Name);
            if (user != null)
            {
                Koszyk_Przedmiot cartItem = new Koszyk_Przedmiot { KlientId = user.KlientId, ProduktId = Id, LiczbaSztuk = 1 };

                db.Koszyk_Przedmiot.Add(cartItem);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


    }
}