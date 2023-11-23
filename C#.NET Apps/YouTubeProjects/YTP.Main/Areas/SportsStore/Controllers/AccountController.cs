using System.Web.Mvc;
using YTP.Main.Areas.SportsStore.Models;
using YTP.Main.Infrastructure.Abstract;

namespace YTP.Main.Areas.SportsStore.Controllers {
    public class AccountController : Controller {

        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider) {
            _authProvider = authProvider;
        }   

        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login_VM model, string returnUrl) {

            if(ModelState.IsValid) {
                if(_authProvider.Authenticate(model.UserName, model.Password)) {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                } else {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            } else {
                return View();
            }
        }
    }
}