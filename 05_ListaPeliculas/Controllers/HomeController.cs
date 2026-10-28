using PyoyectoTest.Services;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PyoyectoTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Pelicula()
        {
            ViewBag.Message = "Lista de Peliculas";

            var objPelicula = new PeliculasService();
            //var model = objPelicula.ObtenerPelicula();
            var model = objPelicula.obtenerPeliculas();


            return View(model);

           
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}