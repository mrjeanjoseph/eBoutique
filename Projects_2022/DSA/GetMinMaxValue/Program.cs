using System;
using System.Collections.Generic;

namespace GetMinMaxValue {
    class Program {
        static void Main(string[] args) {
            List<int> list = new List<int>() { 5, -1, 4, 9, -7, 8 };

            Console.WriteLine("Maximum element " + list.findMax());
            Console.WriteLine("Minimum element " + list.findMin());

            Console.ReadLine();
        }
    }
}
