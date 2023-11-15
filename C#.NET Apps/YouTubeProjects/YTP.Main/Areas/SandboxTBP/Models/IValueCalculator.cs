using System.Collections.Generic;

namespace YTP.Main.Areas.SandboxTBP.Models {
    public interface IValueCalculator {
		decimal ValueProducts(IEnumerable<ETProduct> products);
	}
}