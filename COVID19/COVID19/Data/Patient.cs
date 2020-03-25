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
    
    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.Audits = new HashSet<Audit>();
            this.Contacts = new HashSet<Contact>();
            this.ContactReasons = new HashSet<ContactReason>();
            this.PatientHistories = new HashSet<PatientHistory>();
        }
    
        public int PatientID { get; set; }
        public Nullable<int> PersonId { get; set; }
        public Nullable<int> AdressID { get; set; }
        public bool HaveSymptoms { get; set; }
        public bool InfectedContact { get; set; }
        public bool RiskPatient { get; set; }
        public Nullable<int> RiskReasonId { get; set; }
        public bool Isolation { get; set; }
        public Nullable<System.DateTime> IsolationDate { get; set; }
        public Nullable<System.DateTime> IsolationFinishDate { get; set; }
        public bool CountryEntrance { get; set; }
        public Nullable<System.DateTime> CountryEntranceDAte { get; set; }
        public int NumberOdFamilyGroup { get; set; }
        public string TravelCountry { get; set; }
        public Nullable<System.DateTime> PositiveTestDate { get; set; }
        public string Doctor { get; set; }
        public bool Travelling { get; set; }
        public Nullable<int> StatuID { get; set; }
        public string RiskObservation { get; set; }
        public System.DateTime EffectDate { get; set; }
        public Nullable<System.DateTime> NullDate { get; set; }
        public bool HadInfectedContact { get; set; }
        public bool IsReturning { get; set; }
        public Nullable<int> QtyPersonsInTouch { get; set; }
    
        public virtual Adress Adress { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audit> Audits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactReason> ContactReasons { get; set; }
        public virtual Person Person { get; set; }
        public virtual RiskReason RiskReason { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientHistory> PatientHistories { get; set; }
    }
}
