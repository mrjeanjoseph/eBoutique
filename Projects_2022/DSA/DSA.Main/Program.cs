using System;

namespace DSA.Main {
    class Program {
        static void Main(string[] args) {

            DateTime myDate = DateTime.Now;
            Console.WriteLine(myDate);
            Console.ReadLine();
        }

        public static void MyDSA() {

            DSA mydsa = new DSA(001, "ValueOne", "A0045B5-6XX", Convert.ToDateTime("04/06/1997"));

            Console.WriteLine("My DSA ID: " + mydsa.dsaID);
            Console.WriteLine("My DSA Name: " + mydsa.dsaName);
            Console.WriteLine("My DSA AltID: " + mydsa.dsaAltID);
        }
    }
}
