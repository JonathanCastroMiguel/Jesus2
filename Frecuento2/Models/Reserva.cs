
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Frecuento2.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Reserva
{

    public int Id_Reserva { get; set; }

    public int Precio_total { get; set; }

    public int Id_Cliente { get; set; }

    public int Id_Empresa { get; set; }

    public System.DateTime Fecha { get; set; }

    public int EvenEmpreId_Evento { get; set; }

    public int Precio_Servicios { get; set; }

    public int Precio_base_Evento { get; set; }



    public virtual Cliente Cliente { get; set; }

    public virtual Empresa Empresa { get; set; }

    public virtual EvenEmpre EvenEmpre { get; set; }

}

}
