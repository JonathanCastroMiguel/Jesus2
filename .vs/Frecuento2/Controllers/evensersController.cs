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
    public class evensersController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();

        // GET: evensers
        public ActionResult Index()
        {
            var evenser = db.evenser.Include(e => e.EvenEmpre).Include(e => e.ServiEmpre);
            return View(evenser.ToList());
        }

        // GET: evensers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenser evenser = db.evenser.Find(id);
            if (evenser == null)
            {
                return HttpNotFound();
            }
            return View(evenser);
        }

        // GET: evensers/Create
        public ActionResult Create()
        {
            ViewBag.Id_Event = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre");
            ViewBag.Id_Servi = new SelectList(db.ServiEmpre, "Id_ServiEmpre", "Id_ServiEmpre");
            return View();
        }

        // POST: evensers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Servi,Id_Event")] evenser evenser)
        {
            if (ModelState.IsValid)
            {
                db.evenser.Add(evenser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Event = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre", evenser.Id_Event);
            ViewBag.Id_Servi = new SelectList(db.ServiEmpre, "Id_ServiEmpre", "Id_ServiEmpre", evenser.Id_Servi);
            return View(evenser);
        }

        // GET: evensers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenser evenser = db.evenser.Find(id);
            if (evenser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Event = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre", evenser.Id_Event);
            ViewBag.Id_Servi = new SelectList(db.ServiEmpre, "Id_ServiEmpre", "Id_ServiEmpre", evenser.Id_Servi);
            return View(evenser);
        }

        // POST: evensers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Servi,Id_Event")] evenser evenser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Event = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre", evenser.Id_Event);
            ViewBag.Id_Servi = new SelectList(db.ServiEmpre, "Id_ServiEmpre", "Id_ServiEmpre", evenser.Id_Servi);
            return View(evenser);
        }

        // GET: evensers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenser evenser = db.evenser.Find(id);
            if (evenser == null)
            {
                return HttpNotFound();
            }
            return View(evenser);
        }

        // POST: evensers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            evenser evenser = db.evenser.Find(id);
            db.evenser.Remove(evenser);
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
