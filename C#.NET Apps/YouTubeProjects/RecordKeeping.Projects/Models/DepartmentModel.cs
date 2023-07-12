using System;

namespace RecordKeeping.Projects.Models {
    public class DepartmentModel {
        public short DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public DateTime ModifiedDate { get; set; } 
    }
}