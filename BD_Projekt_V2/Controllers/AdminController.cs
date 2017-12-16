using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BD_Projekt_V2.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private SklepEntities db = new SklepEntities();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminsList()
        {
            //przykład widoku
            var admins = db.Administratorzy.ToList();
            return View(admins);
        }

    }
}