using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordKeeping.Projects.Utility {
    public class ConnectionString {
        private static string cName = "Data Source=JEANPC; Initial Catalog=Student;";
        public static string ConnName { get => ConnName; }
    }
}