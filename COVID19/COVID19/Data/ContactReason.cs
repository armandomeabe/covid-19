//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace COVID19.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactReason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContactReason()
        {
            this.Contacts = new HashSet<Contact>();
        }
    
        public int Id { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string Reason { get; set; }
        public System.DateTime EffectDate { get; set; }
        public Nullable<System.DateTime> NullDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
