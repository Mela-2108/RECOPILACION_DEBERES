using ConAuntenticacion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConAuntenticacion.Controllers
{
    [Authorize]
    public class PacientesController : Controller
    {
        private UnificadaBD_Context db = new UnificadaBD_Context();

        // GET: Pacientes
        public ActionResult Index()
        {
            var paciente = db.Paciente.Include(p => p.AspNetUsers);
            return View(paciente.ToList());
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create

        [Authorize(Roles = "edit")]
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "create")]
        public ActionResult Create([Bind(Include = "codPaciente,cedula,nombre,fechaNacimiento,peso,altura,foto,IdUsuario")] Paciente paciente)
        {
            // Tratamiento exclusivo para la altura (punto/coma y redondeo limpio en double)
            string alturaStr = Request.Form["altura"];
            if (!string.IsNullOrEmpty(alturaStr))
            {
                alturaStr = alturaStr.Replace(',', '.');
                double valAltura;
                if (double.TryParse(alturaStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valAltura))
                {
                    paciente.altura = Math.Round(valAltura, 2);
                }
            }

            ModelState.Remove("altura");

            if (ModelState.IsValid)
            {
                db.Paciente.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email", paciente.IdUsuario);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email", paciente.IdUsuario);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPaciente,cedula,nombre,fechaNacimiento,peso,altura,foto,IdUsuario")] Paciente paciente)
        {
            // Tratamiento exclusivo para la altura al editar
            string alturaStr = Request.Form["altura"];
            if (!string.IsNullOrEmpty(alturaStr))
            {
                alturaStr = alturaStr.Replace(',', '.');
                double valAltura;
                if (double.TryParse(alturaStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valAltura))
                {
                    paciente.altura = Math.Round(valAltura, 2);
                }
            }

            ModelState.Remove("altura");

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email", paciente.IdUsuario);
            return View(paciente);
        }
        // GET: Pacientes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            // Validación: Si NO es Pepito (ni por correo ni por nombre), lo regresamos al Index
            if (User.Identity.Name != "pepito@yahoo.com" && User.Identity.Name != "PEPITO")
            {
                TempData["ErrorBorrar"] = "¡Acceso denegado! Solo el usuario Pepito puede eliminar registros de pacientes.";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            // Doble validación de seguridad por si intentan saltarse el paso
            if (User.Identity.Name != "pepito@yahoo.com" && User.Identity.Name != "PEPITO")
            {
                TempData["ErrorBorrar"] = "¡Acceso denegado! Solo el usuario Pepito puede eliminar registros de pacientes.";
                return RedirectToAction("Index");
            }

            Paciente paciente = db.Paciente.Find(id);
            if (paciente != null)
            {
                db.Paciente.Remove(paciente);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
