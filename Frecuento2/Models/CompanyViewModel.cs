using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frecuento2.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public int Id_Service { get; set; }
        public int Id_Event { get; set; }
        public string Id_EvenEmpre { get; set; }
        public int Client_Id { get; set; }
        public string Nombre { get; set; }
        public string Services { get; set; }

        public int Precio_Base { get; set; }
        public int Precio_Servicios { get; set; }

        public string Id_EvenSers { get; set; }

        public string EventName { get; set; }
    }
}