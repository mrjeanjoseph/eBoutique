using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.DataAccess;
using YTP.Main.Models;

namespace YTP.Main.Controllers {

    public class CustomerController : Controller {

        // GET: Customer
        public ActionResult Index() {

            return View();
        }

        // GET: /Customer/
        [HttpGet]
        public ActionResult InsertCustomer() {

            return View();
        }

        [HttpPost]

        public ActionResult InsertCustomer(CustomerModel objCustomer) {

            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);

            if (ModelState.IsValid){ //checking model is valid or not

                DataAccessLayer objDB = new DataAccessLayer();
                string result = objDB.InsertData(objCustomer);
                ViewData["result"] = result;
                ModelState.Clear(); //clearing model
                return View();

            } else {

                ModelState.AddModelError("", "Error in saving data");
                return View();

            }
        }

        [HttpGet]
        public ActionResult ShowAllCustomerDetails() {

            CustomerModel objCustomer = new CustomerModel();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata
            objCustomer.ShowallCustomer = objDB.Selectalldata();

            return View(objCustomer);

        }
    }
}