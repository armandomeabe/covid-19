using COVID19.Data;
using COVID19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COVID19.Controllers
{
    [Authorize]
    public class PersonController : BaseController
    {
        // GET: Person
        public ActionResult Index(string SearchPeople)
        {


            var Search = from P in this.COVID19Entities.People
                         select P;
                         
                // busco por documento o por apellido
                Search = Search.Where(s => s.DocumentNumber.ToString().Contains(SearchPeople) || s.LastName.Contains(SearchPeople));


            // Las entidades no deberían llegar a la vista nunca. Se recurre a este método provisionalmente para salir rápido.
            //var model = this.COVID19Entities.People.ToList();
            return View(Search);
        }

        public ActionResult PersonalInfo(int? PersonID)
        {
            var maritalStatusOptions = this.COVID19Entities.MaritalStatus.ToList().Select(x =>
                new SelectListItem
                {
                    Text = x.MaritalStatus,
                    Value = x.Id.ToString()
                }
            ).ToList();
            ViewBag.maritalStatusOptions = maritalStatusOptions;

            var model = new NewPersonDtoStep1
            {
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult PersonalInfo(NewPersonDtoStep1 model)
        {
            if (!ModelState.IsValid)
            {
                var maritalStatusOptions = this.COVID19Entities.MaritalStatus.ToList().Select(x =>
                    new SelectListItem
                    {
                        Text = x.MaritalStatus,
                        Value = x.Id.ToString()
                    }
                ).ToList();

                ViewBag.maritalStatusOptions = maritalStatusOptions;

                return View(model);
            }

            Person dbPerson;
            if (model.PersonID == 0)
            {
                dbPerson = new Person
                {
                    Name = model.Name,
                    SecondName = model.SecondName,
                    LastName = model.LastName,
                    DocumentTypeID = -1,
                    DocumentNumber = model.DocumentNumber.GetValueOrDefault(-1),
                    Gender = model.Gender,
                    BirrhtDate = DateTime.Parse(model.BirrhtDate),
                    MaritalSatatusID = model.MaritalSatatusID,
                    EffectDate = DateTime.Now,
                    Adress = new Adress
                    {
                        
                    }
                };

                this.COVID19Entities.People.Add(dbPerson);
                this.COVID19Entities.SaveChanges();
            }
            else
            {
                dbPerson = new Person { };
            }

            return RedirectToAction("Address", "Person", new { id = dbPerson.Personid });
        }

        public ActionResult Address(int id)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);

            //if (dbPerson)

            var model = new NewPersonDtoStep2
            {
                PersonID = id,
                City = dbPerson.Location,
                //Comments = dbPerson.
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Address(NewPersonDtoStep2 model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == model.PersonID);
            var address = dbPerson.Adress;

            address.Street = model.Street;
            address.Number = int.Parse(model.Number);
            address.Floor = model.Floor;
            address.Department = model.Dept;
            address.Comments = model.Comments;
            address.EffectDate = DateTime.Now;

            this.COVID19Entities.SaveChanges();

            return RedirectToAction("Contact", "Person", new { id = dbPerson.Personid });
        }

        public ActionResult Contact(int id)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);

            var model = new NewPersonDtoStep3
            {
                PersonID = id,
                Mail = dbPerson.Mail,
                MobilePhone = dbPerson.MobileNumber,
                Phone = dbPerson.TelephoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(NewPersonDtoStep3 model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == model.PersonID);

            dbPerson.Mail = model.Mail;
            dbPerson.MobileNumber = model.MobilePhone;
            dbPerson.TelephoneNumber = model.Phone;

            this.COVID19Entities.SaveChanges();

            return RedirectToAction("Medical", "Person", new { id = dbPerson.Personid });
        }

        public ActionResult Medical(int id)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);

            if (!dbPerson.Patients.Any())
            {
                var model = new NewPersonDtoStep4
                {
                    
                };
                return View(model);
            }
            else
            {
                var patient = dbPerson.Patients.First();

                var model = new NewPersonDtoStep4
                {
                    CountryEntranceDate = patient.CountryEntranceDAte,
                    //HadContact = patient.HadInfectedContact,
                    HaveSymptoms = patient.HaveSymptoms
                    //IsReturning = patient.
                };
                return View(model);
            }
        }

        //public ActionResult New()
        //{
        //    return View(new NewPersonDto());
        //}

        //[HttpPost]
        //public ActionResult New(NewPersonDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    return RedirectToAction("Index");
        //}

        //public ActionResult Test()
        //{
        //    return View(new NewPersonDto());
        //}

        //[HttpPost]
        //public ActionResult Test(NewPersonDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}