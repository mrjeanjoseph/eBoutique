using RecordKeeping.Projects.DataAccess;
using RecordKeeping.Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecordKeeping.Projects.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Customer Customer) {
            Customer.Birthdate = Convert.ToDateTime(Customer.Birthdate);

            if (ModelState.IsValid){ //checking model is valid or not
                DataAccessLayer objDB = new DataAccessLayer();
                string result = objDB.InsertData(Customer);
                ViewData["result"] = result;

                ModelState.Clear(); //clearing model
                return View();
            } else {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        public ActionResult Insert() {
            return View();
        }
    }
}