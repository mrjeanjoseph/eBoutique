using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using YTP.Main.DataAccess;
using YTP.Main.Models;

namespace YTP.Main.Areas.ShoppingCart.Controllers {

    public class ItemController : Controller {

        private readonly DBContext _itemdbcontext;

        public ItemController() {

            _itemdbcontext = new DBContext();
        }
        // GET: ShoppingCart/Item
        public ActionResult Index() {

            VM_Item viewModel = new VM_Item {
                CategorySelectListItem = 
                from objCat in _itemdbcontext.Categories
                select new SelectListItem() { // This is to load all the categories into the dropdown.
                    Text = objCat.CategoryName,
                    Value = objCat.CategoryId.ToString(),
                    Selected = true
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult SaveItems(VM_Item viewModel) {

            string newImage = Guid.NewGuid() + Path.GetExtension(viewModel.ImagePath.FileName);
            string shoppingCartDir = $"~/Areas/ShoppingCart/Content/images/{newImage}";
            viewModel.ImagePath.SaveAs(Server.MapPath(shoppingCartDir));

            Item objItem = new Item {

                ImagePath = shoppingCartDir,
                CategoryId = viewModel.CategoryId,
                Description = viewModel.Description,
                ItemId = Guid.NewGuid(),

                ItemCode = viewModel.ItemCode,
                ItemName = viewModel.ItemName,
                ItemPrice = viewModel.ItemPrice

            };

            _itemdbcontext.Items.Add(objItem);
            _itemdbcontext.SaveChanges();

            return Json(new {Success = true, Message = "Entry Added Successfully!"}, JsonRequestBehavior.AllowGet);
        }
    }
}