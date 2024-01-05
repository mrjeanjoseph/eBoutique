using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicAlgorithms {
    class Program {
        static void Main(string[] args) {

            Console.ReadLine();
        }

        static string CheckWhetherStringsAppearAtIndex(string userInput) {
            if (userInput.Substring(1, 2).Equals("yt"))
                return userInput.Remove(1, 2);

            return userInput;
        }

        static bool CheckWhetherTwoIntegersAreWithinRange(int valueOne, int valueTwo) {
            if (valueOne >= 100 && valueOne <= 200)
                return true;

            return false;
        }

        static bool TemperatureCheckLessOrGreaterThan(int userInput1, int userInput2) {
            if (userInput1 < 0 && userInput2 > 100)
                return true;

            return false;
        }

        static string ThreeNewStringFrontAndBack(string strInput) {
            //Create new string taking the first 3 chars of a given string and return it with the 3 chars added at both the front and back.
            if (strInput.Length < 3)
                return "Not yet implemented";
            else {
                string front = strInput.Substring(0, 3);
                return front + strInput + front;
            }
        }

        static string ReturnAListOfNumArray(int arrQty) {
            int[] n = new int[arrQty]; /* n is an array of 10 integers */
            int i, j;
            string result="";

            /* initialize elements of array n */
            for (i = 0; i < arrQty; i++) {
                n[i] = i + 100;
            }

            /* output each array element's value */
            for (j = 0; j < n.Length; j++) {
                result += $"Element[{j}] = {n[j]}\n";
            }
            return result;
        }

        static bool MultipleOfThreeOrSeven(int myInt) {
            //check whether a given positive number is a multiple of 3 or a multiple of 7
            //return myInt % 3 == 0 || myInt % 7 == 0;

            if(myInt % 3 == 0 || myInt % 7 == 0) {
                return true;
            }
            return false;
        }

        static string NewStringLastCharAddToFrontAndBack(string myStr) {
            //create a new string with the last char added at the front and back of a given string of length 1 or more.

            string lastStr = myStr.Substring(myStr.Length - 1);
            return $"{lastStr}{myStr}{lastStr}";
        }

        static string CreateNewStringWithFourCopies(string myStr) {
            //create a new string which is 4 copies of the 2 front characters of a given string.If the given string length is less than 2 return the original string
            //return myStr.Length < 2 ? myStr : myStr.Substring(0, 2) + myStr.Substring(0, 2) + myStr.Substring(0, 2) + myStr.Substring(0, 2);
            string result = myStr;
            if(myStr.Length < 2) return result;
            //return myStr.Substring(0, 2) + myStr.Substring(0, 2) + myStr.Substring(0, 2) + myStr.Substring(0, 2);

            result = "";
            for (int i = 0; i < 4; i++) {
                result += myStr.Substring(0, 2);
            }
            return result;
        }

        public static string ExchangeFirstLastCharacters(string myStr) {
            // Exchange the first and last characters in a given string and return the new string.

            //return myStr.Length > 1 ? myStr.Substring(myStr.Length - 1) + myStr.Substring(1, myStr.Length - 2) + myStr.Substring(0,1) : myStr;
            if (myStr.Length > 1)
                return myStr.Substring(myStr.Length - 1) + myStr.Substring(1, myStr.Length - 2) + myStr.Substring(0, 1);
            else
                return myStr;
        }

        static string CheckSpecificString(string givenString, string checkedString) {
            //Check if part of a string exist at the beginning. Return the string if exist, add the missing strings if does not exists.
            if(givenString.Length > 2 && givenString.Substring(0,2).Equals(checkedString))
                return givenString;
            return $"{checkedString} {givenString}";
        }

        static bool CheckTwoValueIfTrue(int a, int b) {
            //Check two given integers, and return true if one of them is 30 or if their sum is 30.
            //return a == 30 || b == 30 || (a + b == 30);
            if(a == 30) return true;
            if(b == 30) return true;
            if (a + b == 30) return true;
            else return false;
        }

        static int ComputeSumAndTrippleSum(int x, int y) {
            //compute the sum of the two given integer values. If the two values are the same, then return triple their sum

            if (x == y) {
                return (x + y) * 3;
            } else {
                return x + y;
            }
        }

        static int ComputeAbsoluteDiffirence(int num) {
            //get the absolute difference between num and 51. If n is greater than 51 return quad the absolute difference.
            const int x = 51;
            const int q = 4;
            if (x > num) {
                return (x - num) * q;
            } else {
                return x - num;
            }
        }
    }
}
