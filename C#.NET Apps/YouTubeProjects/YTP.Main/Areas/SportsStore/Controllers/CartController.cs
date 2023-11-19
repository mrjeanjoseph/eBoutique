using System;
using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SportsStore.Models;

namespace YTP.Main.Areas.SportsStore.Controllers {
    public class CartController : Controller {

        private readonly IProductsRepository _repository;

        public CartController(IProductsRepository repository) {
            _repository = repository;
        }

        public ActionResult Index(string returnUrl) {
            return View(new CartIndex_VM {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl) {
            Product product = _repository.Products
                    .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                GetCart().AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl) {
            Product product = _repository.Products
        .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                GetCart().RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart() {

            Cart cart = (Cart)Session["Cart"];
            if (cart == null) {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}