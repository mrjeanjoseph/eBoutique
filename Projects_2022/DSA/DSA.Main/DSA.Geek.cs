using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSA.Main {
    class DSA {
        public int dsaID;
        public string dsaName;
        public string dsaAltID;
        public DateTime createdDate;
        public DateTime modifiedDate;
        public bool isActive;
        public List<DSA> dsaList;
        public string[] dsaMultValues;

        public DSA() {

        }

        public DSA(int dsaID, string dsaName, string dsaAltID, DateTime createdDate) {
            this.dsaID = dsaID;
            this.dsaName = dsaName;
            this.dsaAltID = dsaAltID;
            this.createdDate = createdDate;
            this.dsaList = new List<DSA>();
        }
    }

    class Geeks {

        private string month;
        private int year;

        // declaring Copy constructor
        public Geeks(Geeks s) {
            month = s.month;
            year = s.year;
        }

        // Instance constructor
        public Geeks(string month, int year) {
            this.month = month;
            this.year = year;
        }

        // Get details of Geeks
        public string Details {
            get {
                return "Month: " + month.ToString() +
                         "\nYear: " + year.ToString();
            }
        }

        // Main Method
        //public void NotMainYet() {

        //    // Create a new Geeks object.
        //    Geeks g1 = new Geeks("June", 2018);

        //    // here is g1 details is copied to g2.
        //    Geeks g2 = new Geeks(g1);

        //    Console.WriteLine(g2.Details);
        //}
    }
}
