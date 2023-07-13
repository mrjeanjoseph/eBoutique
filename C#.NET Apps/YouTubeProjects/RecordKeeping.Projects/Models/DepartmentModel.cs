using System;
using System.ComponentModel.DataAnnotations;

namespace RecordKeeping.Projects.Models {
    public class DepartmentModel {
        public short DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; } 
    }
}