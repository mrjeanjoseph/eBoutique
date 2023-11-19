using YTP.Domain.SportsStore.Entities;

namespace YTP.Domain.SportsStore.Abstract {
    public interface IOrderProcessor {

        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
