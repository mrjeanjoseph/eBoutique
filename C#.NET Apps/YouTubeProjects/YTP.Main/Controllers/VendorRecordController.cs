using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
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
    }
}