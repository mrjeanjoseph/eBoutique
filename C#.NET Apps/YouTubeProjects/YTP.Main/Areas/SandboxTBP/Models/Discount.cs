

namespace YTP.Main.Models {
    public interface IDiscountHelper {
        decimal ApplyDiscount(decimal discount);
    }

    public class DefaultDiscountHelper : IDiscountHelper {

        public decimal? DiscountSize { get; set; }
        public decimal ApplyDiscount(decimal totalParam) {
            return ((decimal)(totalParam - (DiscountSize / 100m * totalParam)));
        }
    }
}