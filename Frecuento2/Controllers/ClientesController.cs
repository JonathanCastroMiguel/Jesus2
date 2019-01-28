using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Frecuento2.Models;
using Microsoft.AspNet.Identity;

namespace Frecuento2.Controllers
{
    public class ClientesController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();

        [Authorize(Roles = "Administrador")]
        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View("Details", "_LayoutCliente", cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Cliente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cliente,Nombre,Apellidos,Dirección,Código_Postal,Fecha_Nacimiento,Teléfono,DNI,Nº_Cuenta")] Cliente cliente)
        {

            cliente.Email = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();

                return RedirectToAction("Reservas", "Home");
            }

            return View("Create", "_LayoutCliente", cliente);
        }

        // GET: Clientes/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cliente,Nombre,Apellidos,Dirección,Código_Postal,Fecha_Nacimiento,Email,Teléfono,DNI,Nº_Cuenta")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Buscador()
        {
            ViewBag.EventList = db.Tipo_Evento.ToList();
            return View("Buscador", "_LayoutCliente", null);
        }

        [HttpGet]
        public ActionResult BindCheckboxForService(int? eventId)
        {
            List<evenser> lstService = db.evenser.Where(x => x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId).ToList();
            return PartialView("_ServicesForEvent", lstService);
        }

        [HttpGet]
        public ActionResult BindCompany(string serviceIDs)
        {
            List<evenser> lstService = db.evenser.Where(x => serviceIDs.Contains(x.Id.ToString())).ToList();
            return PartialView("_CompanyList", lstService);
        }

        [HttpPost]
        public JsonResult CheckOut(string strevenserIDs, int cartTotal, int serviceTotal)
        {
            int[] nums = Array.ConvertAll(strevenserIDs.Split(','), int.Parse);
            try
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    int id = nums[i];
                    evenser e = db.evenser.Where(x => x.Id == id).FirstOrDefault();
                    Reserva cart = new Reserva();
                    cart.Precio_total = cartTotal;
                    cart.Id_Cliente = this.GetClientIDByMail(User.Identity.GetUserName()); ;
                    cart.Id_Empresa = e.EvenEmpre.Id_Empresa;
                    cart.Fecha = DateTime.Now;
                    cart.EvenEmpreId_Evento = e.EvenEmpre.Id_EvenEmpre;
                    cart.Precio_Servicios = serviceTotal;
                    cart.Precio_base_Evento = e.EvenEmpre.Precio_Base;
                    db.Reserva.Add(cart);
                    db.SaveChanges();
                }
                return Json(new { success = "true" });
            }
            catch (Exception e)
            {
            }
            return Json(new { success = "false" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private int GetClientIDByMail(string mail)
        {
            //get from DB
            return db.Cliente.SingleOrDefault(cliente => cliente.Email == mail).Id_Cliente;
        }
    }
}
