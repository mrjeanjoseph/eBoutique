// PMC-300 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic-algo/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7
 //==========================================================================================================

// DSAP-10 - Exercise-46 Basic Algorithm
// Write a C# Sharp program to check whether a given string begins with "F" or ends with "B".
// If the string starts with "F" return "Fizz" and return "Buzz" if it ends with "B" If the string starts with "F" and ends with "B" return "FizzBuzz".
// In other cases return the original string.
	public static string ReturnFizzBuzz(string str) {
		
		if (str.StartsWith("F") && str.EndsWith("B"))            
			return "FizzBuzz";            
		else if (str.StartsWith("F"))            
			return "Fizz";            
		else if (str.EndsWith("B"))            
			return "Buzz";            
		else            
			return str;            
	} //=====================================================================================================

// DSAP-9 - Exercise-45 Basic Algorithm
// Compute the sum of the two given integers. Return 18 if one of the given integer values is in the range of 10..20 inclusive.
	public static int ComputeSumOfTwo(int x, int y) {
		
		return (x >= 10 && x <= 20) || (y >= 10 && y <= 20) ? 18 : x + y;
	} //=====================================================================================================

// DSAP-8 - Exercise-36 Basic Algorithm
// Create a string from a given string where a specified char is removed except for the beginning and ending positions.
	public static string StringX(string str1, string c) {
		
		  for (int i = str1.Length - 2; i > 0; i--) {
			  
			if (str1[i] == c[0])
				str1 = str1.Remove(i, 1);
			
		}

		return str1;
	} //=====================================================================================================

// DSAP-7 - Exercise-33 Basic Algorithm
// Check if one of the first 4 elements in an array of integers is equal to a given element.
	public static bool CheckIfSequenceNums(int[] nums)	{
		
		for (var i = 0; i < nums.Length-2; i++) {
			
			if (nums[i] == 1 && nums[i + 1] == 2 && nums[i + 2] == 3)
				return true;
		}
	   return false;
	} //=====================================================================================================

// DSAP-6 - Exercise-29 Basic Algorithm
// Create a string that starts over and increment on a given string.
	public static string StartOverThenIncr(string str) {

		var result = string.Empty;

		for (var i = 0; i < str.Length; i++)
			result += str.Substring(0, i + 1) + ',';
		
		return result;
	} //=====================================================================================================

// DSAP-5 - Exercise-25 Basic Algorithm
// Create a string that is n (non-negative integer) copies of the first 3 chars. If it is shorter than 3, return n copies of the string.
	public static string MakeCopies(string s, int n) {

		var result = string.Empty;
		var frontOfString = 3;

		if (frontOfString > s.Length)
			frontOfString = s.Length;

		var front = s.Substring(0, frontOfString);

		for (var i = 0; i < n; i++)
		{
			result += front;
		}
		return result;

	} //=====================================================================================================


// DSAP-4 - Exercise-24 Basic Algorithm
// Convert the last 3 chars of a given string to uppercase. If the length is less than 3, then uppercase all the chars.
	public static string MakeUpperCase(string str) {
		
		return str.Length < 3 ? str.ToUpper() : str.Remove(str.Length - 3) + str.Substring(str.Length - 3).ToUpper();
	} //=====================================================================================================


// DSAP-3 - Exercise-99
// Create and display all prime numbers in strict asc or desc decimal digit order.
	public static bool IsPrime(uint n) {

		if (n <= 1) { return false; }

		int ctr = 0;
		for (int i = 1; i <= n; i++) {

			if (n % i == 0) { ctr++; }
			if (ctr > 2) return false;                  
		}

		return true;
	} //=====================================================================================================

	static void DisplayPrimeInAsc() {

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

			Console.Write(p);
			Console.Write(", ");
		}

		Console.WriteLine();
	} //=====================================================================================================
	
	static void DisplayPrimeInDesc() {

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

			if (nc > 1) 
				Array.Resize(ref p, nc); Array.Copy(nxt, p, nc);
			
			else break;
		}

		Console.WriteLine("\n{0} descending primes found", z);
	} //=====================================================================================================


// DSAP-2 - Exercise-102
// Create a identity matrix
	public static void IndentityMatrix() {

		int n;
		Console.WriteLine("Input a number:");
		n = Convert.ToInt32(Console.ReadLine());
		var M =
		Enumerable.Range(0, n).Select(i => Enumerable.Repeat(0, n).Select((z, j) => j == i ? 1 : 0).ToList()).ToList();
		foreach (var r in M) {

			foreach (var e in r) {

				Console.Write(" " + e);
			}

			Console.WriteLine();
		}
	} //=====================================================================================================


// DSAP-1 - Exercise-103
// Sort characters in a given string (uppercase/lowercase letters and numbers). Return the newly sorted string.
	public static string SortChar(string text)
	{
		bool flag = string.IsNullOrWhiteSpace(text);
		if (flag)
			return "Blank string!";
		var text_nums = text.Where(char.IsDigit).OrderBy(el => el).ToList();
		var text_chars = text.Where(char.IsLetter)
			.Select(el => new { l_char = char.ToLower(el), _char = el })
			.OrderBy(el => el.l_char)
			.ThenByDescending(el => el._char)
			.ToList();

		return new string(text_chars.Select(el => el._char).Concat(text_nums).ToArray());
	}
//Pass a value to the method
SortChar("Eating Berries"); // Returns: aBeeEgiinrrst
//=====================================================================================================
