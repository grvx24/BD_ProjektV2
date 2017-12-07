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
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

    }
}