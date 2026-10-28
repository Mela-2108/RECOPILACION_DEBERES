using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PyoyectoTest.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult VerRoles()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Integrantes()
        {
            ViewBag.Message = "PAGINA DE INTEGRANTES .......";

            return View();
        }

        public ActionResult Contacto()
        {
            ViewBag.Message = "PAGINA DE CONTACTO .......";

            return View();
        }
    }
}