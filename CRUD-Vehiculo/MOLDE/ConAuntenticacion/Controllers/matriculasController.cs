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
    public class matriculasController : Controller
    {
        private MatriculacionFinalBD_NewEntities db = new MatriculacionFinalBD_NewEntities();

        // GET: matriculas
        public ActionResult Index()
        {
            var matricula = db.matricula.Include(m => m.agencia1).Include(m => m.vehiculo1);
            return View(matricula.ToList());
        }

        // GET: matriculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matricula matricula = db.matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // GET: matriculas/Create
        public ActionResult Create()
        {
            ViewBag.agencia = new SelectList(db.agencia, "id", "descripcion");
            ViewBag.vehiculo = new SelectList(db.vehiculo, "id", "placa");
            return View();
        }

        // POST: matriculas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fecha,vehiculo,agencia,anio")] matricula matricula)
        {
            if (ModelState.IsValid)
            {
                db.matricula.Add(matricula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.agencia = new SelectList(db.agencia, "id", "descripcion", matricula.agencia);
            ViewBag.vehiculo = new SelectList(db.vehiculo, "id", "placa", matricula.vehiculo);
            return View(matricula);
        }

        // GET: matriculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matricula matricula = db.matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            ViewBag.agencia = new SelectList(db.agencia, "id", "descripcion", matricula.agencia);
            ViewBag.vehiculo = new SelectList(db.vehiculo, "id", "placa", matricula.vehiculo);
            return View(matricula);
        }

        // POST: matriculas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fecha,vehiculo,agencia,anio")] matricula matricula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matricula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.agencia = new SelectList(db.agencia, "id", "descripcion", matricula.agencia);
            ViewBag.vehiculo = new SelectList(db.vehiculo, "id", "placa", matricula.vehiculo);
            return View(matricula);
        }

        // GET: matriculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matricula matricula = db.matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // POST: matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            matricula matricula = db.matricula.Find(id);
            db.matricula.Remove(matricula);
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
