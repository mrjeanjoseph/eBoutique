using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicAlgorithms.Deux {
    class Program {
        static void Main(string[] args) {
            /*
            Console.WriteLine(ListAllPrimeValues(125));
            Console.WriteLine(CalculateSquareRoot(81));
            string[] arr_strings = { "Where", "Who", "What", "Whatever" };
            Console.WriteLine($"{string.Join(", ", arr_strings)}");
            Console.WriteLine(GetLongestArrayOfStrings(arr_strings));            
            ListAllPrimeInDescendingOrder();
            int[] listOfNum = { -4, -3, -2, 0, 3, 5, 6, 2, 6 };
            Console.WriteLine(GetPositiveOrNegative(listOfNum));
            Console.WriteLine(CountLettersAndDigitsOfString("dsfkaso230samdm2423sa"));
            Console.WriteLine(IndexNumberOfLowercaseLetters("Which that you do most"));

            */

            Console.ReadLine();
        }

        public static string IndexNumberOfLowercaseLetters(string inputStr) {
            //Index number of all lower case letters in a given string
            string result;
            int[] convertStr = inputStr.Select((x, i) => i).Where(i => char.IsLower(inputStr[i])).ToArray();

            result = $"Original string: {inputStr}";
            result += "\nIndices of all lower case letters of the said string:\n";
            foreach (var item in convertStr) {
                result += $"{item}, ".TrimEnd(',');
            }
            return result;
        }
        public static string CountLettersAndDigitsOfString(string givenStr) {
            int letterCounts = givenStr.Count(char.IsLetter);
            int digitCounts = givenStr.Count(char.IsDigit);
            int letterOrDigitCounts = givenStr.Count(char.IsLetterOrDigit);
            return $"Letters: {letterCounts}\n" +
                $"Digits: {digitCounts}\n" +
                $"Let/Dig: {letterOrDigitCounts}";
        }
        public static string GetPositiveOrNegative(int[] nums) {
            //Count positive and negative numbers in a given array of integers
            var pos = nums.Where(n => n > 0);
            var neg = nums.Where(n => n < 0);

            string result = "Original Array elements:\n";            
            foreach (var item in nums) {
                result += $"{item}, ";
            }
            
            return $"{result.TrimEnd(',')}\nNumber of positive numbers: {pos.Count()}" +
                $"\nNumber of negative numbers: {neg.Count()}";
        }
        public static int CalculateSquareRoot(double num) {
            int sq = 1;
            while (sq < num / sq) {
                sq++;
            }
            if (sq > num / sq) return sq - 1;
            return sq;
        }
        public static string GetLongestArrayOfStrings(string[] arr_strings) {
            //Finds the longest common prefix from an array of strings.
            if (arr_strings.Length == 0 || Array.IndexOf(arr_strings, "") != -1)
                return "";
            string result = arr_strings[0];
            int i = result.Length;
            foreach (string word in arr_strings) {
                int j = 0;
                foreach (char c in word) {
                    if (j >= i || result[j] != c)
                        break;
                    j += 1;
                }
                i = Math.Min(i, j);
            }
            return result.Substring(0, i);
        }
        static void ListAllPrimeInDescendingOrder() {
            //Create and display a list of all prime numbers in ascending order
            Console.WriteLine("Displaying a list of prime numbers in ascending order");
            uint z = 0; int nc;
            var p = new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var nxt = new uint[128];

            while (true) {
                nc = 0;
                foreach (var x in p) {
                    if (IsPrime(x))
                        Console.Write("{0,8}{1}", x, ++z % 5 == 0 ? "\n" : " ");

                    for (uint y = x * 10, l = x % 10 + y++; y < l; y++)
                        nxt[nc++] = y;
                }
                if (nc > 1) {
                    Array.Resize(ref p, nc); Array.Copy(nxt, p, nc);
                } else break;
            }

            Console.WriteLine("\n{0} descending primes found", z);
        }
        static void ListAllPrimeInAscendingOrder() {
            //Create and display a list of all prime numbers in ascending order
            Console.WriteLine("Displaying a list of prime numbers in ascending order");
            var Q = new Queue<uint>();
            var prime_nums = new List<uint>();

            for (uint i = 1; i <= 9; i++)
                Q.Enqueue(i);

            while (Q.Count > 0) {
                uint n = Q.Dequeue();
                if (IsPrime(n))
                    prime_nums.Add(n);
                for (uint i = n % 10 + 1; i <= 9; i++)
                    Q.Enqueue(n * 10 + i);
            }

            foreach (uint p in prime_nums) {
                Console.Write($"{p}, ");
            }

            Console.WriteLine();
        }
        public static bool IsPrime(uint n) {
            //Calculating whether a number passing through is prime or not.
            if (n <= 1) { return false; }

            int ctr = 0;
            for (int i = 1; i <= n; i++) {
                if (n % i == 0) { ctr++; }
                if (ctr > 2) {
                    return false;
                }
            }
            return true;
        }
        public static int ListAllPrimeValues(int n) {
            //Get next prime number of a given number. If the given number is a prime number, return the number.
            for (int i = 2; i < n; i++) {
                if (n % i == 0) { n++; i = 2; }
            }
            return n;
        }
    }
}
