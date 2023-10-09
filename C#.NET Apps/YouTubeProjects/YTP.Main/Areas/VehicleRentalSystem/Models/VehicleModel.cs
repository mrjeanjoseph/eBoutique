using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Areas.VehicleRentalSystem.Models {

    [MetadataType(typeof(VehicleMetaData))]

    public partial class VehicleModel {

        public class VehicleMetaData {

            public int VehicleId { get; set; }

            [DisplayName("Vehicle Number")]
            public string RegNo { get; set; }
            [DisplayName("Vehicle Make")]
            public string Make { get; set; }
            [DisplayName("Vehicle Model")]
            public string Model { get; set; }
            public string Trim { get; set; }
            [DisplayName("Available")]
            public string Status { get; set; }
        }
    }
}