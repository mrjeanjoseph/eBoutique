using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.CarRentalSystem.Controllers {
    public class RentalController : Controller {

        readonly YTP_DBContext _db = new YTP_DBContext();

        // GET: Rental
        public ActionResult Index() {

            var result = (from r in _db.rentals
                          join c in _db.carregs
                          on r.carid equals c.carno
                          select new RentalViewModel {
                              id = r.id,
                              carid = r.carid,
                              custid = r.custid,
                              fee = r.fee ?? 100,
                              sdate = r.sdate,
                              edate = r.edate,
                              available = c.available
                          }).ToList();

            if(result.Count == 0)
                ViewBag.DisplayRentals = false; // There is an error here to be solved.
            

            return View(result);
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

        [HttpPost]
        public ActionResult Save(rental rental) {
            if (ModelState.IsValid) {
                _db.rentals.Add(rental);

                var car = _db.carregs.FirstOrDefault(c => c.carno == rental.carid);
                if (car == null) return HttpNotFound("Car No NOT FOUND");

                car.available = "no";
                _db.Entry(car).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rental);
        }
    }
}