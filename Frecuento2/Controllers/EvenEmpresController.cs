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
    public class EvenEmpresController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();

        // GET: EvenEmpres
        public ActionResult Index()
        {
            //var evenEmpre = db.EvenEmpre.Include(e => e.Empresa).Include(e => e.Evento);
           return View();
        }

        // GET: EvenEmpres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvenEmpre evenEmpre = db.EvenEmpre.Find(id);
            if (evenEmpre == null)
            {
                return HttpNotFound();
            }
            return View(evenEmpre);
        }

        // GET: EvenEmpres/Create
        public ActionResult Create()
        {
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            return View();
        }

        // POST: EvenEmpres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Empresa,Id_EvenEmpre")] EvenEmpre evenEmpre)
        {
            if (ModelState.IsValid)
            {
                db.EvenEmpre.Add(evenEmpre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", evenEmpre.Id_Empresa);
            return View(evenEmpre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromCompany(EvenEmpre evenEmpre)
        {
            if (ModelState.IsValid)
            {
                if (evenEmpre.Id_EvenEmpre > 0)
                {
                    var data = db.EvenEmpre.Find(evenEmpre.Id_EvenEmpre);
                    data.Precio_Base = evenEmpre.Precio_Base;
                    data.Id_Tipo_Evento = evenEmpre.Id_Tipo_Evento;
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.EvenEmpre.Add(evenEmpre);
                    db.SaveChanges();
                }

                return RedirectToAction("Details", "Empresas", new { id = evenEmpre.Id_Empresa });
            }

            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", evenEmpre.Id_Empresa);
            return RedirectToAction("Details", "Empresas",new { id=evenEmpre.Id_Empresa });
        }

        [HttpPost]
        public ActionResult DeleteFromCompany(int evenEmpId,int compId)
        {
            try
            {
                EvenEmpre evenEmpre = db.EvenEmpre.Find(evenEmpId);
                db.EvenEmpre.Remove(evenEmpre);
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Details", "Empresas", new { id = compId });
        }

        // GET: EvenEmpres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvenEmpre evenEmpre = db.EvenEmpre.Find(id);
            if (evenEmpre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", evenEmpre.Id_Empresa);
            return View(evenEmpre);
        }

        // POST: EvenEmpres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Evento,Id_Empresa,Id_EvenEmpre")] EvenEmpre evenEmpre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenEmpre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", evenEmpre.Id_Empresa);
            return View(evenEmpre);
        }

        // GET: EvenEmpres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvenEmpre evenEmpre = db.EvenEmpre.Find(id);
            if (evenEmpre == null)
            {
                return HttpNotFound();
            }
            return View(evenEmpre);
        }

        // POST: EvenEmpres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvenEmpre evenEmpre = db.EvenEmpre.Find(id);
            db.EvenEmpre.Remove(evenEmpre);
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
