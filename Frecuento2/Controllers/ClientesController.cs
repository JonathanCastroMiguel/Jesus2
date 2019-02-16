using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
        public ActionResult Index(string Busqueda2)
        {
            List<Cliente> Clientes = db.Cliente.ToList();
            if (!string.IsNullOrEmpty(Busqueda2))
            {
                Clientes = Clientes.Where(e => e.Nombre.Contains(Busqueda2)).ToList();
            }
            return View("Index", "_LayoutAdmin", Clientes);
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

                return RedirectToAction("Buscador", "Clientes");
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
                if (User.IsInRole("Cliente"))
                {
                    return RedirectToAction("Perfil", "Home");
                }
                return RedirectToAction("Index");
            }
            return View("_LayoutCliente",cliente);
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
        public ActionResult Buscador(bool fromUI = false)
        {
            ViewBag.EventList = db.Tipo_Evento.ToList();
            List<Tipo_Evento> lstEvent = db.Tipo_Evento.ToList();
            List<CompanyViewModel> lstCompanyViewModel = new List<CompanyViewModel>();
            for (int evti = 0; evti < lstEvent.Count; evti++)
            {
                int eventId = lstEvent[evti].Id_Tipo_Evento;
                if (eventId > 0)
                {
                    List<int> lstCompany = db.EvenEmpre.Where(x => x.Tipo_Evento.Id_Tipo_Evento == eventId).Select(x => x.Id_Empresa).Distinct().ToList();
                    for (int i = 0; i < lstCompany.Count; i++)
                    {
                        int companyID = lstCompany[i];
                        List<evenser> lstEvenSer = db.evenser.Where(x => x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId && x.ServiEmpre.Id_Empresa == companyID).ToList();

                        List<int> lstAllServicesByCompany = lstEvenSer.Select(x => x.ServiEmpre.Tipo_Servicio.Id_Tipo_Servicio).Distinct().ToList();

                        CompanyViewModel model = new CompanyViewModel();
                        EvenEmpre objEvenEmpre = db.EvenEmpre.Where(x => x.Tipo_Evento.Id_Tipo_Evento == eventId && x.Id_Empresa == companyID).FirstOrDefault();
                        model.Id_EvenEmpre = objEvenEmpre.Id_EvenEmpre;
                        model.Nombre = objEvenEmpre.Empresa.Nombre;
                        model.Precio_Base = objEvenEmpre.Precio_Base;
                        model.EventName = objEvenEmpre.Tipo_Evento.Descripción;

                        List<ServiEmpre> lstServiceEmpre = db.evenser.Where(x => x.ServiEmpre.Id_Empresa == companyID && x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId).Select(x => x.ServiEmpre).ToList();

                        model.Services = string.Join(",", lstServiceEmpre
                                .GroupBy(x => x.Tipo_Servicio.Id_Tipo_Servicio)
                              .Select(p => p.FirstOrDefault().Tipo_Servicio.Descripción.ToString()));
                        model.Precio_Servicios = Convert.ToInt32(lstServiceEmpre.Sum(x => x.Precio_Servicio).ToString());
                        model.Id_EvenSers = string.Join(",", db.evenser.Where(x => x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId && x.ServiEmpre.Id_Empresa == companyID).Select(x => x.Id));
                        lstCompanyViewModel.Add(model);
                    }
                }
            }
            if (fromUI)
            {
                return PartialView("_CompanyList", lstCompanyViewModel);
            }
            return View("Buscador", "_LayoutCliente", lstCompanyViewModel);
        }

        [HttpGet]
        public ActionResult BindCheckboxForService(int? eventId)
        {
            List<Tipo_Servicio> lstService = new List<Tipo_Servicio>();
            if (eventId != null)
            {
                lstService =
                   db.evenser.Where(x => x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId).GroupBy(x => x.ServiEmpre.Tipo_Servicio.Id_Tipo_Servicio).Select(x => x.FirstOrDefault().ServiEmpre.Tipo_Servicio).ToList();
            }
            return PartialView("_ServicesForEvent", lstService);
        }

        [HttpGet]
        public ActionResult BindCompany(int eventId, string serviceIDs)
        {
            List<CompanyViewModel> lstCompanyViewModel = new List<CompanyViewModel>();
            if (eventId > 0)
            {

                List<int> lstCompany = db.EvenEmpre.Where(x => x.Tipo_Evento.Id_Tipo_Evento == eventId).Select(x => x.Id_Empresa).Distinct().ToList();
                for (int i = 0; i < lstCompany.Count; i++)
                {
                    int companyID = lstCompany[i];
                    List<evenser> lstEvenSer = db.evenser.Where(x => x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId && x.ServiEmpre.Id_Empresa == companyID).ToList();

                    List<string> lstAllServicesByCompany = lstEvenSer.Select(x => x.ServiEmpre.Tipo_Servicio.Descripción).Distinct().ToList();
                    bool results = true;
                    if (!string.IsNullOrEmpty(serviceIDs) && serviceIDs.Length > 0)
                    {
                        List<string> selectedServices = serviceIDs.Split(',').ToList();
                        results = selectedServices.All(j => lstAllServicesByCompany.Contains(j));
                    }
                    if (results)
                    {
                        CompanyViewModel model = new CompanyViewModel();
                        EvenEmpre objEvenEmpre = db.EvenEmpre.Where(x => x.Tipo_Evento.Id_Tipo_Evento == eventId && x.Id_Empresa == companyID).FirstOrDefault();
                        model.Id_EvenEmpre = objEvenEmpre.Id_EvenEmpre;
                        model.Nombre = objEvenEmpre.Empresa.Nombre;
                        model.Precio_Base = objEvenEmpre.Precio_Base;
                        model.EventName = objEvenEmpre.Tipo_Evento.Descripción;
                        if (!string.IsNullOrEmpty(serviceIDs) && serviceIDs.Length > 0)
                        {
                            List<ServiEmpre> lstServiceEmpre = db.evenser.Where(x => x.ServiEmpre.Id_Empresa == companyID && x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId && serviceIDs.Contains(x.ServiEmpre.Tipo_Servicio.Descripción.ToString())).Select(x => x.ServiEmpre).ToList();

                            model.Services = string.Join(",", lstServiceEmpre
                                    .GroupBy(x => x.Tipo_Servicio.Id_Tipo_Servicio)
                                  .Select(p => p.FirstOrDefault().Tipo_Servicio.Descripción.ToString()));
                            model.Precio_Servicios = Convert.ToInt32(lstServiceEmpre.Sum(x => x.Precio_Servicio).ToString());

                            model.Id_EvenSers = string.Join(",", db.evenser.Where(x => x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId && x.ServiEmpre.Id_Empresa == companyID && x.ServiEmpre.Tipo_Servicio.Descripción.ToString().Contains(serviceIDs)).Select(x => x.Id));
                        }
                        else
                        {
                            List<ServiEmpre> lstServiceEmpre = db.evenser.Where(x => x.ServiEmpre.Id_Empresa == companyID && x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId).Select(x => x.ServiEmpre).ToList();

                            model.Services = string.Join(",", lstServiceEmpre
                                    .GroupBy(x => x.Tipo_Servicio.Id_Tipo_Servicio)
                                  .Select(p => p.FirstOrDefault().Tipo_Servicio.Descripción.ToString()));
                            model.Precio_Servicios = Convert.ToInt32(lstServiceEmpre.Sum(x => x.Precio_Servicio).ToString());
                            model.Id_EvenSers = string.Join(",", db.evenser.Where(x => x.EvenEmpre.Tipo_Evento.Id_Tipo_Evento == eventId && x.ServiEmpre.Id_Empresa == companyID).Select(x => x.Id));
                        }


                        lstCompanyViewModel.Add(model);
                    }

                }


            }
            //List<evenser> lstService = db.evenser.Where(x => serviceIDs.Contains(x.Id.ToString())).ToList();
            return PartialView("_CompanyList", lstCompanyViewModel);
        }

        [HttpPost]
        public JsonResult CheckOut(string Evenempre_Id, int serviceTotal, string Fecha)
        {
            try
            {
                int id_evenEmpre = Convert.ToInt32(Evenempre_Id);
                EvenEmpre evenEmpre = db.EvenEmpre.Where(x => x.Id_EvenEmpre == id_evenEmpre).FirstOrDefault();

                Reserva cart = new Reserva();
                cart.Precio_total = evenEmpre.Precio_Base + serviceTotal;
                cart.Id_Cliente = this.GetClientIDByMail(User.Identity.GetUserName()); ;
                cart.Id_Empresa = evenEmpre.Id_Empresa;
                cart.Fecha = Convert.ToDateTime(Fecha);
                cart.EvenEmpreId_Evento = evenEmpre.Id_EvenEmpre;
                cart.Precio_Servicios = serviceTotal;
                cart.Precio_base_Evento = evenEmpre.Precio_Base;
                db.Reserva.Add(cart);
                db.SaveChanges();
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
            return db.Cliente.FirstOrDefault(cliente => cliente.Email == mail).Id_Cliente;
        }
    }
}
