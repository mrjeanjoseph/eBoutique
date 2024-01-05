using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SportsStore.Models;

namespace YTP.Main.Areas.SportsStore.Controllers {
    public class CartController : Controller {

        private readonly IProductsRepository _repository;
        private readonly IOrderProcessor _orderProcessor;

        public CartController(IProductsRepository repository, IOrderProcessor orderProcessor) {
            _repository = repository;
            _orderProcessor = orderProcessor;
        }

        public ViewResult Index(Cart cart, string returnUrl) {
            return View(new CartIndex_VM {
                ReturnUrl = returnUrl,
                Cart = cart,
            });
        }

        //public ActionResult Index(string returnUrl) {
        //    return View(new CartIndex_VM {
        //        Cart = GetCart(),
        //        ReturnUrl = returnUrl
        //    });
        //}

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl) {
            Product product = _repository.Products
                    .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
        //public RedirectToRouteResult AddToCart(int productId, string returnUrl) {
        //    Product product = _repository.Products
        //            .FirstOrDefault(p => p.ProductID == productId);

        //    if (product != null) {
        //        GetCart().AddItem(product, 1);
        //    }

        //    return RedirectToAction("Index", new { returnUrl });
        //}

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl) {
            Product product = _repository.Products
        .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
        //public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl) {
        //    Product product = _repository.Products
        //.FirstOrDefault(p => p.ProductID == productId);

        //    if (product != null) {
        //        GetCart().RemoveLine(product);
        //    }

        //    return RedirectToAction("Index", new { returnUrl });
        //}

        private Cart GetCart() { //We're using the cart model binders instead

            Cart cart = (Cart)Session["Cart"];
            if (cart == null) {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public PartialViewResult Summary(Cart cart) {
            return PartialView("_Summary", cart);
        }


        public ViewResult CheckoutCart() {

            return View(new ShippingDetails());
        }

        [HttpPost]

        public ViewResult CheckoutCart(Cart cart, ShippingDetails shippingDetails) {


            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry your cart is empty");

            if(ModelState.IsValid) {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();

                return View("CheckoutCompleted");

            } else {
                return View(new ShippingDetails());
            }
        }
    }
}