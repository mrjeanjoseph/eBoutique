using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models {

    [MetadataType(typeof(CarregMetaData))]
    public partial class carreg {
        public class CarregMetaData {

            [DisplayName("Car No")]
            public string carno { get; set; }

            [DisplayName("Make")]
            public string make { get; set; }

            [DisplayName("Model")]
            public string model { get; set; }

            [DisplayName("Available")]
            public string available { get; set; }
        }
    }
}