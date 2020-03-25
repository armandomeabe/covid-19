using COVID19.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COVID19.Models
{
    public class ContactsDto
    {
        public NewPersonDtoStep1 PersonDto { get; set; }
        public List<ContactDto> Contacts { get; set; }
    }

    public class ContactDto
    {
        public int? PersonID { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Tipo de documento")]
        public int DocumentTypeID { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Documento")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Género")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Nacimiento")]
        public DateTime? BirthDay { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Estado Civil")]
        public int MaritalStatusID { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Motivo")]
        public int? ReasonID { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(1024)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }

        public List<string> HistoryMetadata { get; set; }

    }

    public class EditPersonAuditDto
    {
        [Display(Name = "Motivo por el que se está actualizando la información")]
        public int PatientActionID { get; set; }
        
        public List<SelectListItem> PatientActions { get; set; }

        public int PersonID { get; set; }
    }

    public class NewPersonDtoStep1
    {
        public int PersonID { get; set; }
        public int? ContactPersonId { get; set; }

        [Display(Name = "Motivo de contacto")]
        public int? ContactReasonId { get; set; }

        [Display(Name = "Observaciones")]
        public string ContactObservations { get; set; }

        // Paso 1: Información Personal
        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Segundo nombre")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Documento")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Género")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirrhtDate { get; set; }

        //[Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Estado Civil")]
        public int MaritalSatatusID { get; set; }
        public string MaritalSatatusSTR { get; set; }
        //public string MaritalState { get; set; }

        public List<string> HistoryMetadata { get; set; }
    }

    public class NewPersonDtoStep2
    {
        public int PersonID { get; set; }

        // Paso 2: Domicilio
        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Calle")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Número")]
        public int? Number { get; set; }

        [Display(Name = "Piso")]
        public string Floor { get; set; }

        [Display(Name = "Dpto.")]
        public string Dept { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Provincia")]
        public string State { get; set; }

        [Display(Name = "Comentarios")]
        public string Comments { get; set; }
    }

    public class NewPersonDtoStep3
    {
        public int PersonID { get; set; }

        // Paso 3: Contacto
        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Teléfono")]
        public int? Phone { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Celular")]
        public int? MobilePhone { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Email")]
        public string Mail { get; set; }
    }

    public class NewPersonDtoStep4
    {
        public int PersonID { get; set; }

        // Paso 4: Datos Médicos
        [Display(Name = "Presenta Síntomas")]
        public bool HaveSymptoms { get; set; }

        [Display(Name = "Mantuvo contacto con personas infectadas")]
        public bool HadInfectedContact { get; set; }

        [Display(Name = "Grupo de riesgo")]
        public bool RiskGroup { get; set; }

        [Display(Name = "Motivo de riesgo")]
        public int? RiskReasonID { get; set; }

        [Display(Name = "En viaje de regreso")]
        public bool IsReturning { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Fecha de ingreso al país")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CountryEntranceDate { get; set; }

        //[Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "País de procedencia")]
        public string TravelCountry { get; set; }

        [Display(Name = "Fecha de test positivo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PositiveTestDate { get; set; }

        [Display(Name = "Médico tratante")]
        public string TreatingDoctor { get; set; }
    }


    public class NewPersonDtoStep5
    {
        public int PersonID { get; set; }

        // Paso 5: Datos de aislamiento
        [Display(Name = "En aislamiento")]
        public bool InIsolation { get; set; }

        [Display(Name = "Fecha de inicio de aislamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? IsolationStart { get; set; }

        [Display(Name = "Fecha de fin de aislamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? IsolationEnd { get; set; }

        [Display(Name = "Cantidad de personas en contacto")]
        public int? QtyPersonsInTouch { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Calle")]
        public string ContactStreet { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Número")]
        public int? ContactNumber { get; set; }

        [Display(Name = "Piso")]
        public string ContactFloor { get; set; }

        [Display(Name = "Dpto.")]
        public string ContactDept { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Ciudad")]
        public string ContactCity { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(200)]
        [Display(Name = "Provincia")]
        public string ContactState { get; set; }
    }

    public class NewPersonBriefDto
    {
        public NewPersonDtoStep1 step1 { get; set; }
        public NewPersonDtoStep2 step2 { get; set; }
        public NewPersonDtoStep3 step3 { get; set; }
        public NewPersonDtoStep4 step4 { get; set; }
        public NewPersonDtoStep5 step5 { get; set; }
    }
}