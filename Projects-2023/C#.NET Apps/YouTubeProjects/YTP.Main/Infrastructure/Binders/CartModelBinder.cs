using System.Web.Mvc;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Main.Infrastructure.Binders {
    public class CartModelBinder : IModelBinder{

        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {

            //Get the cart from the session
            Cart cart = null;
            if(controllerContext.HttpContext.Session != null) 
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            
            if(cart == null ) {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                
            }

            return cart;
        }
    }
}
