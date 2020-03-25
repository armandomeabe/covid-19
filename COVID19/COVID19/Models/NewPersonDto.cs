using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COVID19.Models
{
    public class NewPersonDtoStep1
    {
        public int PersonID { get; set; }

        // Paso 1: Información Personal
        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Segundo nombre")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Documento")]
        public int? DocumentNumber { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Género")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Nacimiento")]
        public DateTime? BirrhtDate { get; set; }

        //[Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Estado Civil")]
        public int MaritalSatatusID { get; set; }
        //public string MaritalState { get; set; }
    }

    public class NewPersonDtoStep2
    {
        public int PersonID { get; set; }

        // Paso 2: Domicilio
        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Calle")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Número")]
        public int? Number { get; set; }

        [Display(Name = "Piso")]
        public string Floor { get; set; }

        [Display(Name = "Dpto.")]
        public string Dept { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
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
        public DateTime? CountryEntranceDate { get; set; }

        //[Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "País de procedencia")]
        public string TravelCountry { get; set; }

        [Display(Name = "Fecha de test positivo")]
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
        public DateTime? IsolationStart { get; set; }

        [Display(Name = "Fecha de fin de aislamiento")]
        public DateTime? IsolationEnd { get; set; }

        [Display(Name = "Cantidad de personas en contacto")]
        public int? QtyPersonsInTouch { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Calle")]
        public string ContactStreet { get; set; }

        [Required(ErrorMessage = "Información requerida")]
        [Display(Name = "Número")]
        public int? ContactNumber { get; set; }

        [Display(Name = "Piso")]
        public string ContactFloor { get; set; }

        [Display(Name = "Dpto.")]
        public string ContactDept { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
        [Display(Name = "Ciudad")]
        public string ContactCity { get; set; }

        [Required(ErrorMessage = "Información requerida"), MaxLength(250)]
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