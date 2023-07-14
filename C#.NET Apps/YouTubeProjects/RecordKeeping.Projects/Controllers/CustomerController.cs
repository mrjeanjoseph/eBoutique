using RecordKeeping.Projects.DataAccess;
using RecordKeeping.Projects.Models;
using System.Web.Mvc;
using System;

namespace RecordKeeping.Projects.Controllers {
    public class CustomerController : Controller {
        // GET: Customer
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Customer Customer) {
            Customer.Birthdate = Convert.ToDateTime(Customer.Birthdate);

            if (ModelState.IsValid) { //checking model is valid or not
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

        [HttpGet]
        public ActionResult DisplayAll() {

            Customer objCustomer = new Customer();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata

            objCustomer.ShowallCustomer = objDB.Selectalldata();
            return View(objCustomer);

        }

        [HttpGet]
        public ActionResult Edit(string ID) {

            Customer objCustomer = new Customer();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpPost]
        public ActionResult Edit(Customer objCustomer) {
            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);

            if (ModelState.IsValid){ //checking model is valid or not
                DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata
                string result = objDB.UpdateData(objCustomer);
                ViewData["result"] = result;
                ModelState.Clear(); //clearing model
                return View();

            } else {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(string ID) {
            Customer objCustomer = new Customer();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpPost]
        public ActionResult Delete(Customer objCustomer) {
            DataAccessLayer objDB = new DataAccessLayer();
            string result = objDB.DeleteData(objCustomer);

            ViewData["result"] = result;
            ModelState.Clear(); //clearing model
            return View();
        }
    }
}