

namespace YTP.Main.Areas.SandboxTBP.Models {
    public interface IDiscountHelper {
        decimal ApplyDiscount(decimal totalParam);
    }

    public class DefaultDiscountHelper : IDiscountHelper {

        //public decimal? DiscountSize { get; set; }
        public decimal? DiscountSize;

        public DefaultDiscountHelper(decimal discountParam) {
            this.DiscountSize = discountParam;
        }
        public decimal ApplyDiscount(decimal totalParam) {
            return (decimal)(totalParam - (DiscountSize / 100m * totalParam));
        }
    }
}