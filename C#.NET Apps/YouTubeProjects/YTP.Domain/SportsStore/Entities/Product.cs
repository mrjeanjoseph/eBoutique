using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YTP.Domain.SportsStore.Entities {

    public class Product {

        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage ="Please enter Product Name")]
        public string ProductName { get; set; }

        //[DataType(DataType.MultilineText)]

        [Required(ErrorMessage = "Please enter Product Description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please confirm the pricing value you entered")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Please enter Product Category")]
        public string Category { get; set; }

        public int IsAvailable { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

    }

}
