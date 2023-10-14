using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers {

    public class RentalController : Controller {

        readonly supercarEntities _db = new supercarEntities();

        // GET: Rental
        public ActionResult Index() {

            return View();
        }

        [HttpGet]
        public ActionResult GetCar() {

            return Json(_db.carregs.ToList(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Getid(int id) {

            var customer = (from c in _db.customers where c.id == id select c.custname).ToList();
            return Json(customer, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult CheckAvailability(string carno) {

            var availability = (from c in _db.carregs where c.carno == carno select c.available).FirstOrDefault();
            return Json(availability, JsonRequestBehavior.AllowGet);

        }
    }
}