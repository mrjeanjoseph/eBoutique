using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.CarRentalSystem.Controllers {
    public class CarController : Controller {
        private readonly YTP_DBContext db = new YTP_DBContext();

        // GET: CarRentalSystem/Car
        public ActionResult Index() {
            return View(db.carregs.ToList());
        }

        // GET: CarRentalSystem/Car/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carreg carreg = db.carregs.Find(id);
            if (carreg == null) {
                return HttpNotFound();
            }
            return View(carreg);
        }

        // GET: CarRentalSystem/Car/Create
        public ActionResult Create() {
            return View();
        }

        // POST: CarRentalSystem/Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,carno,make,model,available")] carreg carreg) {
            if (ModelState.IsValid) {
                db.carregs.Add(carreg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carreg);
        }

        // GET: CarRentalSystem/Car/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carreg carreg = db.carregs.Find(id);
            if (carreg == null) {
                return HttpNotFound();
            }
            return View(carreg);
        }

        // POST: CarRentalSystem/Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,carno,make,model,available")] carreg carreg) {
            if (ModelState.IsValid) {
                db.Entry(carreg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carreg);
        }

        // GET: CarRentalSystem/Car/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carreg carreg = db.carregs.Find(id);
            if (carreg == null) {
                return HttpNotFound();
            }
            return View(carreg);
        }

        // POST: CarRentalSystem/Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            carreg carreg = db.carregs.Find(id);
            db.carregs.Remove(carreg);
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
