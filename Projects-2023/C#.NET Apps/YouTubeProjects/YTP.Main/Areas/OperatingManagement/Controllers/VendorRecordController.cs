using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using YTP.Main.Areas.OperatingManagement.Models;

namespace YTP.Main.Areas.OperatingManagement.Controllers {
    public class VendorRecordController : Controller {
        // GET: VendorRecord
        public ActionResult Index() {

            IEnumerable<VendorRecordModel> vendorRec;

            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("VendorRecord").Result;
            vendorRec = response.Content.ReadAsAsync<IEnumerable<VendorRecordModel>>().Result;

            return View(vendorRec);
        }

        public ActionResult AddOrEdit(int id = 0) {

            if (id == 0)
                return View(new VendorRecordModel());
            else {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("VendorRecord/" + id.ToString()).Result;

                return View(response.Content.ReadAsAsync<VendorRecordModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(VendorRecordModel emp) {

            if (emp.VendorId == 0) {
                HttpResponseMessage responseData = GlobalVariables.webApiClient.PostAsJsonAsync("VendorRecord", emp).Result;
                TempData["SuccessMessage"] = "Record Added Successfully";

            } else {
                HttpResponseMessage responseData = GlobalVariables.webApiClient.PutAsJsonAsync("VendorRecord/" + emp.VendorId, emp).Result;
                TempData["SuccessMessage"] = "Record Updated Successfully";

            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {

            HttpResponseMessage responseData = GlobalVariables.webApiClient.DeleteAsync("VendorRecord/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Record Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}