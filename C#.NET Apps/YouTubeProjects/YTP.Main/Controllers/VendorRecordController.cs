using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using YTP.Main.Models; 

namespace YTP.Main.Controllers {
    public class VendorRecordController : Controller {
        // GET: VendorRecord
        public ActionResult Index() {

            IEnumerable<VendorRecordModel> vendorRec;

            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("VendorRecord").Result;
            vendorRec = response.Content.ReadAsAsync<IEnumerable<VendorRecordModel>>().Result;

            return View(vendorRec);
        }

        public ActionResult AddOrEdit(int id = 0) {
            return View(new VendorRecordModel());
        }

        [HttpPost]
        public ActionResult AddOrEdit(VendorRecordModel emp) {

            HttpResponseMessage responseData = GlobalVariables.webApiClient.PostAsJsonAsync("VendorRecord", emp).Result;
            TempData["SuccessMessage"] = "Saved Successfully";
            return RedirectToAction("Index");
        }
    }
}