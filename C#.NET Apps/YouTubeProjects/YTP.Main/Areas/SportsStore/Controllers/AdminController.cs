﻿using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Main.Areas.SportsStore.Controllers {

    public class AdminController : Controller {

        private readonly IProductsRepository _productRepo;

        public AdminController(IProductsRepository products) {            
            _productRepo = products;
        }

        // GET: SportsStore/Admin
        public ViewResult Index() {
            return View(_productRepo.Products);
        }

        public ViewResult Edit(int productId) {
            Product product = _productRepo.Products
                .FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(Product product) {
            if(ModelState.IsValid) {
                _productRepo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.ProductName);
                return RedirectToAction("Index");
            } else {
                // There is something wrong with the data values
                return View(product);
            }
        }
    }
}