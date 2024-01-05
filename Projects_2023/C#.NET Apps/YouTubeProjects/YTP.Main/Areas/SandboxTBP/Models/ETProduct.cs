using System.Collections.Generic;
using System.Diagnostics;
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

		private readonly IDiscountHelper _discounter;
		private static int counter = 0;

		public LinqValueCalculator(IDiscountHelper discountParam) {
			_discounter = discountParam;
			Debug.WriteLine(string.Format("Instance {0} Created", ++counter));
		}
		public decimal ValueProducts(IEnumerable<ETProduct> products) {
			//return products.Sum(x => x.ProductPrice);

			return _discounter.ApplyDiscount(products.Sum(x => x.ProductID));
		}
	}
}