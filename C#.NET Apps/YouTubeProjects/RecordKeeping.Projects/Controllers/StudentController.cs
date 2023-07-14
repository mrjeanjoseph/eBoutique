using RecordKeeping.Projects.DataAccess;
using RecordKeeping.Projects.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;
using System;

namespace RecordKeeping.Projects.Controllers {
    public class StudentController : Controller {
        // GET: Student  
        public ActionResult Index() {
            IEnumerable<Student> students = DataAccessLayer.GetAllStudent();
            return View(students);
        }

        // GET: Student/Details/5  
        public ActionResult Details(int id) {
            return View();
        }

        // GET: Student/Create  
        public ActionResult Create() {
            return View();
        }

        // POST: Student/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student) {
            try {
                // TODO: Add insert logic here  
                DataAccessLayer.AddStudent(student);

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                return View();
            }
        }

        // GET: Student/Edit/5  
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: Student/Edit/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                // TODO: Add update logic here  

                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: Student/Delete/5  
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: Student/Delete/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                // TODO: Add delete logic here  

                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}