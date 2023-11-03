using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YTP.Main.DataAccess {

    [MetadataType(typeof(CustomerMetaData))]

    public partial class customer {

        public class CustomerMetaData {

            [DisplayName("Customer Name")]
            public string custname { get; set; }

            [DisplayName("Physical Address")]
            public string address { get; set; }

            [DisplayName("Mobile Number")]
            public int? mobile { get; set; }
        }
    }
}