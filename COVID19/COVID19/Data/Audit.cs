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
    
    public partial class Audit
    {
        public int Id { get; set; }
        public Nullable<int> VoluntaryId { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string UserLoged { get; set; }
        public System.DateTime EffectDate { get; set; }
        public Nullable<System.DateTime> NullDate { get; set; }
    
        public virtual Patient Patient { get; set; }
        public virtual Voluntary Voluntary { get; set; }
    }
}
