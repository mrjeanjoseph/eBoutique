using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class CustomerController : Controller {
        // GET: Customer
        public ActionResult Index() {
            CustomerDBHandle dBHandle = new CustomerDBHandle();
            ModelState.Clear();

            return View(dBHandle.GetCustomer());
        }
    }
}
