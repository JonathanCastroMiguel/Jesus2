
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
    
public partial class evenser
{

    public int Id { get; set; }

    public int Id_Servi { get; set; }

    public int Id_Event { get; set; }



    public virtual EvenEmpre EvenEmpre { get; set; }

    public virtual ServiEmpre ServiEmpre { get; set; }

}

}
