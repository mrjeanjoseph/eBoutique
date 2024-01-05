using AdoCrudWebApp.mvc.DataAccess;
using AdoCrudWebApp.mvc.Models;
using System;
using System.Web.Mvc;

namespace AdoCrudWebApp.mvc.Controllers
{
    public class CustomerController : Controller {
        [HttpGet]
        public ActionResult InsertCustomer() {
            return View();

        }

        [HttpPost]
        public ActionResult InsertCustomer(Customer objCustomer) {
            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);

            if (ModelState.IsValid) { //checking model is valid or not
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
            Customer objCustomer = new Customer();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata
            objCustomer.ShowallCustomer = objDB.Selectalldata();
            return View(objCustomer);

        }
    }
}
