using System;

namespace EmployeeRecord.Models {
    public class EmployeeModel {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
    }
}