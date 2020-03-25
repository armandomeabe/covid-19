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
        public ActionResult Contacts(int id)
        {
            var dbPerson = this.COVID19Entities.People.FirstOrDefault(x => x.Personid == id);
            var dbContacts = this.COVID19Entities.Contacts.Where(x => x.ContactPersonId == id);

            var model = new ContactsDto
            {
                PersonDto = new NewPersonDtoStep1
                {
                    PersonID = id,
                    Name = dbPerson.Name,
                    SecondName = dbPerson.SecondName,
                    LastName = dbPerson.LastName,
                    DocumentNumber = dbPerson.DocumentNumber,
                    Gender = dbPerson.Gender,
                    BirrhtDate = dbPerson.BirrhtDate,
                    MaritalSatatusID = dbPerson.MaritalSatatusID
                },
                Contacts = dbContacts.ToList().Select(x => new ContactDto
                {
                    BirthDay = x.Patient.Person.BirrhtDate,
                    DocumentNumber = x.Patient.Person.DocumentNumber,
                    DocumentTypeID = x.Patient.Person.DocumentTypeID,
                    Gender = x.Patient.Person.Gender,
                    Name = x.Patient.Person.Name,
                    LastName = x.Patient.Person.LastName,
                    SecondName = x.Patient.Person.SecondName,
                    Observations = x.Observations,
                    ReasonID = x.ContactReasonId,
                    PersonID = x.Patient.PersonId,
                    HistoryMetadata = x.Patient.PatientHistories.ToList().Select(m => $"Fecha: {m.EffectDate.ToString("d")} - Usuario: { m.Observation } - Acción realiazda: { m.PatientAction.Action }").ToList()
                }).ToList()
            };

            return View(model);
        }

        public ActionResult EditPersonAudit(int id)
        {
            var model = new EditPersonAuditDto
            {
                PersonID = id,
                PatientActions = this.COVID19Entities.PatientActions.Where(x => x.Id != 5 && x.NullDate == null).Select(x => new SelectListItem
                {
                    Text = x.Action,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPersonAudit(EditPersonAuditDto model)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == model.PersonID);

            var dbPatient = dbPerson.Patients.FirstOrDefault();


            if (dbPatient == null)
            {
                dbPerson.Patients.Add(new Patient
                {
                    EffectDate = DateTime.Now
                });
                this.COVID19Entities.SaveChanges();
            }

            var dbPatientHistory = new PatientHistory
            {
                ActionID = model.PatientActionID,
                EffectDate = DateTime.Now,
                Datetime = DateTime.Now,
                Observation = User.Identity.Name + " creó el nuevo registro con éxito",
                PatientID = dbPatient.PatientID
            };

            this.COVID19Entities.PatientHistories.Add(dbPatientHistory);
            this.COVID19Entities.SaveChanges();

            return RedirectToAction("PersonalInfo", "Person", new { id = model.PersonID });
        }

        // GET: Person
        public ActionResult Index(string SearchPeople)
        {
            var Search = from P in this.COVID19Entities.People
                         select P;

            // busco por documento o por apellido
            Search = Search.Where(s =>

                s.NullDate == null &&

                    (s.DocumentNumber.ToString().Contains(SearchPeople) || s.LastName.Contains(SearchPeople))

                ).OrderByDescending(x => x.EffectDate);

            var model = Search.ToList().Select(x => new NewPersonDtoStep1
            {
                PersonID = x.Personid,
                Name = x.Name,
                SecondName = x.SecondName,
                LastName = x.LastName,
                DocumentNumber = x.DocumentNumber,
                Gender = x.Gender,
                BirrhtDate = x.BirrhtDate,
                MaritalSatatusSTR = this.COVID19Entities.MaritalStatus.First(m => m.Id == x.MaritalSatatusID).MaritalStatus,
                HistoryMetadata = x.Patients?.FirstOrDefault()?.PatientHistories?.Select(m => $"Fecha: {m.EffectDate.ToString("d")} - Usuario: { m.Observation } - Acción realiazda: { m.PatientAction.Action }").ToList() ?? new List<string>()
            });

            return View(model);
        }

        public ActionResult PersonalInfo(int? id, int? contactPersonId)
        {
            if (contactPersonId.HasValue)
            {
                ViewBag.ContactPersonFrom = this.COVID19Entities.People.First(x => x.Personid == contactPersonId.Value);
            }

            var maritalStatusOptions = this.COVID19Entities.MaritalStatus.ToList().Select(x =>
                new SelectListItem
                {
                    Text = x.MaritalStatus,
                    Value = x.Id.ToString()
                }
            ).ToList();
            ViewBag.maritalStatusOptions = maritalStatusOptions;

            var contactReasons = this.COVID19Entities.ContactReasons.ToList().Select(x => new SelectListItem
            {
                Text = x.Reason,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.ContactReasons = contactReasons;

            if (id.HasValue)
            {
                var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id.Value);
                var model = new NewPersonDtoStep1
                {
                    PersonID = id.Value,
                    Name = dbPerson.Name,
                    SecondName = dbPerson.SecondName,
                    LastName = dbPerson.LastName,
                    DocumentNumber = dbPerson.DocumentNumber,
                    Gender = dbPerson.Gender,
                    BirrhtDate = dbPerson.BirrhtDate,
                    MaritalSatatusID = dbPerson.MaritalSatatusID,
                    ContactPersonId = contactPersonId
                };

                return View(model);
            }
            else
            {
                var model = new NewPersonDtoStep1
                {
                    ContactPersonId = contactPersonId
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult PersonalInfo(NewPersonDtoStep1 model)
        {
            if (!ModelState.IsValid)
            {
                if (model.ContactPersonId.HasValue)
                {
                    ViewBag.ContactPersonFrom = this.COVID19Entities.People.First(x => x.Personid == model.ContactPersonId.Value);
                }

                var maritalStatusOptions = this.COVID19Entities.MaritalStatus.ToList().Select(x =>
                    new SelectListItem
                    {
                        Text = x.MaritalStatus,
                        Value = x.Id.ToString()
                    }
                ).ToList();

                ViewBag.maritalStatusOptions = maritalStatusOptions;

                var contactReasons = this.COVID19Entities.ContactReasons.ToList().Select(x => new SelectListItem
                {
                    Text = x.Reason,
                    Value = x.Id.ToString()
                }).ToList();
                ViewBag.ContactReasons = contactReasons;

                return View(model);
            }

            Person dbPerson;
            if (model.PersonID == 0)
            {
                var existingPersonByDocumentNumber = this.COVID19Entities.People.FirstOrDefault(x => x.DocumentNumber == model.DocumentNumber);
                if (existingPersonByDocumentNumber != null)
                {
                    ModelState.AddModelError(string.Empty, "El documento ya está en uso");

                    var maritalStatusOptions = this.COVID19Entities.MaritalStatus.ToList().Select(x =>
                        new SelectListItem
                        {
                            Text = x.MaritalStatus,
                            Value = x.Id.ToString()
                        }
                    ).ToList();

                    ViewBag.maritalStatusOptions = maritalStatusOptions;

                    var contactReasons = this.COVID19Entities.ContactReasons.ToList().Select(x => new SelectListItem
                    {
                        Text = x.Reason,
                        Value = x.Id.ToString()
                    }).ToList();
                    ViewBag.ContactReasons = contactReasons;

                    return View(model);
                }

                dbPerson = new Person
                {
                    Name = model.Name,
                    SecondName = model.SecondName,
                    LastName = model.LastName,
                    DocumentTypeID = -1,
                    DocumentNumber = model.DocumentNumber,
                    Gender = model.Gender,
                    BirrhtDate = model.BirrhtDate,
                    MaritalSatatusID = model.MaritalSatatusID,
                    EffectDate = DateTime.Now,
                    Adress = new Adress
                    {

                    },
                    Patients = new List<Patient>() { new Patient { EffectDate = DateTime.Now } }
                };

                this.COVID19Entities.People.Add(dbPerson);
                this.COVID19Entities.SaveChanges();

                // CREACIÓN
                var dbPatientHistory = new PatientHistory
                {
                    ActionID = 5,
                    EffectDate = DateTime.Now,
                    Datetime = DateTime.Now,
                    Observation = User.Identity.Name + " creó el nuevo registro con éxito",
                    PatientID = dbPerson.Patients.First().PatientID
                };
                
                this.COVID19Entities.PatientHistories.Add(dbPatientHistory);
                this.COVID19Entities.SaveChanges();

                if (model.ContactPersonId.HasValue)
                {
                    // FEDE! TE CEDO EL PODER!

                    var contactDbPerson = this.COVID19Entities.People.First(x => x.Personid == model.ContactPersonId);

                    var dbContact = new Contact
                    {
                        PatientID = dbPerson.Patients.First().PatientID, //dbPerson.Patients.First().PatientID,
                        ContactPersonId = model.ContactPersonId,
                        EffectDate = DateTime.Now,
                        Observations = model.ContactObservations,
                        ContactReasonId = model.ContactReasonId
                    };

                    this.COVID19Entities.Contacts.Add(dbContact);
                    this.COVID19Entities.SaveChanges();
                }
            }
            else
            {
                // EDICIÓN
                dbPerson = this.COVID19Entities.People.First(x => x.Personid == model.PersonID);

                dbPerson.Name = model.Name;
                dbPerson.SecondName = model.SecondName;
                dbPerson.LastName = model.LastName;
                dbPerson.DocumentTypeID = -1;
                dbPerson.DocumentNumber = model.DocumentNumber;
                dbPerson.Gender = model.Gender;
                dbPerson.BirrhtDate = model.BirrhtDate;
                dbPerson.MaritalSatatusID = model.MaritalSatatusID;
                dbPerson.EffectDate = DateTime.Now;

                this.COVID19Entities.SaveChanges();
            }

            return RedirectToAction("Address", "Person", new { id = dbPerson.Personid });
        }

        public ActionResult Address(int id)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);
            if (dbPerson.Adress == null)
            {
                dbPerson.Adress = new Adress
                {
                };
                this.COVID19Entities.SaveChanges();
            }

            var dbAddress = dbPerson.Adress;

            var model = new NewPersonDtoStep2
            {
                PersonID = id,
                City = dbPerson.Location,
                Comments = dbAddress.Comments,
                Dept = dbAddress.Department,
                Floor = dbAddress.Floor,
                Number = dbAddress.Number,
                State = dbAddress.Province,
                Street = dbAddress.Street
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
            address.Number = model.Number;
            address.Floor = model.Floor;
            address.Department = model.Dept;
            address.Comments = model.Comments;
            address.EffectDate = DateTime.Now;
            address.Location = model.City;
            dbPerson.Location = model.City;

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

            var riskReasons = this.COVID19Entities.RiskReasons.ToList().Select(x =>
                new SelectListItem
                {
                    Text = x.Reason,
                    Value = x.Id.ToString()
                }
            ).ToList();
            ViewBag.riskReasons = riskReasons;

            var yesNoOptions = new List<SelectListItem> {
                new SelectListItem
                {
                    Text = "No",
                    Value = "false"
                },
                new SelectListItem
                {
                    Text = "Si",
                    Value = "true"
                }
            };
            ViewBag.yesNoOptions = yesNoOptions;

            if (!dbPerson.Patients.Any())
            {
                var newDBPatient = new Patient
                {
                    Person = dbPerson,
                    EffectDate = DateTime.Now
                };
                this.COVID19Entities.Patients.Add(newDBPatient);
                this.COVID19Entities.SaveChanges();
            }

            var patient = dbPerson.Patients.First();
            var model = new NewPersonDtoStep4
            {
                PersonID = dbPerson.Personid,
                HaveSymptoms = patient.HaveSymptoms,
                HadInfectedContact = patient.HadInfectedContact,
                RiskGroup = patient.RiskPatient,
                RiskReasonID = patient.RiskReasonId,
                IsReturning = patient.IsReturning,
                CountryEntranceDate = patient.CountryEntranceDAte,
                TravelCountry = patient.TravelCountry,
                PositiveTestDate = patient.PositiveTestDate,
                TreatingDoctor = patient.Doctor
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Medical(NewPersonDtoStep4 model)
        {
            if (!ModelState.IsValid)
            {
                var riskReasons = this.COVID19Entities.RiskReasons.ToList().Select(x =>
                    new SelectListItem
                    {
                        Text = x.Reason,
                        Value = x.Id.ToString()
                    }
                ).ToList();
                ViewBag.riskReasons = riskReasons;

                var yesNoOptions = new List<SelectListItem> {
                    new SelectListItem
                    {
                        Text = "No",
                        Value = "false"
                    },
                    new SelectListItem
                    {
                        Text = "Si",
                        Value = "true"
                    }
                };
                ViewBag.yesNoOptions = yesNoOptions;

                return View(model);
            }

            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == model.PersonID);
            var dbPatient = dbPerson.Patients.First(); // Está garantizado por el flujo de trabajo

            dbPatient.HaveSymptoms = model.HaveSymptoms;
            dbPatient.HadInfectedContact = model.HadInfectedContact;
            dbPatient.RiskPatient = model.RiskGroup;
            dbPatient.RiskReasonId = model.RiskReasonID;
            dbPatient.IsReturning = model.IsReturning;
            dbPatient.CountryEntranceDAte = model.CountryEntranceDate;
            dbPatient.TravelCountry = model.TravelCountry;
            dbPatient.PositiveTestDate = model.PositiveTestDate;
            dbPatient.Doctor = model.TreatingDoctor;

            this.COVID19Entities.SaveChanges();

            return RedirectToAction("Isolation", "Person", new { id = dbPerson.Personid });
        }

        public ActionResult Isolation(int id)
        {
            var yesNoOptions = new List<SelectListItem> {
                    new SelectListItem
                    {
                        Text = "No",
                        Value = "false"
                    },
                    new SelectListItem
                    {
                        Text = "Si",
                        Value = "true"
                    }
                };
            ViewBag.yesNoOptions = yesNoOptions;

            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);
            var dbPatient = dbPerson.Patients.First(); // Está garantizado por el flujo de trabajo

            if (dbPerson.Adress == null)
            {
                dbPerson.Adress = new Adress
                {
                };
                this.COVID19Entities.SaveChanges();
            }

            var model = new NewPersonDtoStep5
            {
                PersonID = dbPatient.PersonId.GetValueOrDefault(),
                InIsolation = dbPatient.Isolation,
                IsolationStart = dbPatient.IsolationDate,
                IsolationEnd = dbPatient.IsolationFinishDate,
                QtyPersonsInTouch = dbPatient.QtyPersonsInTouch,
                ContactStreet = dbPerson.Adress.Street,
                ContactNumber = dbPerson.Adress.Number,
                ContactFloor = dbPerson.Adress.Floor,
                ContactDept = dbPerson.Adress.Department,
                ContactCity = dbPerson.Adress.Location,
                ContactState = dbPerson.Adress.Province
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Isolation(NewPersonDtoStep5 model)
        {
            if (!ModelState.IsValid)
            {
                var yesNoOptions = new List<SelectListItem> {
                    new SelectListItem
                    {
                        Text = "No",
                        Value = "false"
                    },
                    new SelectListItem
                    {
                        Text = "Si",
                        Value = "true"
                    }
                };
                ViewBag.yesNoOptions = yesNoOptions;

                return View(model);
            }

            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == model.PersonID);
            var dbPatient = dbPerson.Patients.First(); // Está garantizado por el flujo de trabajo
            var dbAddress = dbPerson.Adress;

            dbPatient.Isolation = model.InIsolation;
            dbPatient.IsolationDate = model.IsolationStart;
            dbPatient.IsolationFinishDate = model.IsolationEnd;
            dbPatient.QtyPersonsInTouch = model.QtyPersonsInTouch;
            dbAddress.Street = model.ContactStreet;
            dbAddress.Number = model.ContactNumber;
            dbAddress.Floor = model.ContactFloor;
            dbAddress.Department = model.ContactDept;
            dbAddress.Location = model.ContactCity;
            dbAddress.Province = model.ContactState;

            this.COVID19Entities.SaveChanges();

            return RedirectToAction("Brief", "Person", new { id = model.PersonID });
        }

        public ActionResult Brief(int id)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);

            var model = new NewPersonBriefDto
            {
                step1 = new NewPersonDtoStep1
                {
                    PersonID = id,
                    Name = dbPerson.Name,
                    SecondName = dbPerson.SecondName,
                    LastName = dbPerson.LastName,
                    DocumentNumber = dbPerson.DocumentNumber,
                    Gender = dbPerson.Gender,
                    BirrhtDate = dbPerson.BirrhtDate,
                    MaritalSatatusID = dbPerson.MaritalSatatusID
                },
                step2 = new NewPersonDtoStep2
                {
                    PersonID = id,
                    City = dbPerson.Location,
                    Comments = dbPerson.Adress.Comments,
                    Dept = dbPerson.Adress.Department,
                    Floor = dbPerson.Adress.Floor,
                    Number = dbPerson.Adress.Number,
                    State = dbPerson.Adress.Province,
                    Street = dbPerson.Adress.Street
                },
                step3 = new NewPersonDtoStep3
                {
                    PersonID = id,
                    Mail = dbPerson.Mail,
                    MobilePhone = dbPerson.MobileNumber,
                    Phone = dbPerson.TelephoneNumber
                },
                step4 = new NewPersonDtoStep4
                {
                    PersonID = dbPerson.Personid,
                    HaveSymptoms = dbPerson.Patients.First().HaveSymptoms,
                    HadInfectedContact = dbPerson.Patients.First().HadInfectedContact,
                    RiskGroup = dbPerson.Patients.First().RiskPatient,
                    RiskReasonID = dbPerson.Patients.First().RiskReasonId,
                    IsReturning = dbPerson.Patients.First().IsReturning,
                    CountryEntranceDate = dbPerson.Patients.First().CountryEntranceDAte,
                    TravelCountry = dbPerson.Patients.First().TravelCountry,
                    PositiveTestDate = dbPerson.Patients.First().PositiveTestDate,
                    TreatingDoctor = dbPerson.Patients.First().Doctor
                },
                step5 = new NewPersonDtoStep5
                {
                    PersonID = id,
                    InIsolation = dbPerson.Patients.First().Isolation,
                    IsolationStart = dbPerson.Patients.First().IsolationDate,
                    IsolationEnd = dbPerson.Patients.First().IsolationFinishDate,
                    QtyPersonsInTouch = dbPerson.Patients.First().QtyPersonsInTouch,
                    ContactStreet = dbPerson.Adress.Street,
                    ContactNumber = dbPerson.Adress.Number,
                    ContactFloor = dbPerson.Adress.Floor,
                    ContactDept = dbPerson.Adress.Department,
                    ContactCity = dbPerson.Adress.Location,
                    ContactState = dbPerson.Adress.Province
                }
            };

            ViewBag.MaritalStatus = this.COVID19Entities.MaritalStatus.First(x => x.Id == dbPerson.MaritalSatatusID).MaritalStatus;

            var patient = dbPerson.Patients.FirstOrDefault();
            if (patient != null && patient.RiskReasonId.HasValue)
                ViewBag.RiskReason = this.COVID19Entities.RiskReasons.Where(x => x.Id == patient.RiskReasonId.Value).First().Reason;
            else
                ViewBag.RiskReason = "Sin especificar";

            return View(model);
        }

        public ActionResult ConfirmDelete(int id)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);
            dbPerson.NullDate = DateTime.Now;
            this.COVID19Entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var dbPerson = this.COVID19Entities.People.First(x => x.Personid == id);

            var model = new NewPersonBriefDto
            {
                step1 = new NewPersonDtoStep1
                {
                    PersonID = id,
                    Name = dbPerson.Name,
                    SecondName = dbPerson.SecondName,
                    LastName = dbPerson.LastName,
                    DocumentNumber = dbPerson.DocumentNumber,
                    Gender = dbPerson.Gender,
                    BirrhtDate = dbPerson.BirrhtDate,
                    MaritalSatatusID = dbPerson.MaritalSatatusID
                },
                step2 = new NewPersonDtoStep2
                {
                    PersonID = id,
                    City = dbPerson.Location,
                    Comments = dbPerson.Adress.Comments,
                    Dept = dbPerson.Adress.Department,
                    Floor = dbPerson.Adress.Floor,
                    Number = dbPerson.Adress.Number,
                    State = dbPerson.Adress.Province,
                    Street = dbPerson.Adress.Street
                },
                step3 = new NewPersonDtoStep3
                {
                    PersonID = id,
                    Mail = dbPerson.Mail,
                    MobilePhone = dbPerson.MobileNumber,
                    Phone = dbPerson.TelephoneNumber
                },
                step4 = new NewPersonDtoStep4
                {
                    PersonID = dbPerson.Personid,
                    HaveSymptoms = dbPerson.Patients.First().HaveSymptoms,
                    HadInfectedContact = dbPerson.Patients.First().HadInfectedContact,
                    RiskGroup = dbPerson.Patients.First().RiskPatient,
                    RiskReasonID = dbPerson.Patients.First().RiskReasonId,
                    IsReturning = dbPerson.Patients.First().IsReturning,
                    CountryEntranceDate = dbPerson.Patients.First().CountryEntranceDAte,
                    TravelCountry = dbPerson.Patients.First().TravelCountry,
                    PositiveTestDate = dbPerson.Patients.First().PositiveTestDate,
                    TreatingDoctor = dbPerson.Patients.First().Doctor
                },
                step5 = new NewPersonDtoStep5
                {
                    PersonID = id,
                    InIsolation = dbPerson.Patients.First().Isolation,
                    IsolationStart = dbPerson.Patients.First().IsolationDate,
                    IsolationEnd = dbPerson.Patients.First().IsolationFinishDate,
                    QtyPersonsInTouch = dbPerson.Patients.First().QtyPersonsInTouch,
                    ContactStreet = dbPerson.Adress.Street,
                    ContactNumber = dbPerson.Adress.Number,
                    ContactFloor = dbPerson.Adress.Floor,
                    ContactDept = dbPerson.Adress.Department,
                    ContactCity = dbPerson.Adress.Location,
                    ContactState = dbPerson.Adress.Province
                }
            };

            ViewBag.MaritalStatus = this.COVID19Entities.MaritalStatus.First(x => x.Id == dbPerson.MaritalSatatusID).MaritalStatus;

            var patient = dbPerson.Patients.FirstOrDefault();
            if (patient != null && patient.RiskReasonId.HasValue)
                ViewBag.RiskReason = this.COVID19Entities.RiskReasons.Where(x => x.Id == patient.RiskReasonId.Value).First().Reason;
            else
                ViewBag.RiskReason = "Sin especificar";

            return View(model);
        }

    }
}