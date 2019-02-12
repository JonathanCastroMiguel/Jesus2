//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Frecuento2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Empresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresa()
        {
            this.EvenEmpre = new HashSet<EvenEmpre>();
            this.Reserva = new HashSet<Reserva>();
            this.ServiEmpre = new HashSet<ServiEmpre>();
        }

        public int Id_Empresa { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50, ErrorMessage = "Longuitud debe ser entre 3 y 50.", MinimumLength = 3)]
        public string Nombre { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50, ErrorMessage = "Longuitud debe ser entre 3 y 50.", MinimumLength = 3)]
        public string Dirección { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [RegularExpression(@"^(\([0-9]{3}\)|[0-9]{3}-)[0-9]{3}-[0-9]{3}|(\([0-9]{3}\)|[0-9]{3})[0-9]{3}[0-9]{3}$",
          ErrorMessage = "Teléfono incorrecto.")]
        public int Teléfono { get; set; }
        [Display(Name = "Número de empleados")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int Nº_Empleados { get; set; }
        [Display(Name = "Número de Cuenta")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(24, ErrorMessage = "Formato debe ser ES y 22 dígitos.", MinimumLength = 24)]
        public string Nº_Cuenta { get; set; }
        [Display(Name = "Correo electrónico")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
          ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvenEmpre> EvenEmpre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiEmpre> ServiEmpre { get; set; }
    }
}
