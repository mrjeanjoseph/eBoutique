
// PMC-315 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic-algo/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7

// DSAP-07 - Exercise - 53 Basic Algorithm
// Check two given integers, each in the range 10..99. Return true if a digit appears in both numbers, such as the 3 in 13 and 33
	public static bool CheckTwoGivenIntegers(int x, int y)	{
		
	   return x / 10 == y / 10 || x / 10 == y % 10 || x % 10 == y / 10 || x % 10 == y % 10;
	} //=====================================================================================================
	
// DSAP-08 - Exercise - 52 Basic Algorithm
// find the largest of two given integers. 
// If the two integers have the same remainder when divided by 7, then return the smallest integer. 
// If the two integers are the same, return 0.
    public static int FindLargerInteger(int x, int y) {
      if (x == y) return 0;
      if (x % 7 == y % 7) return (x < y) ? x: y;
      return (x > y) ? x: y;
	} //=====================================================================================================

// DSAP-09 - Exercise - 51 Basic Algorithm
// Check three given integers and return true if one of them is 20 or more less than one of the others.
	public static bool GreaterOrLessThanOthers(int x, int y, int z) {
		
	  return Math.Abs(x - y) >= 20 || Math.Abs(x - z) >= 20 ||
			   Math.Abs(y - z) >= 20;
	} //=====================================================================================================
	
// DSAP-10 - Exercise - 39 Basic Algorithm
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