using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YTP.Domain.SportsStore.Entities {

    public class Product {

        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public decimal ProductPrice { get; set; }
        public string Category { get; set; }
        public int IsAvailable { get; set; }

    }

}
