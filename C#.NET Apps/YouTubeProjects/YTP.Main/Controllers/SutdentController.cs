using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Controllers
{
    public class SutdentController : Controller
    {
        // GET: Sutdent
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sutdent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sutdent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sutdent/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sutdent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sutdent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sutdent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sutdent/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
