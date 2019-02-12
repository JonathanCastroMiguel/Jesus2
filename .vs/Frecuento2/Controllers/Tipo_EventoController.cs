using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Frecuento2.Models;

namespace Frecuento2.Controllers
{
    public class Tipo_EventoController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();

        // GET: Tipo_Evento
        public ActionResult Index()
        {
            return View(db.Tipo_Evento.ToList());
        }

        // GET: Tipo_Evento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Evento tipo_Evento = db.Tipo_Evento.Find(id);
            if (tipo_Evento == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Evento);
        }

        // GET: Tipo_Evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Evento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tipo_Evento,Descripción")] Tipo_Evento tipo_Evento)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Evento.Add(tipo_Evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Evento);
        }

        // GET: Tipo_Evento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Evento tipo_Evento = db.Tipo_Evento.Find(id);
            if (tipo_Evento == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Evento);
        }

        // POST: Tipo_Evento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Tipo_Evento,Descripción")] Tipo_Evento tipo_Evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Evento);
        }

        // GET: Tipo_Evento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Evento tipo_Evento = db.Tipo_Evento.Find(id);
            if (tipo_Evento == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Evento);
        }

        // POST: Tipo_Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Evento tipo_Evento = db.Tipo_Evento.Find(id);
            db.Tipo_Evento.Remove(tipo_Evento);
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
