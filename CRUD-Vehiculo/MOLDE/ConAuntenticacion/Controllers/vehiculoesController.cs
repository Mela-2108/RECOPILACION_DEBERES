using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConAuntenticacion.Models;

namespace ConAuntenticacion.Controllers
{
    public class vehiculoesController : Controller
    {
        private MatriculacionFinalBD_NewEntities db = new MatriculacionFinalBD_NewEntities();

        // ----------------------------------------------------
        // FUNCIÓN REQUERIDA (Punto 7): Calcular el Avalúo
        // ----------------------------------------------------
        public decimal CalcularAvaluo(int anioVehiculo, string marca)
        {
            int yearActual = DateTime.Now.Year;
            decimal impuesto = 0;

            if (!string.IsNullOrEmpty(marca))
            {
                string m = marca.Trim().ToUpper();

                // Asignación de impuestos según el país de origen de la marca
                if (m == "1" || m == "ECUADOR")
                {
                    impuesto = 0;
                }
                else if (m == "2" || m == "JAPON")
                {
                    impuesto = 500;
                }
                else if (m == "3" || m == "USA")
                {
                    impuesto = 1000;
                }
                else
                {
                    impuesto = 2000; // Cualquier otro país
                }
            }

            // Fórmula corregida (sin el guion extraño y con la resta adecuada)
            return (1000 * (yearActual - anioVehiculo) + impuesto);
        }

        // GET: vehiculoes (Actualizado con los filtros)
        public ActionResult Index(string combustibleFiltro, string marcaFiltro)
        {
            var vehiculos = db.vehiculo.AsQueryable();

            // Filtro por combustible
            if (!string.IsNullOrEmpty(combustibleFiltro))
            {
                combustibleFiltro = combustibleFiltro.Trim();
                vehiculos = vehiculos.Where(v => v.combustible == combustibleFiltro);
            }

            // Filtro por marca o país
            if (!string.IsNullOrEmpty(marcaFiltro))
            {
                marcaFiltro = marcaFiltro.Trim();
                vehiculos = vehiculos.Where(v => v.marca.ToString() == marcaFiltro);
            }

            return View(vehiculos.ToList());
        }

        // GET: vehiculoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculo vehiculo = db.vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: vehiculoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vehiculoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,placa,marca,motor,chasis,combustible,anio,color,foto")] vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                // Calculamos el avalúo automáticamente antes de guardar
                vehiculo.avaluo = CalcularAvaluo(vehiculo.anio ?? 0, vehiculo.marca?.ToString());
                db.vehiculo.Add(vehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehiculo);
        }

        // GET: vehiculoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculo vehiculo = db.vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: vehiculoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,placa,marca,motor,chasis,combustible,anio,color,foto")] vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                // Recalculamos el avalúo al editar
                vehiculo.avaluo = CalcularAvaluo(vehiculo.anio ?? 0, vehiculo.marca?.ToString());
                db.Entry(vehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehiculo);
        }

        // GET: vehiculoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // RESTRICCIÓN: Si el usuario es ADM o AGENTE, no pueden eliminar -> Redirigimos al Index
            if (User.IsInRole("ADM") || User.IsInRole("AGENTE"))
            {
                TempData["Error"] = "No tienes autorización para eliminar registros.";
                return RedirectToAction("Index");
            }

            vehiculo vehiculo = db.vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Doble seguridad en el POST por si intentan forzar la acción
            if (User.IsInRole("ADM") || User.IsInRole("AGENTE"))
            {
                TempData["Error"] = "No tienes autorización para eliminar registros.";
                return RedirectToAction("Index");
            }

            vehiculo vehiculo = db.vehiculo.Find(id);
            db.vehiculo.Remove(vehiculo);
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