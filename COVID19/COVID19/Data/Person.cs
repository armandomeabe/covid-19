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
    
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.Patients = new HashSet<Patient>();
        }
    
        public int Personid { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentNumber { get; set; }
        public int MaritalSatatusID { get; set; }
        public Nullable<System.DateTime> BirrhtDate { get; set; }
        public Nullable<int> AdressId { get; set; }
        public string Location { get; set; }
        public string Province { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public Nullable<int> TelephoneNumber { get; set; }
        public Nullable<int> MobileNumber { get; set; }
        public string Mail { get; set; }
        public System.DateTime EffectDate { get; set; }
        public Nullable<System.DateTime> NullDate { get; set; }
    
        public virtual Adress Adress { get; set; }
        public virtual MaritalStatu MaritalStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
