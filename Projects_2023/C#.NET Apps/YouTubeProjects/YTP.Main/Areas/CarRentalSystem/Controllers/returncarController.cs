using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.CarRentalSystem.Controllers {
    public class ReturncarController : Controller {

        readonly YTP_DBContext _db = new YTP_DBContext();


        // GET: returncar
        public ActionResult Index() {

            return View();
        }

        // GET: returncar
        public ActionResult Save(returncar recar) {

            if(ModelState.IsValid) {

                _db.returncars.Add(recar);

                var car = _db.carregs.SingleOrDefault(c => c.carno == recar.carno);

                if(car == null)
                    return HttpNotFound("Car No. not Found!");

                car.available = "yes";
                _db.Entry(car).State = EntityState.Modified;

                _db.SaveChanges();
                return RedirectToAction("Index");
                
            }

            return View(recar);
        }

        [HttpPost]
        public ActionResult Getid(string carno) {

            var car = (from c in _db.rentals
                       where c.carid == carno
                       select new {
                           StartDate = c.sdate,
                           EndDate = c.edate,
                           Custid = c.custid,
                           CarNo = c.carid,
                           Fee = c.fee,
                           ElapseDays = SqlFunctions.DateDiff("day", c.edate, DateTime.Now)

                       }).ToArray();

            return Json(car, JsonRequestBehavior.AllowGet);

        }
    }
}