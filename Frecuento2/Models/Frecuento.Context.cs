﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class FrecuentoEntities1 : DbContext
{
    public FrecuentoEntities1()
        : base("name=FrecuentoEntities1")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Empresa> Empresa { get; set; }

    public virtual DbSet<EvenEmpre> EvenEmpre { get; set; }

    public virtual DbSet<evenser> evenser { get; set; }

    public virtual DbSet<Reserva> Reserva { get; set; }

    public virtual DbSet<ServiEmpre> ServiEmpre { get; set; }

    public virtual DbSet<Tipo_Evento> Tipo_Evento { get; set; }

    public virtual DbSet<Tipo_Servicio> Tipo_Servicio { get; set; }

}

}

