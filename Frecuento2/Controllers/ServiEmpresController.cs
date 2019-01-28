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
    public class ServiEmpresController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();

        // GET: ServiEmpres
        public ActionResult Index()
        {
            var serviEmpre = db.ServiEmpre.Include(s => s.Empresa).Include(s => s.Tipo_Servicio);
            return View(serviEmpre.ToList());
        }

        // GET: ServiEmpres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiEmpre serviEmpre = db.ServiEmpre.Find(id);
            if (serviEmpre == null)
            {
                return HttpNotFound();
            }
            return View(serviEmpre);
        }

        // GET: ServiEmpres/Create
        public ActionResult Create()
        {
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.Id_Servicio = new SelectList(db.Tipo_Servicio, "Id_Servicio", "Descripción");
            return View();
        }

        // POST: ServiEmpres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tipo_Servicio,Id_Empresa,Id_ServiEmpre")] ServiEmpre serviEmpre)
        {
            if (ModelState.IsValid)
            {
                db.ServiEmpre.Add(serviEmpre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", serviEmpre.Id_Empresa);
            ViewBag.Id_Servicio = new SelectList(db.Tipo_Servicio, "Id_Tipo_Servicio", "Descripción", serviEmpre.Id_Tipo_Servicio);
            return View(serviEmpre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForCompany(ServiEmpre serviEmpre)
        {
            if (ModelState.IsValid)
            {
                if (serviEmpre.Id_ServiEmpre > 0)
                {
                    var data = db.ServiEmpre.Find(serviEmpre.Id_ServiEmpre);
                    data.Precio_Servicio = serviEmpre.Precio_Servicio;
                    data.Id_Tipo_Servicio = serviEmpre.Id_Tipo_Servicio;
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.ServiEmpre.Add(serviEmpre);
                    db.SaveChanges();
                }
                UpdateEvenserAfterServiceChange(serviEmpre.Id_Empresa, serviEmpre.Id_ServiEmpre);
                return RedirectToAction("Details", "Empresas", new { id = serviEmpre.Id_Empresa });
            }

            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", serviEmpre.Id_Empresa);
            ViewBag.Id_Servicio = new SelectList(db.Tipo_Servicio, "Id_Tipo_Servicio", "Descripción", serviEmpre.Id_Tipo_Servicio);
            return RedirectToAction("Details", "Empresas", new { id = serviEmpre.Id_Empresa });
        }

        public void UpdateEvenserAfterServiceChange(int Id_Empresa, int Id_ServiEmpre)
        {
            List<EvenEmpre> lstEvent = db.EvenEmpre.Where(x => x.Id_Empresa == Id_Empresa).ToList();
            for (int i = 0; i < lstEvent.Count; i++)
            {
                int Id_EvenEmpre = lstEvent[i].Id_EvenEmpre;
                evenser oldObjEvenser = db.evenser.Where(x => x.Id_Servi == Id_ServiEmpre && x.Id_Event == Id_EvenEmpre).FirstOrDefault();
                if (oldObjEvenser == null)
                {
                    evenser objEvenser = new evenser();
                    objEvenser.Id_Event = Id_EvenEmpre;
                    objEvenser.Id_Servi = Id_ServiEmpre;
                    db.evenser.Add(objEvenser);
                    db.SaveChanges();
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteFromCompany(int servEmpId, int compId)
        {
            try
            {
                ServiEmpre evenEmpre = db.ServiEmpre.Find(servEmpId);
                db.ServiEmpre.Remove(evenEmpre);
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Details", "Empresas", new { id = compId });
        }

        // GET: ServiEmpres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiEmpre serviEmpre = db.ServiEmpre.Find(id);
            if (serviEmpre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", serviEmpre.Id_Empresa);
            ViewBag.Id_Servicio = new SelectList(db.Tipo_Servicio, "Id_Tipo_Servicio", "Descripción", serviEmpre.Id_Tipo_Servicio);
            return View(serviEmpre);
        }

        // POST: ServiEmpres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Tipo_Servicio,Id_Empresa,Id_ServiEmpre")] ServiEmpre serviEmpre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviEmpre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", serviEmpre.Id_Empresa);
            ViewBag.Id_Servicio = new SelectList(db.Tipo_Servicio, "Id_Tipo_Servicio", "Descripción", serviEmpre.Id_Tipo_Servicio);
            return View(serviEmpre);
        }

        // GET: ServiEmpres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiEmpre serviEmpre = db.ServiEmpre.Find(id);
            if (serviEmpre == null)
            {
                return HttpNotFound();
            }
            return View(serviEmpre);
        }

        // POST: ServiEmpres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiEmpre serviEmpre = db.ServiEmpre.Find(id);
            db.ServiEmpre.Remove(serviEmpre);
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