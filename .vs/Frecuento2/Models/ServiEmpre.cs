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
    
    public partial class ServiEmpre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiEmpre()
        {
            this.evenser = new HashSet<evenser>();
        }
    
        public int Id_Tipo_Servicio { get; set; }
        public int Precio_Servicio { get; set; }
        public int Id_Empresa { get; set; }
        public int Id_ServiEmpre { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evenser> evenser { get; set; }
        public virtual Tipo_Servicio Tipo_Servicio { get; set; }
    }
}