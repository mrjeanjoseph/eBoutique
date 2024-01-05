using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Rehearsals.Models {
    public class Employee {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
    }
}