using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly DatabaseRepository _db = new DatabaseRepository();
        // GET: Student
        public ActionResult Index()
        {
            var students = _db.GetAll();
            return View(students);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                 var isSaved = _db.Add(student);

                if (isSaved)
                {
                    ViewBag.Message = "Saved Successfully!";
                }
                else
                {
                    ViewBag.Message = "Saved Failed!";
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Something went wrong!");
            }

            return View(student);
        }
    }
}