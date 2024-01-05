using System;
using System.Globalization;
using System.Threading;

namespace DateTimeReheasals {
    class Program {
        static void Main(string[] args) {

            GetDayOfTheWeek();
            Console.ReadLine();
        }

        public static void DisplayUTCNowDates() {
            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;
            String[] cultureNames = { "en-US", "en-GB", "fr-FR",
                                "de-DE", "ru-RU", "ht-HT" };

            foreach (var cultureName in cultureNames) {
                var culture = new CultureInfo(cultureName);
                Console.WriteLine("{0}:", culture.NativeName);
                Console.WriteLine("   Local date and time: {0}, {1:G}",
                                  localDate.ToString(culture), localDate.Kind);
                Console.WriteLine("   UTC date and time: {0}, {1:G}\n",
                                  utcDate.ToString(culture), utcDate.Kind);
            }
        }

        public static void DisplayDateTimeValue() {
            DateTime localDate = DateTime.Now;

            DateTime dt1 = new DateTime(2016, 6, 8, 11, 49, 0);
            Console.WriteLine("Complete date: " + dt1.ToString());

            // Get date-only portion of date, without its time.
            DateTime dateOnly = dt1.Date;

            Console.WriteLine("Short Date: " + dateOnly.ToString("d"));

            Console.WriteLine("Display date using 24-hour clock format:");

            Console.WriteLine(dateOnly.ToString("g"));
            Console.WriteLine(dateOnly.ToString("MM/dd/yyyy HH:mm"));
        }

        public static void Display15DatesPerMonth() {
            var dates = new DateTime(2022, 9, 4);
            for (int z = 0; z <= 15; z++)
                Console.WriteLine(dates.AddMonths(z).ToString("d"));
        }

        public static void PastAndFutureDates() {
            DateTime baseDate = new DateTime(2022, 9, 4);
            Console.WriteLine($"Base Date:\t\t {baseDate:d}");
            Console.WriteLine("============================");

            for (int z = 0; z >= -15; z--) {
                //Console.WriteLine("{0,2} year(s) ago:\t\t {1:d}", Math.Abs(z), baseDate.AddYears(z));
                string result1 = Math.Abs(z).ToString("d");
                string result2 = baseDate.AddYears(z).ToString("d");
                Console.WriteLine($"{result1} year (s) ago:\t\t {result2}");
            }

        }

        public static void ComparingTwoDates() {
            DateTime dateOne = new DateTime(2022, 9, 4);
            DateTime dateTwo = new DateTime(2022, 10, 19);

            int result = DateTime.Compare(dateOne, dateTwo);
            string relationship;

            if (result > 0)
                relationship = "is earlier than";
            else if (result == 0)
                relationship = "is the same date/time as";
            else
                relationship = "is later than";

            Console.WriteLine($"{dateOne.ToString("M")} {relationship} {dateTwo.ToString("M")}");
        }

        public static void ComparePastDateAndFutureDates() {
            DateTime thisDate = DateTime.Today;
            DateTime thisDateNextYear, thisDateLastYear;

            //  add/substract 1 year
            thisDateNextYear = thisDate.AddYears(1);
            thisDateLastYear = thisDate.AddYears(-1);

            DateCompare comparison;
            // Compare today to last year
            comparison = (DateCompare)thisDate.CompareTo(thisDateLastYear);
            Console.WriteLine("{0}: {1:d} is {2} than {3:d}",
                              (int)comparison, thisDate, comparison.ToString().ToLower(),
                              thisDateLastYear);

            // Compare today to next year
            comparison = (DateCompare)thisDate.CompareTo(thisDateNextYear);
            Console.WriteLine("{0}: {1:d} is {2} than {3:d}",
                              (int)comparison, thisDate, comparison.ToString().ToLower(),
                              thisDateNextYear);
        }

        public static void CompareCurrentAndGivenDates() {
            DateTime futureDate = new DateTime(DateTime.Today.Year, 9, 5);
            int compareValue;

            try {
                compareValue = futureDate.CompareTo(DateTime.Today);
            } catch (ArgumentException) {
                Console.WriteLine("Value is not a DateTime");
                return;
            }

            if (compareValue < 0)
                Console.WriteLine("{0:d} is in the past.", futureDate);
            else if (compareValue == 0)
                Console.WriteLine("{0:d} is today!", futureDate);
            else // compareValue > 0
                Console.WriteLine("{0:d} has not come yet.", futureDate);

        }

        public static void CompareDateTimeObjects() {

            DateTime one = DateTime.UtcNow;
            DateTime two = DateTime.Now;
            DateTime three = one;

            bool result = one.Equals(two);
            Console.WriteLine("The result of comparing DateTime object one and two is: {0}.", result);

            result = one.Equals(three);
            Console.WriteLine("The result of comparing DateTime object one and three is: {0}.", result);
        }

        public static void DateTimeStringRepresentation() {
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

            string[] dateStrings = {"8/1/2022 6:32 PM", "01/01/2022 6:32:05 PM",
                              "2/19/2020 6:32:00", "12/08/2019 06:32",
                              "08/06/2019 06:32:00 PM", "05/11/2018 06:32:00"};

            DateTime dateValue;

            foreach (string dateString in dateStrings) {
                if (DateTime.TryParseExact(dateString, formats,
                                           new CultureInfo("zh-CN"),
                                           DateTimeStyles.None,
                                           out dateValue))
                    Console.WriteLine("Converted '{0}' to {1}.", dateString, dateValue);
                else
                    Console.WriteLine("Unable to convert '{0}' to a date.", dateString);
            }
        }

        public static void DateStringRepresentation() {
            DateTime july28 = new DateTime(2009, 7, 28, 5, 23, 15, 16);

            //string[] july28Formats = july28.GetDateTimeFormats();
            string[] july28Formats = july28.GetDateTimeFormats('d');

            // Print out july28 in all DateTime formats using the default culture.
            foreach (string format in july28Formats) {
                Console.WriteLine(format);
            }
        }

        public static void DatesFallWithinRange() {
            DateTime date1 = new DateTime(2016, 3, 14, 2, 30, 00);
            Console.WriteLine("Invalid Time: {0}",
                              TimeZoneInfo.Local.IsInvalidTime(date1));
            long ft = date1.ToFileTime();
            DateTime date2 = DateTime.FromFileTime(ft);
            Console.WriteLine("{0} -> {1}", date1, date2);
        }

        public static void DetermineObjectTypes() {

            object[] values = {
                (int) 24,
                (long) 10653,
                (byte) 24,
                (sbyte) -5,
                26.3,
                "string"
            };

            foreach (var value in values) {
                Type t = value.GetType();
                if (t.Equals(typeof(byte)))
                    Console.WriteLine("{0} is an unsigned byte.", value);
                else if (t.Equals(typeof(sbyte)))
                    Console.WriteLine("{0} is a signed byte.", value);
                else if (t.Equals(typeof(int)))
                    Console.WriteLine("{0} is a 32-bit integer.", value);
                else if (t.Equals(typeof(long)))
                    Console.WriteLine("{0} is a 32-bit integer.", value);
                else if (t.Equals(typeof(double)))
                    Console.WriteLine("{0} is a double-precision floating point.", value);
                else
                    Console.WriteLine("'{0}' is another data type.", value);
            }
        }

        public static void FindLeapYear() {
            int firstYear = 1995;
            int secondYear = 2022;

            for (int year = firstYear; year <= secondYear; year++) {
                if (DateTime.IsLeapYear(year)) {
                    Console.WriteLine("{0} is a leap year.", year);
                    DateTime leapDay = new DateTime(year, 2, 29);
                    DateTime nextYear = leapDay.AddYears(1);
                    Console.WriteLine("   One year from {0} is {1}.",
                                      leapDay.ToString("d"),
                                      nextYear.ToString("d"));
                }
            }
        }

        public static void CalculateLeapYear() {
            Console.WriteLine("Enter Year : ");

            int Year = int.Parse(Console.ReadLine());

            if (((Year % 4 == 0) && (Year % 100 != 0)) || (Year % 400 == 0))
                Console.WriteLine("{0} is a Leap Year.", Year);
            else
                Console.WriteLine("{0} is not a Leap Year.", Year);

        }

        public static void CalculateLeapYear2() {
            int i = 0;
            int[] arr = new int[5];

            Console.WriteLine("Enter years : ");
            for (i = 0; i < arr.Length; i++) {
                Console.Write("Year[" + (i + 1) + "]: ");
                arr[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("List of leap years : ");
            for (i = 0; i < arr.Length; i++) {
                if ((arr[i] % 4 == 0) && (arr[i] % 100 != 0))
                    Console.Write(arr[i] + " ");
                else if ((arr[i] % 4 == 0) && (arr[i] % 100 == 0) && (arr[i] % 400 == 0))
                    Console.Write(arr[i] + " ");
            }
        }

        public static void DiffBetweenTwoDates() {

            //establish DateTimes
            DateTime start = new DateTime(2022, 9, 5);
            DateTime end = new DateTime(2022, 10, 19);

            //create TimeSpan object
            TimeSpan difference = end - start;

            //Extract days, write to Console.
            Console.WriteLine("Difference in days: " + difference.Days);
        }

        public static void CurrentDateTimeObjectToLocalTime() {
            DateTime localDateTime, univDateTime;

            Console.WriteLine("Enter a date and time.");
            string strDateTime = Console.ReadLine();

            try {
                localDateTime = DateTime.Parse(strDateTime);
                univDateTime = localDateTime.ToUniversalTime();

                Console.WriteLine("{0} local time is {1} universal time.",
                                       localDateTime,
                                        univDateTime);
            } catch (FormatException) {
                Console.WriteLine("Invalid format.");
                return;
            }

            Console.WriteLine("Enter a date and time in universal time.");
            strDateTime = Console.ReadLine();

            try {
                univDateTime = DateTime.Parse(strDateTime);
                localDateTime = univDateTime.ToLocalTime();

                Console.WriteLine("{0} universal time is {1} local time.",
                                         univDateTime,
                                         localDateTime);
            } catch (FormatException) {
                Console.WriteLine("Invalid format.");
                return;
            }
        }

        public static void CurrentDateTimeObjectToLongDate() {
            string msg1 = "The date and time patterns are defined in the DateTimeFormatInfo \n" +
                  "object associated with the current thread culture.\n";

            // Initialize a DateTime object.
            Console.WriteLine("Initialize the DateTime object to May 16, 2016 3:02:15 AM.\n");
            DateTime myDateTime = new System.DateTime(2016, 5, 16, 3, 2, 15);

            // Identify the source of the date and time patterns.
            Console.WriteLine(msg1);

            // Display the name of the current culture.
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            Console.WriteLine("Current culture: \"{0}\"\n", ci.Name);

            // Display the long date pattern and string.
            Console.WriteLine("Long date pattern: \"{0}\"", ci.DateTimeFormat.LongDatePattern);
            Console.WriteLine("Long date string:  \"{0}\"\n", myDateTime.ToLongDateString());

            // Display the long time pattern and string.
            Console.WriteLine("Long time pattern: \"{0}\"", ci.DateTimeFormat.LongTimePattern);
            Console.WriteLine("Long time string:  \"{0}\"\n", myDateTime.ToLongTimeString());

            // Display the short date pattern and string.
            Console.WriteLine("Short date pattern: \"{0}\"", ci.DateTimeFormat.ShortDatePattern);
            Console.WriteLine("Short date string:  \"{0}\"\n", myDateTime.ToShortDateString());

            // Display the short time pattern and string.
            Console.WriteLine("Short time pattern: \"{0}\"", ci.DateTimeFormat.ShortTimePattern);
            Console.WriteLine("Short time string:  \"{0}\"\n", myDateTime.ToShortTimeString());
        }

        public static void CurrentDateTimeObjectToLongTime() {
            // Create an array of culture names.
            string[] names = { "en-CA", "en-HA", "fr-CH", "de-AT" };
            // Initialize a DateTime object.
            DateTime dateValue = new DateTime(2016, 8, 17, 10, 30, 15);

            // Iterate the array of culture names.
            foreach (var name in names) {
                // Change the culture of the current thread.
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(name);
                // Display the name of the current culture and the date.
                Console.WriteLine("Current culture: {0}", CultureInfo.CurrentCulture.Name);
                Console.WriteLine("Date: {0:G}", dateValue);

                // Display the long time pattern and the long time.
                Console.WriteLine("Long time pattern: '{0}'", DateTimeFormatInfo.CurrentInfo.LongTimePattern);
                                  
                Console.WriteLine("Long time with format string:     {0:T}", dateValue);
                Console.WriteLine("Long time with ToLongTimeString:  {0}\n", dateValue.ToLongTimeString());
                                  
            }
        }

        public static void CultureSpecificFormat() {
            string dateString;
            CultureInfo culture;
            DateTimeStyles styles;
            DateTime dateResult;

            // Parse a date and time with no styles.
            dateString = "05/06/2016 10:00 AM";
            culture = CultureInfo.CreateSpecificCulture("de-DE");
            styles = DateTimeStyles.None;
            if (DateTime.TryParse(dateString, culture, styles, out dateResult))
                Console.WriteLine("{0} converted to {1} {2}.", dateString, dateResult, dateResult.Kind);
            else
                Console.WriteLine("Unable to convert {0} to a date and time.", dateString);
                                  

            // Parse the same date and time with the AssumeLocal style.
            styles = DateTimeStyles.AssumeLocal;
            if (DateTime.TryParse(dateString, culture, styles, out dateResult))
                Console.WriteLine("{0} converted to {1} {2}.", dateString, dateResult, dateResult.Kind);
            else
                Console.WriteLine("Unable to convert {0} to a date and time.", dateString);

            // Parse a date and time that is assumed to be local.
            // This time is five hours behind UTC. The local system's time zone is 
            // eight hours behind UTC.
            dateString = "2016/05/06T10:00:00-5:00";
            styles = DateTimeStyles.AssumeLocal;
            if (DateTime.TryParse(dateString, culture, styles, out dateResult))
                Console.WriteLine("{0} converted to {1} {2}.", dateString, dateResult, dateResult.Kind);
            else
                Console.WriteLine("Unable to convert {0} to a date and time.", dateString);

            // Attempt to convert a string in improper ISO 8601 format.
            dateString = "05/06/2016T10:00:00-5:00";
            if (DateTime.TryParse(dateString, culture, styles, out dateResult))
                Console.WriteLine("{0} converted to {1} {2}.", dateString, dateResult, dateResult.Kind);
            else
                Console.WriteLine("Unable to convert {0} to a date and time.", dateString);

            // Assume a date and time string formatted for the fr-BE culture is the local 
            // time and convert it to UTC.
            dateString = "2015-05-06 10:00";
            culture = CultureInfo.CreateSpecificCulture("fr-BE");
            styles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal;
            if (DateTime.TryParse(dateString, culture, styles, out dateResult))
                Console.WriteLine("{0} converted to {1} {2}.", dateString, dateResult, dateResult.Kind);
            else
                Console.WriteLine("Unable to convert {0} to a date and time.", dateString);
        }

        public static void YesterdayAndTomorrow() {
            Console.Write("\n\n Compute what day will be Tomorrow :\n");
            Console.Write("----------------------------------------\n");
            Console.WriteLine(" Today is : {0}", DateTime.Today.ToString("MM/dd/yyyy"));
            DateTime dt = ModifyDates(1);
            Console.WriteLine(" Tomorrow will be : {0} \n", dt.ToString("MM/dd/yyyy"));

            dt = ModifyDates(-1);
            Console.WriteLine(" Yesterday was : {0} \n", dt.ToString("MM/dd/yyyy"));
        }
        static DateTime ModifyDates(int x) {
            return DateTime.Today.AddDays(x);
        }
    
        public static void GetDaysForAGivenMonth() {
            int mn, yr;

            Console.Write("\n\n Find  the number of days in a given month for a year :\n");
            Console.Write("-----------------------------------------------------------\n");
            Console.Write("Enter a numeric month number: ");
            mn = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the year: ");
            yr = Convert.ToInt32(Console.ReadLine());
            DateTimeFormatInfo dinfo = new DateTimeFormatInfo();
            string mnum = dinfo.GetMonthName(mn);
            int nodays = DateTime.DaysInMonth(yr, mn);
            Console.WriteLine("The number of days in the month {0} is : {1} \n", mnum, nodays);
        }

        public static void PrintNamesOfAllMonth() {
            Console.Write("\n\n Display the name of the first three letters of month of a year :\n");
            Console.Write("---------------------------------------------------------------------\n");
            DateTime now = DateTime.Now;
            Console.WriteLine(" The date of Today : {0}", now.ToString("MM/dd/yyyy"));
            Console.WriteLine(" The twelve months are :");
            for (int i = 0; i < 12; i++) {
                Console.WriteLine(" {0}", now.ToString("MMM"));
                Console.WriteLine(" {0}", now.ToString("MMMM"));
                now = now.AddMonths(1);
            }
            Console.WriteLine();
        }

        public static void GetDayOfTheWeek() {
            int yr, mn, dt;

            Console.Write("\n\n Find the day for a given date :\n");
            Console.Write("------------------------------------\n");

            Console.Write("Input the Day : ");
            dt = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input the Month : ");
            mn = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input the Year : ");
            yr = Convert.ToInt32(Console.ReadLine());
            DateTime d = new DateTime(yr, mn, dt);
            Console.WriteLine("The formatted Date is : {0}", d.ToString("MM/dd/yyyy"));
            DateTime pp;
            pp = DayOfWeek(d);
            Console.WriteLine("The day for the date is : {0}\n ", pp.DayOfWeek);
        }
        static DateTime DayOfWeek(DateTime dt) {
            DateTime ss = new DateTime(dt.Year, dt.Month, dt.Day);
            return ss;
        }
    }
}
