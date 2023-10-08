﻿using System;

namespace YTP.Main.Models {
    public class VendorRecordModel {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string Location { get; set; }
        public Nullable<int> Priority { get; set; }
        public string WebsiteAddress { get; set; }
        public string MainContact { get; set; }
    }
}