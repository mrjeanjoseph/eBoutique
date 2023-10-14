using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models {

    [MetadataType(typeof(customerMetaData))]
    public partial class customer {
        public class customerMetaData {


            [DisplayName("Customer Name")]
            public string custname { get; set; }

            [DisplayName("Physical Address")]
            public string address { get; set; }

            [DisplayName("Mobile Number")]
            public int? mobile { get; set; }
        }
    }
}