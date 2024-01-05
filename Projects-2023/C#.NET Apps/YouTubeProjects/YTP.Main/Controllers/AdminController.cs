using System.Web.Mvc;
using YTP.Main.DataAccess;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class AdminController : Controller {

        private readonly IMembersRepository _membersRepo;

        public AdminController(IMembersRepository repoParam) {
            _membersRepo = repoParam;
        }
        // GET: Admin
        public ActionResult Index() {
            return View();
        }

        public ActionResult ChangeLoginName(string oldLoginParam, string newLoginParam) {
            Member member = _membersRepo.FetchByLoginName(oldLoginParam);
            member.LoginName = newLoginParam;
            _membersRepo.SubmitChanges();

            return View();
        }
    }
}