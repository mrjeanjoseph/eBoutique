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
            var getVehicles = _db.Vehicles.ToList();

            return Json(getVehicles, JsonRequestBehavior.AllowGet);
        }


    }
}