using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConAutenticacion.Models;

namespace ConAutenticacion.Controllers
{
    public class ConsultasController : Controller
    {
        private PacienteDB_FotoEntities db = new PacienteDB_FotoEntities();

        // GET: Consultas
        public ActionResult Index()
        {
            var consultas = db.Consultas.Include(c => c.Medico).Include(c => c.Paciente);
            return View(consultas.ToList());
        }

        // GET: Consultas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            ViewBag.codMedico = new SelectList(db.Medicos, "codMedico", "nombre");
            ViewBag.codPaciente = new SelectList(db.Pacientes, "codPaciente", "cedula");
            return View();
        }

        // POST: Consultas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoConsulta,fecha,horaInicio,horaFin,codPaciente,codMedico")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Consultas.Add(consulta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codMedico = new SelectList(db.Medicos, "codMedico", "nombre", consulta.codMedico);
            ViewBag.codPaciente = new SelectList(db.Pacientes, "codPaciente", "cedula", consulta.codPaciente);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.codMedico = new SelectList(db.Medicos, "codMedico", "nombre", consulta.codMedico);
            ViewBag.codPaciente = new SelectList(db.Pacientes, "codPaciente", "cedula", consulta.codPaciente);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoConsulta,fecha,horaInicio,horaFin,codPaciente,codMedico")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consulta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codMedico = new SelectList(db.Medicos, "codMedico", "nombre", consulta.codMedico);
            ViewBag.codPaciente = new SelectList(db.Pacientes, "codPaciente", "cedula", consulta.codPaciente);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consulta consulta = db.Consultas.Find(id);
            db.Consultas.Remove(consulta);
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
