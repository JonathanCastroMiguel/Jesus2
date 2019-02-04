using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Frecuento2.Models;

namespace Frecuento2.Controllers
{
    public class ReservasController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();

        // GET: Reservas
        public ActionResult Index()
        {
            int client_ID = this.GetClientIDByMail(User.Identity.GetUserName()); ;
            var reserva = db.Reserva.Include(r => r.Cliente).Include(r => r.Empresa).Include(r => r.EvenEmpre).Where(r => r.Id_Cliente == client_ID).OrderByDescending(r => r.Fecha).ToList();
            return View("index", "_LayoutCliente", reserva.ToList());
        }

        public ActionResult ReservasAdmin()
        {
            var reserva = db.Reserva.ToList();
            return View("index", "_LayoutAdmin", reserva.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre");
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre");
            ViewBag.EvenEmpreId_Evento = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Reserva,Precio_total,Id_Cliente,Id_Empresa,Fecha,EvenEmpreId_Evento,Precio_Servicios,Precio_base_Evento")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Reserva.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", reserva.Id_Cliente);
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", reserva.Id_Empresa);
            ViewBag.EvenEmpreId_Evento = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre", reserva.EvenEmpreId_Evento);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", reserva.Id_Cliente);
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", reserva.Id_Empresa);
            ViewBag.EvenEmpreId_Evento = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre", reserva.EvenEmpreId_Evento);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Reserva,Precio_total,Id_Cliente,Id_Empresa,Fecha,EvenEmpreId_Evento,Precio_Servicios,Precio_base_Evento")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", reserva.Id_Cliente);
            ViewBag.Id_Empresa = new SelectList(db.Empresa, "Id_Empresa", "Nombre", reserva.Id_Empresa);
            ViewBag.EvenEmpreId_Evento = new SelectList(db.EvenEmpre, "Id_EvenEmpre", "Id_EvenEmpre", reserva.EvenEmpreId_Evento);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reserva.Find(id);
            db.Reserva.Remove(reserva);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private int GetClientIDByMail(string mail)
        {
            //get from DB
            return db.Cliente.FirstOrDefault(cliente => cliente.Email == mail).Id_Cliente;
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
