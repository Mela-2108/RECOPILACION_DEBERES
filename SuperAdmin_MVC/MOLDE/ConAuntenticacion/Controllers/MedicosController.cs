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
    public class MedicosController : Controller
    {
        private UnificadaBD_Context db = new UnificadaBD_Context();

        // GET: Medicos
        public ActionResult Index()
        {
            var medico = db.Medico.Include(m => m.AspNetUsers).Include(m => m.Clinica).Include(m => m.Especialidad);
            return View(medico.ToList());
        }

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.codClinica = new SelectList(db.Clinica, "codClinica", "nombre");
            ViewBag.codEspecialidad = new SelectList(db.Especialidad, "codEspecialidad", "nombre");
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codMedico,nombre,horaInicio,horaFin,codEspecialidad,codClinica,IdUsuario")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Medico.Add(medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email", medico.IdUsuario);
            ViewBag.codClinica = new SelectList(db.Clinica, "codClinica", "nombre", medico.codClinica);
            ViewBag.codEspecialidad = new SelectList(db.Especialidad, "codEspecialidad", "nombre", medico.codEspecialidad);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email", medico.IdUsuario);
            ViewBag.codClinica = new SelectList(db.Clinica, "codClinica", "nombre", medico.codClinica);
            ViewBag.codEspecialidad = new SelectList(db.Especialidad, "codEspecialidad", "nombre", medico.codEspecialidad);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codMedico,nombre,horaInicio,horaFin,codEspecialidad,codClinica,IdUsuario")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.AspNetUsers, "Id", "Email", medico.IdUsuario);
            ViewBag.codClinica = new SelectList(db.Clinica, "codClinica", "nombre", medico.codClinica);
            ViewBag.codEspecialidad = new SelectList(db.Especialidad, "codEspecialidad", "nombre", medico.codEspecialidad);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Medico.Find(id);
            db.Medico.Remove(medico);
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
