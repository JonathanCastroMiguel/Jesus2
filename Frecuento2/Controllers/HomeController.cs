using Frecuento2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frecuento2.Controllers
{
    public class HomeController : Controller
    {
        private FrecuentoEntities1 db = new FrecuentoEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            if (User.IsInRole("Empresa"))
            {
                return View("About", "_Layout", null);
            }
            else if (User.IsInRole("Cliente"))
            {
                return View("About", "_LayoutCliente", null);
            }
            else
            {
                return View("About", "_LayoutAdmin", null);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            if (User.IsInRole("Empresa"))
            {
                return View("Contact", "_Layout", null);
            }
            else if (User.IsInRole("Cliente"))
            {
                return View("Contact", "_LayoutCliente", null);
            }
            else
            {
                return View("Contact", "_LayoutAdmin", null);
            }
        }

        public ActionResult Reservas()
        {
            ViewBag.Message = "Find new services";

            return View("Reservas", "_LayoutCliente", null);
        }

        public ActionResult Carrito()
        {

            ViewBag.Message = "Check your purchased services";

            if (User.IsInRole("Empresa"))
            {
                return View("CarritoEmp", "_Layout", null);
            }
            else
            {
                return View("Carrito", "_LayoutCliente", null);
            }
        }

        public ActionResult Perfil()
        {
            ViewBag.Message = "Edit your profile";

            if (User.IsInRole("Empresa"))
            {
                int id = this.GetCompanyIDByMail(User.Identity.GetUserName());
                //ViewBag.EmpresaID = id;
                return RedirectToAction("Details", "Empresas", new { @id = id });

            }
            else
            {

                int id = this.GetClientIDByMail(User.Identity.GetUserName());
                //ViewBag.EmpresaID = id;
                return RedirectToAction("Details", "Clientes", new { @id = id });
            }
        }

        //*** CUSTOM METHODS ***
        private int GetCompanyIDByMail(string mail)
        {
            //get from DB
            return db.Empresa.SingleOrDefault(empresa => empresa.Email == mail).Id_Empresa;
        }

        private int GetClientIDByMail(string mail)
        {
            //get from DB
            return db.Cliente.SingleOrDefault(cliente => cliente.Email == mail).Id_Cliente;
        }
    }
}