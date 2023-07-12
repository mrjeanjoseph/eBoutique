using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordKeeping.Projects.Models {
    public class DepartmentModel {
        public short DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public System.DateTime ModifiedDate { get; set; } 
    }
}