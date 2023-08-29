using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using YTP.Main.Areas.ShoppingCart.Models;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.ShoppingCart.Controllers {
    public class ShoppingController : Controller {

        private readonly DBContext _cartdbcontext;

        public ShoppingController() {

            _cartdbcontext = new DBContext();
        }

        public ActionResult Index() {

            IEnumerable<VM_ShoppingCart> viewModel = (
                                from item in _cartdbcontext.Items
                                join category in _cartdbcontext.Categories
                                on item.CategoryId equals category.CategoryId
                                select new VM_ShoppingCart() {

                                    ImagePath = item.ImagePath,
                                    ItemName = item.Name,
                                    Description = item.Description,
                                    ItemPrice = item.Price,
                                    ItemId = item.Id,
                                    Category = category.CategoryName

                                }).ToList();
                            
            return View(viewModel);
        }
    }
}