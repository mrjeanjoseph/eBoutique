
// PMC-346 - Practice/Recreate 10 DSA projects
// These projects are located at: https://www.w3resource.com/csharp-exercises/basic-algo/index.php
// Run these on SoloLearn at: https://www.sololearn.com/compiler-playground/cK3vWCFTSxo7


// DSAP-01 Extra: Exercise - 87 Basic Algorithm
// Create a new string from a given string. Return the string without the first or first two 'a' characters otherwise return the original string.
    public static string CreateNewStringWithoutFirstTwoChars(string s1){
		
            // If the length of 's1' is 1 and the only character is "a", remove that character
            if (s1.Length == 1 && s1.Substring(0, 1) == "a")				
                s1 = s1.Remove(0, 1);            

            // If the length of 's1' is greater than 1, perform the following checks:
            if (s1.Length > 1) {
                // If the second character is "a", remove it
                if (s1.Substring(1, 1) == "a")                
                    s1 = s1.Remove(1, 1);                

                // If the first character is "a", remove it
                if (s1.Substring(0, 1) == "a")                
                    s1 = s1.Remove(0, 1);
                
            }

            return s1; // Return the modified or unchanged 's1'
		
	} //=====================================================================================================
	

// DSAP-02 Extra: Exercise - 85 Basic Algorithm
// Create a new string from a given string without the first two characters. Keep the first character if it is "p" and keep the second character if it is "y".
    public static string CreateNewStringWithoutFirstTwoChars(string s1){
		
		if (s1.Length >= 2){ // If the length of 's1' is greater than or equal to 2
		
			// If the character at index 1 of 's1' is not equal to "y", remove the character at index 1
			if (String.Compare(s1.Substring(1, 1), "y") != 0)                
				s1 = s1.Remove(1, 1);
			

			// If the character at index 0 of 's1' is not equal to "p", remove the character at index 0
			if (String.Compare(s1.Substring(0, 1), "p") != 0)                
				s1 = s1.Remove(0, 1);
			
		}
		else if (s1.Length > 0 && String.Compare(s1.Substring(0, 1), "p") != 0)            
			// If 's1' has a length greater than 0 and the character at index 0 is not "p", remove the character at index 0
			s1 = s1.Remove(0, 1);
		

		return s1; // Return the modified or unchanged 's1'
		
	} //=====================================================================================================
	

// DSAP-03 Extra: Exercise - 84 Basic Algorithm
// Create a new string from a string. In the case that the two characters at the beginning and end of the given string are the same, return the given string without the first two characters, otherwise return the original string.
    public static string CombineTwoBeginAndEndStrings(string s1){
		
		if (s1.Length > 1 && s1.Substring(0, 2) == s1.Substring(s1.Length - 2)) {
			
			// If the length of 's1' is greater than 1 and the first two characters are the same as the last two characters
			return s1.Substring(2); // Return 's1' starting from the third character
		}
		else {
			
			return s1; // Otherwise, return the input 's1' unchanged
		}
		
		// We can return a one line result as well.
		//  return (s1.Length > 1 && s1.Substring(0, 2) == s1.Substring(s1.Length - 2)) ? s1.Substring(2) : s1;

		
	} //=====================================================================================================
	

// DSAP-04 Extra: Exercise - 82 Basic Algorithm
// Combine two given strings. If the given strings have different lengths remove the characters from the longer string.
    public static string CombineTwoGivenStrings(string s1, string s2){
		
		if (s1.Length < s2.Length){ // If the length of 's1' is less than the length of 's2'
		
			// Concatenate 's1' with the end portion of 's2' to match the length of 's2'
			return s1 + s2.Substring(s2.Length - s1.Length);
		}
		else if (s1.Length > s2.Length){ // If the length of 's1' is greater than the length of 's2'
		
			// Concatenate the end portion of 's1' with 's2' to match the length of 's1'
			return s1.Substring(s1.Length - s2.Length) + s2;
		}
		else {// If the lengths of 's1' and 's2' are equal
		
			// Concatenate 's1' and 's2'
			return s1 + s2;
		}
		
	} //=====================================================================================================
	

// DSAP-05 Extra: Exercise - 79 Basic Algorithm
// Create a new string from a given string after swapping the last two characters.
    public static string FirstAndLastCharFromTwoStrings(string s1, string s2){
			
		// Check if the length of 's1' is greater than 1
		if (s1.Length > 1) {
			
			// If 's1' has a length greater than 1, rearrange the characters and return the modified string
			return s1.Substring(0, s1.Length - 2) + s1[s1.Length - 1] + s1[s1.Length - 2]; //Don't quite understand the brackets syntax
		} else {
			
			// If 's1' has a length of 1 or less, return 's1' as it is
			return s1;
		}
		// Or we can use a one liner.
        // return (s1.Length > 1) ? (s1.Substring(0, s1.Length - 2) + s1[s1.Length - 1] + s1[s1.Length - 2]) : s1;
		
		
	} //=====================================================================================================
	

// DSAP-06 Extra: Exercise - 78 Basic Algorithm
// Combine two given strings (lowercase). If there are any double characters in the string, omit one character.
    public static string FirstAndLastCharFromTwoStrings(string s1, string s2){
			
            // Check if 's1' is an empty string
            if (s1.Length < 1) {
				
                return s2; // If 's1' is empty, return 's2'
            }

            // Check if 's2' is an empty string
            if (s2.Length < 1) {
				
                return s1; // If 's2' is empty, return 's1'
            }

            // Check if the last character of 's1' does not match the first character of 's2'
            // If true, concatenate 's1' and 's2'; otherwise, concatenate 's1' and 's2' excluding the first character of 's2'
            return !s1.EndsWith(s2.Substring(0, 1)) ? s1 + s2 : s1 + s2.Substring(1);
		
	} //=====================================================================================================
	


// DSAP-07 Extra: Exercise - 77 Basic Algorithm
// Create a new string by taking the first character from a string and the last character from another string. If a string's length is 0, use '#' as its missing character.
    public static string FirstAndLastCharFromTwoStrings(string s1, string s2){
			
		string lastChars = String.Empty; // Initialize an empty string 'lastChars'

		if (s1.Length > 0){ // Check if the length of 's1' is greater than 0
		
			lastChars += s1.Substring(0, 1); // If so, append the first character of 's1' to 'lastChars'
		}else{		
		
			lastChars += "#"; // If 's1' is empty, append "#" to 'lastChars'
		}

		if (s2.Length > 0){ // Check if the length of 's2' is greater than 0
		
			lastChars += s2.Substring(s2.Length - 1); // If so, append the last character of 's2' to 'lastChars'
		}else{		
		
			lastChars += "#"; // If 's2' is empty, append "#" to 'lastChars'
		}

		return lastChars; // Return the concatenated string 'lastChars'
		
	} //=====================================================================================================
	

// DSAP-08 Extra: Exercise - 76 Basic Algorithm
// Create a new string of length 2, using the first two characters of a given string. If the given string length is less than 2 use '#' as missing characters.
    public static string FirstTwoCharFromTwoStrings(string s1, string s2){
			
		if (s1.Length >= 2){ // Check if the length of 's1' is greater than or equal to 2
		
			s1 = s1.Substring(0, 2); // If so, take the substring of 's1' starting from index 0 with a length of 2
		}
		else if (s1.Length > 0){ // Check if 's1' length is greater than 0 but less than 2
		
			s1 = s1.Substring(0, 1) + "#"; // If so, take the first character of 's1' and concatenate "#" to it
		}
		else{ // If 's1' is an empty string
		
			s1 = "##"; // Assign 's1' as "##"
		}

		return s1; // Return the modified or original 's1'
		
	} //=====================================================================================================
	

// DSAP-09 Extra: Exercise - 73 Basic Algorithm
// Create a new string using the first and last n characters from a given string of length at least n.
    public static string FirstAndLastCharPerNs(string s1){
			
        return s1.Substring(0, n) + s1.Substring(s1.Length - n);
		
	} //=====================================================================================================
	
	
// DSAP-10 Extra: Exercise - 70 Basic Algorithm
// Create a new string without the first and last characters of a given string of any length.
    public static string WithoutFirstAndLastChar(string s1){
			
        return s1.Length < 2 ? String.Empty : s1.Substring(1, s1.Length - 2);
		
	} //=====================================================================================================