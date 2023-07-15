using RecordKeeping.Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecordKeeping.Projects.Controllers {
    public class CustomerController : Controller {

        public ActionResult Index() {
            return View();
        }
        // GET: /Customer/
        [HttpGet] public ActionResult InsertCustomer() {

            return View();
        }

        [HttpPost] public ActionResult InsertCustomer(Customer objCustomer) {

            return View();
        }
    }
}