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
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPaciente,cedula,nombre,fechaNacimiento,peso,altura,foto,IdUsuario")] Paciente paciente)
        {
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
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPaciente,cedula,nombre,fechaNacimiento,peso,altura,foto,IdUsuario")] Paciente paciente)
        {
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
            // Validación: Si NO es Pepito, lo sacamos o le mostramos un mensaje de error
            if (User.Identity.Name != "pepito@yahoo.com" && User.Identity.Name != "pepito")
            {
                // Puedes mandarlo a una vista de error o retornar un texto de acceso denegado
                TempData["ErrorBorrar"] = "¡Acceso denegado! Solo el usuario Pepito puede eliminar registros de pacientes.";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
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
        public ActionResult DeleteConfirmed(int id)
        {
            // Doble validación de seguridad estricta al confirmar el POST
            if (User.Identity.Name != "pepito@yahoo.com" && User.Identity.Name != "pepito")
            {
                TempData["ErrorBorrar"] = "¡No tienes permisos para eliminar este registro!";
                return RedirectToAction("Index");
            }

            // Si es Pepito, ejecuta el borrado real de la base de datos
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
            db.SaveChanges();

            TempData["ExitoBorrar"] = "Registro eliminado con éxito.";
            return RedirectToAction("Index");
        }
    }
}
