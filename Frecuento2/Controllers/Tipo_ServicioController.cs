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
    public class Tipo_ServicioController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();

        // GET: Servicios
        public ActionResult Index()
        {
            return View(db.Tipo_Servicio.ToList());
        }

        // GET: Servicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Servicio TipoServicio = db.Tipo_Servicio.Find(id);
            if (TipoServicio == null)
            {
                return HttpNotFound();
            }
            return View(TipoServicio);
        }

        // GET: Servicios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tipo_Servicio,Descripción")] Tipo_Servicio TipoServicio)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Servicio.Add(TipoServicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(TipoServicio);
        }

        // GET: Servicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Servicio TipoServicio = db.Tipo_Servicio.Find(id);
            if (TipoServicio == null)
            {
                return HttpNotFound();
            }
            return View(TipoServicio);
        }

        // POST: Servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Servicio,Descripción")] Tipo_Servicio TipoServicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TipoServicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TipoServicio);
        }

        // GET: Servicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Tipo_Servicio TipoServicio = db.Tipo_Servicio.Find(id);
            if (TipoServicio == null)
            {
                return HttpNotFound();
            }
            return View(TipoServicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Servicio TipoServicio = db.Tipo_Servicio.Find(id);
            db.Tipo_Servicio.Remove(TipoServicio);
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