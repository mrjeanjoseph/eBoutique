
// PMC-312 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic-algo/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7


// DSAP-00 Extra: Exercise - 62 Basic Algorithm
// Create a string using three copies of the last two characters of a given string of length at least two.
	public static string CreateNewString(string s1) {

		string last2 = s1.Substring(s1.Length - 1);
		return last2 + last2 + last2;

	} //=====================================================================================================
		
// DSAP-01: Exercise - 61 Basic Algorithm
// Insert a given string into the middle of another given string of length 4.
	public static string InsertInTheMiddle(string s1, string word) {

		return s1.Substring(0, 2) + word + s1.Substring(2);

	} //=====================================================================================================
	
// DSAP-02: Exercise - 58 Basic Algorithm
// compare 3 int and return true if the difference between small and medium and the between medium and large is the same.
	public static bool CompareThreeInts(int x, int y, int z) {
		
		if (x > y && x > z && y > z) return x - y == y - z;
		if (x > y && x > z && z > y) return x - z == z - y;
		if (y > x && y > z && x > z) return y - x == x - z;
		if (y > x && y > z && z > x) return y - z == z - x;
		if (z > x && z > y && x > y) return z - x == x - y;
		return z - y == y - x;
	} //=====================================================================================================
		 
// DSAP-03: Exercise - 58 Basic Algorithm
// Check two integers and return the value nearest to 13 without crossing over. Return 0 if both numbers exceed.
	public static int CheckTwoGivenIntegers(int x, int y) {
		
		if (x > 13 && y > 13) return 0;
		if (x <= 13 && y > 13) return x;
		if (y <= 13 && x > 13) return y;
		return x > y ? x : y;
	}//=====================================================================================================		
		
// DSAP-04: Exercise - 57 Basic Algorithm
// Compute the sum of the three given integers. All values in the range 10..20 inclusive count as 0, except 13 and 17.
	public static int SumThreeGivenIntegers3(int x, int y, int z) {
		
		return fix_num(x) + fix_num(y) + fix_num(z);
	}
	
	private static int fix_num(int n) {
		
		return (n > 9 && n <13) || (n > 13 && n < 17) || (n > 17 && n < 21) ? 0 : n;
	}//=====================================================================================================
		
// DSAP-05: Exercise - 56 Basic Algorithm
// Compute the sum of the three integers. If one of the values is 13 then do not count it and its right towards the sum.
	public static int SumThreeGivenIntegers2(int x, int y, int z)	{
	   if (x == 13) return 0;
	   if (y == 13) return x;
	   if (z == 13) return x + y;
	   return x + y + z;
	} //=====================================================================================================
		
// DSAP-06: Exercise - 55 Basic Algorithm
// Compute the sum of three given integers. Return the third value if the two values are the same.
    public static int SumThreeGivenIntegers(int x, int y, int z)	{
		
		if (x == y && y == z) return 0;
		if (x == y) return z;
		if (x == z) return y;
		if (y == z) return x;
		return x + y + z;
	} //=====================================================================================================

// DSAP-07: Exercise - 53 Basic Algorithm
// Check two given integers, each in the range 10..99. Return true if a digit appears in both numbers, such as the 3 in 13 and 33
	public static bool CheckTwoGivenIntegers(int x, int y)	{
		
	   return x / 10 == y / 10 || x / 10 == y % 10 || x % 10 == y / 10 || x % 10 == y % 10;
	} //=====================================================================================================
	
// DSAP-08: Exercise - 52 Basic Algorithm
// find the largest of two given integers. 
// If the two integers have the same remainder when divided by 7, then return the smallest integer. 
// If the two integers are the same, return 0.
    public static int FindLargerInteger(int x, int y) {
      if (x == y) return 0;
      if (x % 7 == y % 7) return (x < y) ? x: y;
      return (x > y) ? x: y;
	} //=====================================================================================================

// DSAP-09: Exercise - 51 Basic Algorithm
// Check three given integers and return true if one of them is 20 or more less than one of the others.
	public static bool GreaterOrLessThanOthers(int x, int y, int z) {
		
	  return Math.Abs(x - y) >= 20 || Math.Abs(x - z) >= 20 ||
			   Math.Abs(y - z) >= 20;
	} //=====================================================================================================
	
// DSAP-10: Exercise - 39 Basic Algorithm
// Check if a triple is present in an array of integers or not.
// If a value appears three times in a row in an array it is called a triple.
	public static bool IsTriplePresent(int[] nums) {
		
		int arra_len = nums.Length - 1, n = 0;
		for (int i = 0; i < arra_len; i++)
		{
			 n = nums[i];
			if (n == nums[i + 1] && n == nums[i + 2]) return true;
		}
		return false;
	} //=====================================================================================================