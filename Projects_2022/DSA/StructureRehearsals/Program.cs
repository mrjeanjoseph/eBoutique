using System;
using static StructureRehearsals.ExerciseThreeStruct;

namespace StructureRehearsals {
    class Program {
        static void Main(string[] args) {

            ExerciseEight();
            Console.ReadLine();
        }

        public static void ExerciseEight() {
            int QtyOfBooks = 2;
            ExerciseEight_Books[] booksAdded = new ExerciseEight_Books[QtyOfBooks];
            int w, x, y = QtyOfBooks, z = 1;

            Console.WriteLine("Insert the information of two books :");
            Console.WriteLine("-------------------------------------");

            for(x = 0; x < y; x++) {
                Console.WriteLine("Information for book {0}:", z);

                Console.WriteLine("Enter book name:");
                booksAdded[x].bookTitle = Console.ReadLine();

                Console.WriteLine("Enter book author name:");
                booksAdded[x].bookAuthor = Console.ReadLine();
                z++;
                Console.Clear();
            }
            Console.WriteLine("Here's a list of books you requested:");
            for(w = 0; w < y; w++) {
                Console.WriteLine("\t{0}: {1}, by {2}", w + 1, booksAdded[w].bookTitle, booksAdded[w].bookAuthor);
            }
        }

        public static void ExerciseSeven() {
            Console.WriteLine("Structure declares using default and parameterized constructors :");
            Console.WriteLine("-----------------------------------------------------------------");

            ExerciseSevenNewStruct myStruct1 = new ExerciseSevenNewStruct();
            ExerciseSevenNewStruct myStruct2 = new ExerciseSevenNewStruct(65, 91);

            Console.WriteLine("myStruct1: ");
            Console.WriteLine("valueOne = {0}, valueTwo = {1}", myStruct1.valueOne, myStruct1.valueTwo);

            Console.WriteLine("myStruct2: ");
            Console.WriteLine("valueOne = {0}, valueTwo = {1}", myStruct2.valueOne, myStruct2.valueTwo);
        }

        public static void ExerciseSix() {
            Console.WriteLine("Declares a structure with a property, a method, and a private field :");
            Console.WriteLine("---------------------------------------------------------------------");

            //ExerciseSixNewStruct exeSixInstance = new ExerciseSixNewStruct();
            //exeSixInstance.AnotherNum = 45;
            ExerciseSixNewStruct exeSixInstance = new ExerciseSixNewStruct {
                AnotherNum = 145
            };
            exeSixInstance.ExerciseSixMethod();
        }

        public static void ExerciseFive() {
            Console.WriteLine("When a structure and a class instance is passed to a method :");
            Console.WriteLine("-------------------------------------------------------------");

            ExerciseFiveNewStruct efns = new ExerciseFiveNewStruct();
            ExerciseFiveNewClass efnc = new ExerciseFiveNewClass();
            efns.structID = 125;
            efnc.classID = 138;
            ExerciseFiveTrackStruct(efns);
            ExerciseFiveTrackClass(efnc);

            Console.WriteLine("efns.structID = {0}", efns.structID);
            Console.WriteLine("efnc.classID = {0}", efnc.classID);
        }

        public static void ExerciseFiveTrackStruct(ExerciseFiveNewStruct exStruct) {
            exStruct.structID = 25;
        }

        public static void ExerciseFiveTrackClass(ExerciseFiveNewClass exClass) {
            exClass.classID = 38;
        }

        public static void ExerciseFour() {
            Console.WriteLine("Creating a structure and assigning a value then call it :");
            Console.WriteLine("---------------------------------------------------------");
            ExerciseFourNewClass classCal1 = new ExerciseFourNewClass();
            classCal1.x = 150;
            classCal1.y = 150;

            ExerciseFourNewClass classCal2 = classCal1;
            classCal2.x = 2020;
            classCal2.y = 1985;
            Console.WriteLine("Assign in Class:       x:{0},   y:{1}", classCal2.x, classCal2.y);


            Console.WriteLine("---------------------------------------------------------");
            ExerciseFourNewStruct structCal1 = new ExerciseFourNewStruct();
            structCal1.x = 250;
            structCal1.y = 250;

            ExerciseFourNewStruct structCal2 = structCal1;
            structCal2.x = 150000;
            structCal2.y = 950000;
            Console.WriteLine("Assign in Struct:       x:{0},   y:{1}", structCal2.x, structCal2.y);
        }

        public static void ExerciseThree() {
            string employeeName = "";
            int birthDay = 0, birthMonth = 0, birthYear = 0, total = 1;
            var getCurrYear = DateTime.Now.ToString("yy");
            //DateTime getCurrYear = DateTime.Now;
            //getCurrYear.ToString("yy");
            Console.WriteLine(getCurrYear);

            Console.WriteLine("Declaring a nested structure and store data in an array: ");
            employee[] emp = new employee[total];

            for (int x = 0; x < total; x++) {
                Console.WriteLine("Name of the employee: ");
                employeeName = Console.ReadLine();
                emp[x].empName = employeeName;

                Console.WriteLine("Enter the birth date of the employee: ");
                birthDay = Convert.ToInt32(Console.ReadLine());
                emp[x].Date.Day = birthDay;

                Console.WriteLine("Enter the birth Month of the employee: ");
                birthMonth = Convert.ToInt32(Console.ReadLine());
                emp[x].Date.Month = birthMonth;

                Console.WriteLine("Enter the year of the employee: ");
                birthYear = Convert.ToInt32(Console.ReadLine());
                emp[x].Date.Year = birthYear;
            }
            Console.WriteLine($"Employee: {employeeName} \n \tDate of Birth: {birthMonth}/{birthDay}/{birthYear}");
        }

        public static void ExerciseTwo() {
            Console.WriteLine("Declaring a structure with the use of static fields:");
            Console.WriteLine("====================================================");

            int result = ExerciseTwoStruct.x + ExerciseTwoStruct.y;
            Console.WriteLine($"The sum of x and y using static struct is: {result}");
        }

        public static void ExerciseOne() {
            Console.WriteLine("Declaring a simple struct: ");
            Console.WriteLine("===========================");

            StructAndClassExercises exstruct = new StructAndClassExercises();
            exstruct.x = 71;
            exstruct.y = 29;
            int xytotal = exstruct.x + exstruct.y;
            Console.WriteLine($"The sum of x and y is {xytotal}");
        }
    }
}
