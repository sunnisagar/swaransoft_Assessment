using swaransoft_Assessment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace swaransoft_Assessment.Controllers
{
    public class StudentsController : Controller
    {
        private readonly dbContext _context = new dbContext();

        // GET: Students
        public ActionResult Index()
        {
            if (!_context.States.Any())
            {
                var states = new List<State>
            {
                new State { StateName = "Uttar Pradesh" },
                new State { StateName = "Rajasthan" },
                new State { StateName = "Haryana" },
                new State { StateName = "Maharashtra" },
                new State { StateName = "Punjab" }
            };

                _context.States.AddRange(states);
                _context.SaveChanges();
            }

            if (!_context.Cities.Any())
            {
                var uttarPradeshId = _context.States.First(s => s.StateName == "Uttar Pradesh").StateId;
                var rajasthanId = _context.States.First(s => s.StateName == "Rajasthan").StateId;
                var haryana = _context.States.First(s => s.StateName == "Haryana").StateId;
                var maharashtra = _context.States.First(s => s.StateName == "Maharashtra").StateId;
                var punjab = _context.States.First(s => s.StateName == "Punjab").StateId;

                var cities = new List<City>
            {
                new City { StateId = uttarPradeshId, CityName = "Agra" },
                new City { StateId = uttarPradeshId, CityName = "Noida" },
                new City { StateId = rajasthanId, CityName = "Jaipur" },
                new City { StateId = rajasthanId, CityName = "Udaipur" },
                new City { StateId = haryana, CityName = "Gurugram" },
                new City { StateId = haryana, CityName = "Faridabad" },
                new City { StateId = maharashtra, CityName = "Mumbai" },
                new City { StateId = maharashtra, CityName = "Nagpur" },
                new City { StateId = punjab, CityName = "Amritsar" },
                new City { StateId = punjab, CityName = "Jalandhar" }
            };

                _context.Cities.AddRange(cities);
                _context.SaveChanges();
            }


            var Students = _context.Students.ToList();
            return View(Students);
        }

        public ActionResult Create(int? id, int? stateId, string name, string email, string mobile)
        {
            Student model = new Student();
            model.CascadingModel = new CascadingModel();
            if (id != null)
            {
                model = _context.Students.SingleOrDefault(e => e.StudentId == id);
            }
            else
            {
                model.Name = name;
                model.Email = email;
                model.Mobile = mobile;
            }
            CascadingModel(model, stateId, model.CityId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.PhotoFile != null && student.PhotoFile.ContentLength > 0)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(student.PhotoFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(student.PhotoFile.ContentLength);
                    }
                    student.PhotoData = imageData;
                }

                _context.Students.Add(student);

                if (student.StudentId > 0) { _context.Entry(student).State = EntityState.Modified; }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else { CascadingModel(student, student.StateId, student.CityId); }
            return View(student);
        }

        private void CascadingModel(Student model, int? stateId, int? cityId)
        {
            model.CascadingModel = new CascadingModel();
            foreach (var state in _context.States)
            {
                model.CascadingModel.States.Add(new SelectListItem { Text = state.StateName, Value = state.StateId.ToString() });
            }

            if (stateId.HasValue)
            {
                var cities = (from city in _context.Cities
                              where city.StateId == stateId.Value
                              select city).ToList();
                foreach (var city in cities)
                {
                    if (cityId != null && cityId != 0)
                    {
                        if (city.CityId == cityId)
                            model.CascadingModel.Cities.Add(new SelectListItem { Text = city.CityName, Value = city.CityId.ToString() });
                    }
                    else
                    {
                        model.CascadingModel.Cities.Add(new SelectListItem { Text = city.CityName, Value = city.CityId.ToString() });
                    }
                }
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = _context.Students.SingleOrDefault(e => e.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.SingleOrDefault(x => x.StudentId == id);
            _context.Students.Remove(student ?? throw new InvalidOperationException());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}