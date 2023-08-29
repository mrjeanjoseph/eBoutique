﻿using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using YTP.Main.DataAccess;
using YTP.Main.Models;

namespace YTP.Main.Areas.ShoppingCart.Controllers {

    public class ItemController : Controller {

        private readonly DBContext _cartdbcontext;

        public ItemController() {

            _cartdbcontext = new DBContext();
        }
        // GET: ShoppingCart/Item
        public ActionResult Index() {

            VM_Item viewModel = new VM_Item();
            viewModel.CategorySelectListItem = from objCat in _cartdbcontext.Categories
                    select new SelectListItem() { // This is to load all the categories into the dropdown.
                        Text = objCat.CategoryName,
                        Value = objCat.CategoryId.ToString(),
                        Selected = true
                    };
            
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult SaveItems(VM_Item viewModel) {

            string newImage = Guid.NewGuid() + Path.GetExtension(viewModel.ImagePath.FileName);
            string shoppingCartDir = "~/Areas/ShoppingCart/Content/Images/";
            viewModel.ImagePath.SaveAs(Server.MapPath($"~/Content/images/{newImage}"));

            Item objItem = new Item {
                ImagePath = $"{shoppingCartDir}{newImage}",
                CategoryId = viewModel.CategoryId,
                Description = viewModel.Description,
                ItemId = Guid.NewGuid(),
                ItemCode = viewModel.ItemCode,
                ItemName = viewModel.ItemName,
                ItemPrice = viewModel.ItemPrice
            };

            _cartdbcontext.Items.Add(objItem);
            _cartdbcontext.SaveChanges();

            return Json(new {Success = true, Message = "Entry Added Successfully!"}, JsonRequestBehavior.AllowGet);
        }
    }
}