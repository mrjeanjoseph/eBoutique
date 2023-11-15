using System.Collections.Generic;
using System.Linq;

namespace YTP.Main.Areas.SandboxTBP.Models {
    public class ETProduct {
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public decimal ProductPrice { get; set; }
		public string Category { get; set; }
		public int IsAvailable { get; set; }

	}

	public class ETShoppingCart {
		//private readonly LinqValueCalculator _calc;
		private readonly IValueCalculator _calc;

		public ETShoppingCart(IValueCalculator lvcParam) {
			_calc = lvcParam;
		}

		public IEnumerable<ETProduct> Products { get; set; }

		public decimal CalculatorProductTotal() {
			return _calc.ValueProducts(Products);
		}
	}

	public class LinqValueCalculator : IValueCalculator {
		public decimal ValueProducts(IEnumerable<ETProduct> products) {
			return products.Sum(x => x.ProductPrice);
		}
	}
}