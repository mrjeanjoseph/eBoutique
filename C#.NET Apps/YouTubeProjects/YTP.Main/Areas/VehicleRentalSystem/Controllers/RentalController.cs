using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.VehicleRentalSystem.Controllers {
    public class RentalController : Controller {

        private readonly DBContext _db = new DBContext();
        // GET: VehicleRentalSystem/Rental
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult GetVehicles() {
            var vehichle = _db.Vehicles.ToList();

            return Json(vehichle, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CustomerById(int id) {

            var customer = (from c in _db.Customers 
                            where c.CustomerId == id 
                            select c.CustomerName)
                            .ToList();

            return Json(customer, JsonRequestBehavior.AllowGet);
        }


    }
}