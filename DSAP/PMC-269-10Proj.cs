// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7


//Check a given integer and return true if it is within 10 of 100 or 200.
public static bool CheckInteger(int x)
{
	  if(Math.Abs(x - 100) <= 10 || Math.Abs(x - 200) <= 10)
		return true;
	return false;
}

//Pass a value to the method
CheckInteger(103); //Or 100 or 90