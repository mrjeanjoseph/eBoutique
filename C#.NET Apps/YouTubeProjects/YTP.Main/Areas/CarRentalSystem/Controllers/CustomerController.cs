using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.CarRentalSystem.Controllers {
    public class CustomerController : Controller {
        private readonly YTP_DBContext db = new YTP_DBContext();

        // GET: CarRentalSystem/Customer
        public ActionResult Index() {
            return View(db.customers.ToList());
            //return Json(db.customers.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: CarRentalSystem/Customer/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null) {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: CarRentalSystem/Customer/Create
        public ActionResult Create() {
            return View();
        }

        // POST: CarRentalSystem/Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,custname,address,mobile")] customer customer) {
            if (ModelState.IsValid) {
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: CarRentalSystem/Customer/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null) {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: CarRentalSystem/Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,custname,address,mobile")] customer customer) {
            if (ModelState.IsValid) {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: CarRentalSystem/Customer/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null) {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: CarRentalSystem/Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
