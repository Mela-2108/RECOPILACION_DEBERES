using ConAuntenticacion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Con_Autenticacion.Controllers
{
    public class AlumnoController : Controller
    {
        private AlumnosDBContext db = new AlumnosDBContext();

        // GET: Alumno
        public ActionResult Index()
        {
            var alumno = db.Alumno.Include(a => a.Carrera);
            return View(alumno.ToList());
        }

        // GET: Alumno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "codCarrera", "descripcion");
            return View();
        }

        // POST: Alumno/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Apellidos,Edad,Genero,CodigoCarrera,FechaNacimiento,Foto")] Alumno alumno)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Alumno.Add(alumno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "codCarrera", "descripcion", alumno.CodigoCarrera);
            return View(alumno);
        }
                    catch (Exception ex)
        {
                // Manejar la excepción
                ModelState.AddModelError("", "Ocurrió un error al crear el alumno: " + ex.Message);
                ViewBag.CodigoCarrera = new SelectList(db.Carrera, "codCarrera", "descripcion", alumno.CodigoCarrera);
                return View(alumno);
            }
        }

        // GET: Alumno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "codCarrera", "descripcion", alumno.CodigoCarrera);
            return View(alumno);
        }

        // POST: Alumno/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Apellidos,Edad,Genero,CodigoCarrera,FechaNacimiento,Foto")] Alumno alumno)
        {
            try {  
            if (ModelState.IsValid)
            {
                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "codCarrera", "descripcion", alumno.CodigoCarrera);
            return View(alumno);
        }
                    catch (Exception ex)
        {
                // Manejar la excepción
                ModelState.AddModelError("", "Ocurrió un error al editar el alumno: " + ex.Message);
                ViewBag.CodigoCarrera = new SelectList(db.Carrera, "codCarrera", "descripcion", alumno.CodigoCarrera);
                return View(alumno);
            }
        }

        // GET: Alumno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alumno alumno = db.Alumno.Find(id);
            db.Alumno.Remove(alumno);
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