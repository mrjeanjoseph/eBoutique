using System;

namespace YTP.Main.Areas.CarRentalSystem {
    public class RentalViewModel {
        public int id { get; set; }
        public string carid { get; set; }
        public int? custid { get; set; }
        public int? fee { get; set; }
        public DateTime? sdate { get; set; }
        public DateTime? edate { get; set; }
        public string available { get; set; }
    }
}