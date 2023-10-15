
// PMC-291 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic-algo/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7



// DSAP-10 - Exercise-22
// Check whether two given non-negative integers have the same last digit
	public static bool test(int x, int y) {

		// Divide the given value by 10 will leave us the remainder. 
		// Then we compare that with Abs method.
		return Math.Abs(x % 10) == Math.Abs(y % 10);
	}

// DSAP-9 - Exercise-22
// Check whether a given string contain the character 'z' from 2 to 4 times
	public static bool strcontain(string str) {

		int ctr = 0;
		for (int i = 0; i < str.Length; i++) {

			if (str[i] == 'z')
			{
				ctr++;
			}
		}
		return ctr > 1 && ctr <= 4;
	}

// DSAP-8 - Random Exercise queried from CHAPT-GPT using Exercise-18 req from w3resource site.
// Check the largest number among three given integers.
	public static void LargestNumInArray() {
		// Input: An array of integers
		int[] numbers = { 10, 5, 20, 15, 30, 25 };

		// Initialize the maximum value as the first element of the array
		int max = numbers[0];

		// Iterate through the array to find the largest number
		for (int i = 1; i < numbers.Length; i++) {
			
			if (numbers[i] > max)
				max = numbers[i];
			
		}

		// Output the largest number
		Console.WriteLine("The largest number in the array is: " + max);
	}

// DSAP-7 - Random Exercise queried from CHAPT-GPT
// Check if a given string starts with 'C#' or not.
	public static string CheckTemperature() {

		string returnMessage;
		string prompt ="Please enter temperature: ";
		Console.WriteLine(prompt);
		
		if (double.TryParse(Console.ReadLine(), out double temp1))
		{
			Console.WriteLine(prompt);
			 
			if (double.TryParse(Console.ReadLine(), out double temp2))
			{
				if (temp1 < 0 || temp2 > 100)                    
					returnMessage = "Temperature is brutal.";
				else
					returnMessage = "The temperature is Good";
			}
			else
				returnMessage = "Invalid input for the second temperature.";
		}
		else
			returnMessage = "Invalid input for the first temperature.";

		return returnMessage;
	}


// DSAP-6 - Exercise-12
// Check if a given string starts with 'C#' or not.
	public static bool StringStartWith(string mainstr, string strcheck) {

		return (mainstr.Length < 3 && mainstr.Equals(strcheck)) || (mainstr.StartsWith(strcheck) && mainstr[4] == ' ');
	}
//=====================================================================================================
		
		
// DSAP-5 - Exercise-11
// Create a string taking the first 3 characters of a given string.
// Return the string with the 3 characters added at both the front and back. 
// If the given string length is less than 3, use whatever characters are there.
	public static string  GrabThreeStr(string str) {

		//The short way
		return (str.Length < 3) ? (str + str + str) : (str.Substring(0, 3) + str + str.Substring(0, 3));

		//The long way
		/*
		if (str.Length < 3) {

			return str + str + str;
		} else {

			string front = str.Substring(0, 3);
			return front + str + front;
		}  */          
	}
//=====================================================================================================


// DSAP-4 - Exercise-10
// Check if a given positive number is a multiple of 3 or 7
	public static bool MultipleOf(int n)
	{
		return n % 3 == 0 || n % 7 == 0;
	}
//=====================================================================================================


// DSAP-3 - Exercise-9
// Create a string with the last char added at the front and back of a given string of length 1 or more.
	public static string AddFrontAndBack(string str)
	{
		var s = str.Substring(str.Length-1);
		return s + str + s;
	}
//=====================================================================================================


// DSAP-2 - Exercise-5
// Create a string where 'if' is added to the front of a given string. 
//If the string already begins with 'if', return it unchanged.
	public static string CheckIfStr(string s)	{
		
		//Simplified from original result
		Return (s.Length > 2 && s.Substring(0, 2).Equals("if")) ? s : ("if " + s);
	}	
CheckIfStr("I was a billionaire")
//=====================================================================================================


// DSAP-1 - Exercise-1
//Check a given integer and return true if it is within 10 of 100 or 200.
	public static bool CheckInteger(int x) {
		
		if(Math.Abs(x - 100) <= 10 || Math.Abs(x - 200) <= 10)
			return true;
		return false;
		
	}
//Pass a value to the method
CheckInteger(103); //Or 100 or 90
//=====================================================================================================
