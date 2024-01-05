using System;
using System.IO;

namespace FileHandling {
    class Program {
        static void Main(string[] args) {

            string fileName = @"crudtmpfile.txt";
            ReadLastLineInFile(fileName);

            Console.ReadLine();
        }

        public static void CreateCrudFiles(string fileName) {

            try {
                Console.WriteLine("Creating a new file in the disk");

                using (FileStream fileStr = File.Create(fileName)){
                    //This will save the file in the netcore folder
                    Console.WriteLine($"File {fileName} was created successfully.");
                }

            } catch (Exception e) {

                Console.WriteLine(e.Message);
            }
        }

        public static void DeleteCrudFiles(string fileName) {

            Console.WriteLine("In the process of deleting the file. Please wait...,");
            Console.WriteLine("---");

            if (File.Exists(fileName)){
                File.Delete(fileName);

                Console.WriteLine($"File {fileName} has been deleted.");
            } else {
                Console.WriteLine($"File {fileName} cannot be deleted or does not exist");
            }

        }

        public static void ReCreateCrudFiles(string fileName) {
            try {
                Console.WriteLine(@"In the process of creating a new file.");
                if (File.Exists(fileName)) {
                    Console.WriteLine("Existing same name files will be deleted and a new one recreated.");

                    File.Delete(fileName);
                }
                using(FileStream fileStr = File.Create(fileName)) {
                    Console.WriteLine($"File {fileName} has been created");
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateFileAndContent(string fileName) {
            
            try {
                Console.WriteLine($"Checking if file {fileName} already exist");
                if (File.Exists(fileName)) {
                    Console.WriteLine($"File {fileName} is being deleted.");
                    File.Delete(fileName);
                }

                using (StreamWriter writer = File.CreateText(fileName)) {
                    Console.WriteLine("A new file created and content added");

                    writer.WriteLine("Hello C# and .Net Core");
                    writer.WriteLine("Content is being added everytime the project is ran");
                    writer.WriteLine("Learning new technologies and framework everyday");
                    writer.WriteLine("Then I crud all day, everyday");
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public static void ReadFileContent(string fileName) {

            try {
                CreateFileAndContent(fileName);
                using (StreamReader reader = File.OpenText(fileName)) {
                    string r = "";
                    Console.WriteLine($"Here are the content from the {fileName} file:\n");
                    while((r = reader.ReadLine()) != null) {
                        Console.WriteLine($"\t {r}");
                    }
                    Console.WriteLine("");
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                
            }
        }

        public static void WriteTextArrToFile(string fileName) {
            string[] Lines;
            int numLines, z;

            Console.WriteLine($"Checking if file {fileName} already exist");
            if (File.Exists(fileName)) {
                Console.WriteLine($"File {fileName} is being deleted.");

                File.Delete(fileName);
            }
            Console.WriteLine("How many lines do you want to write in the file?");
            numLines = Convert.ToInt32(Console.ReadLine());
            Lines = new string[numLines];
            Console.WriteLine($"Input {numLines} strings below");

            for(z = 0; z < numLines; z++) {
                Console.WriteLine($"Input {numLines} {z + 1}");
                Lines[z] = Console.ReadLine();
            }
            File.WriteAllLines(fileName, Lines);

            using (StreamReader reader = File.OpenText(fileName)) {
                string r = "";
                Console.WriteLine($"Here are the content from the {fileName} file:\n");
                while ((r = reader.ReadLine()) != null) {
                    Console.WriteLine($"\t {r}");
                }
                Console.WriteLine("");
            }
        }

        public static void AppendTextToFile(string fileName) {
            try {
                Console.WriteLine(@"In the process of creating a new file.");
                if (File.Exists(fileName)) {
                    Console.WriteLine("Existing same name files will be deleted and a new one recreated.");

                    File.Delete(fileName);
                }

                //Maybe the above code needs to go
                Console.WriteLine("\n\nAppending text to an existing file:\n");
                Console.WriteLine("-----------------------------------------");

                using (StreamWriter writer = File.CreateText(fileName)) {
                    writer.WriteLine("I am appending new lines to this text");
                    writer.WriteLine("This is the first content and it looks good");
                    writer.WriteLine("This is the second content and it looks even better");
                }

                using (StreamReader reader = File.OpenText(fileName)) {
                    string data = "";
                    Console.WriteLine($"Here are the content of the file {fileName}");
                    while ((data = reader.ReadLine()) != null) {
                        Console.WriteLine(data);
                    }
                    Console.WriteLine("");
                }

                using (StreamWriter file = new StreamWriter(fileName, true)) {
                    file.WriteLine("This is the line appended at the last line.");
                }

                using(StreamReader file = File.OpenText(fileName)) {
                    string data = "";
                    Console.WriteLine($"Here is the content of the file after appending the text: ");
                    while((data = file.ReadLine()) != null) {
                        Console.WriteLine(data);
                    }
                    Console.WriteLine("");
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public static void CreateCopyFiles(string fileName) {
            
            string fileOne = @"fileOne.txt";
            string fileTwo = @"fileTwo.txt";

            Console.WriteLine(@"In the process of creating a new file.");
            if (File.Exists(fileOne)) {
                Console.WriteLine("Existing same name files will be deleted and a new one recreated.");

                File.Delete(fileOne);
            }

            Console.WriteLine("\n\nCreating a file, then copying the file");
            Console.WriteLine("------------------------------------------");

            using(StreamWriter writer = File.CreateText(fileOne)) {
                writer.WriteLine("I am now created file number one");
                writer.WriteLine("These are all original content");
                writer.WriteLine("I am good at what I do and I am the master of my code");
            }

            using (StreamReader reader = File.OpenText(fileOne)) {
                string data = "";
                Console.WriteLine($"Here are the content inside of file {fileOne}:");
                while ((data = reader.ReadLine()) != null) {
                    Console.WriteLine(data);
                }
                Console.WriteLine("");
            }

            //working on getting the folder path for source and target
            string sourceFolder = @"~Assets";
            string targetFolder = @"~Assets";

            string sourceFile = Path.Combine(sourceFolder, fileOne);
            string targetFile = Path.Combine(targetFolder, fileTwo);

            if (Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            File.Copy(sourceFile, targetFile, true);
            File.Copy(sourceFile, targetFile, true);

            Console.WriteLine($"The file {fileOne} successfully copied to the same {fileTwo}");

            using (StreamReader reader = File.OpenText(fileOne)) {
                string data = "";
                Console.WriteLine($"Here is the content of the file inside of {fileOne}");
                while ((data = reader.ReadLine()) != null) {
                    Console.WriteLine(data);
                }
                Console.WriteLine("");
            }

        }

        public static void ReadFirstLineInFile(string fileName) {
            try {
                // Delete the file if it exists.
                if (File.Exists(fileName)) {
                    File.Delete(fileName);
                }
                Console.Write("\n\n Read the first line from a file  :\n");
                Console.Write("---------------------------------------\n");
                // Create the file.
                using (StreamWriter fileStr = File.CreateText(fileName)) {
                    fileStr.WriteLine(" test line 1");
                    fileStr.WriteLine(" test line 2");
                    fileStr.WriteLine(" Test line 3");
                }
                using (StreamReader sr = File.OpenText(fileName)) {
                    string s = "";
                    Console.WriteLine(" Here is the content of the file mytest.txt : ");
                    while ((s = sr.ReadLine()) != null) {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("");
                }

                Console.Write("\n The content of the first line of the file is :\n");
                if (File.Exists(fileName)) {
                    string[] lines = File.ReadAllLines(fileName);
                    Console.Write(lines[0]);
                }
                Console.WriteLine();
            } catch (Exception MyExcep) {
                Console.WriteLine(MyExcep.ToString());
            }
        }

        public static void ReadLastLineInFile(string fileName) {

            string[] ArrLines;
            int n, i;

            Console.Write("\n\n Create and read the last line of a file  :\n");
            Console.Write("-----------------------------------------------\n");

            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }

            Console.Write(" Input number of lines to write in the file  :");
            n = Convert.ToInt32(Console.ReadLine());
            ArrLines = new string[n];
            Console.Write(" Input {0} strings below :\n", n);

            for (i = 0; i < n; i++) {
                Console.Write(" Input line {0} : ", i + 1);
                ArrLines[i] = Console.ReadLine();
            }

            File.WriteAllLines(fileName, ArrLines);

            Console.Write("\n The content of the last line of the file {0} is  :\n", fileName);
            if (File.Exists(fileName)) {
                string[] lines = File.ReadAllLines(fileName);
                Console.WriteLine(" {0}", lines[n - 1]);
            }

            Console.WriteLine();
        }

        public static void ReadingSpecificLinesFromFile(string fileName) {
            string[] ArrLines;
            int n, i, l;

            Console.Write("\n\n Read a specific line from a file  :\n");
            Console.Write("----------------------------------------\n");

            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }
            Console.Write(" Input number of lines to write in the file  :");

            n = Convert.ToInt32(Console.ReadLine());
            ArrLines = new string[n];
            Console.Write(" Input {0} strings below :\n", n);

            for (i = 0; i < n; i++) {
                Console.Write(" Input line {0} : ", i + 1);
                ArrLines[i] = Console.ReadLine();
            }

            File.WriteAllLines(fileName, ArrLines);

            Console.Write("\n Input which line you want to display  :");
            l = Convert.ToInt32(Console.ReadLine());

            if (l >= 1 && l <= n) {
                Console.Write("\n The content of the line {0} of the file {1} is : \n", l, fileName);
                if (File.Exists(fileName)) {
                    string[] lines = File.ReadAllLines(fileName);
                    Console.WriteLine(" {0}", lines[l - 1]);
                }
            } else {
                Console.WriteLine(" Please input the correct line no.");
            }
        }

        public static void ReadAndCountNumberOfLinesInFile(string fileName) {
            int count;
            try {
                // Delete the file if it exists.
                if (File.Exists(fileName)) {
                    File.Delete(fileName);
                }

                Console.Write("\n\n Count the number of lines in a file :\n");
                Console.Write("------------------------------------------\n");
                // Create the file.
                using (StreamWriter writer = File.CreateText(fileName)) {
                    writer.WriteLine("Test line 1 has been added");
                    writer.WriteLine("Test line 2 has been added");
                    writer.WriteLine("Test line 3 has been added");
                    writer.WriteLine("Test line 4 has been added");
                    writer.WriteLine("Test line 5 has been added");
                    writer.WriteLine("Test line 6 has been added");
                }

                using (StreamReader reader = File.OpenText(fileName)) {
                    string data = "";
                    count = 0;
                    Console.WriteLine($"Here is the content of the file {fileName}: ");
                    while ((data = reader.ReadLine()) != null) {
                        Console.WriteLine(data);
                        count++;
                    }
                    Console.WriteLine("");
                }

                Console.Write($"\tThe number of lines in  the file {fileName} is : {count} \n\n");

            } catch (Exception MyExcep) {
                Console.WriteLine(MyExcep.ToString());
            }
        }
    }
}
