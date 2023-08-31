using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YTP.Main.Areas.ShoppingCart.Models;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.ShoppingCart.Controllers {
    public class ShoppingController : Controller {

        private readonly DBContext _cartdbcontext;
        private List<VM_Cart> _listcartitemcontext;

        public ShoppingController() {

            _cartdbcontext = new DBContext();
            _listcartitemcontext = new List<VM_Cart>();
        }

        public ActionResult Index() {

            IEnumerable<VM_ShoppingCart> viewModel = (

                    from item in _cartdbcontext.Items
                    join category in _cartdbcontext.Categories
                    on item.CategoryId equals category.CategoryId
                    select new VM_ShoppingCart() {

                        ItemId = item.ItemId,
                        ItemCode = item.ItemCode,
                        Category = category.CategoryName,
                        ItemName = item.ItemName,
                        Description = item.Description,
                        ItemPrice = item.ItemPrice,
                        ImagePath = item.ImagePath

                    }).ToList();
                            
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Index(string ItemId) {

            VM_Cart cartmodel = new VM_Cart();
            Item item = _cartdbcontext.Items.Single(model => model.ItemId.ToString() == ItemId);

            if (Session["CartCounter"] != null) {
                _listcartitemcontext = Session["cartitem"] as List<VM_Cart>;
            }

            if(_listcartitemcontext.Any(model => model.ItemId == ItemId)) {
                cartmodel = _listcartitemcontext.Single(model => model.ItemId == ItemId);
                cartmodel.Quantity++;
                cartmodel.Total = cartmodel.Quantity * cartmodel.UnitPrice;
            }
            else { 
                cartmodel.ItemId = ItemId;
                cartmodel.ItemName = item.ItemName;
                cartmodel.ImagePath = item.ImagePath;
                cartmodel.Quantity = 1;
                cartmodel.Total = item.ItemPrice;
                cartmodel.UnitPrice = item.ItemPrice;

                _listcartitemcontext.Add(cartmodel);
            }

            Session["CartCounter"] = _listcartitemcontext.Count;
            Session["CartItem"] = _listcartitemcontext;

            return Json(new {
                Success = true, 
                Counter = _listcartitemcontext.Count, 
                Message = "Item added to cart successfully"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShoppingCart() {
            _listcartitemcontext = Session["cartitem"] as List<VM_Cart>;
            return View(_listcartitemcontext);
        }
    }
}