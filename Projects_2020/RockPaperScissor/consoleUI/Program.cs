using System;

namespace consoleUI
{
    //Thanks to https://alexb72.medium.com/c-tutorial-basic-rock-paper-scissors-game-90e6e2037a0b
    class Program
    {
        static void Main(string[] args)
        {
            string WelcomeMessage = "Welcome TO THE GAME\nROCK PAPER SCISSOR!";
            bool gameLoop = true;
            int userPoints = 0;
            int computerPoints = 0;
            while (gameLoop)
            {
                Console.WriteLine(WelcomeMessage + "Choose a game below");
                Console.WriteLine(WelcomeMessage + "Choose a game below");
                Console.WriteLine("1.Rock");
                Console.WriteLine("2.Paper");
                Console.WriteLine("3.Scissors");
            
                string userChoice = Console.ReadLine();
                Random randomChoice = new Random();
                int computerChoice = randomChoice.Next(1, 4);

                switch (computerChoice)
                {
                    case 1:
                        if (userChoice == "1")
                        {
                            Console.WriteLine("User chose Rock");
                            Console.WriteLine("Computer chose Rock");
                            Console.WriteLine("It is a tie.");
                        }

                        else if (userChoice == "2")
                        {
                            Console.WriteLine("User chose Paper");
                            Console.WriteLine("Computer chose Rock");
                            Console.WriteLine("User wins");
                            userPoints++;
                        }
                        else if (userChoice == "3")
                        {
                            Console.WriteLine("User chose Scissors");
                            Console.WriteLine("Computer chose Rock");
                            Console.WriteLine("Computer wins");
                            computerPoints++;
                        }
                        break;
                    case 2:
                        if (userChoice == "1")
                        {
                            Console.WriteLine("User chose Rock");
                            Console.WriteLine("Computer chose Rock");
                            Console.WriteLine("It is a tie");
                        }

                        else if (userChoice == "2")
                        {
                            Console.WriteLine("User chose Paper");
                            Console.WriteLine("Computer chose Rock");
                            Console.WriteLine("User wins");
                            userPoints++;
                        }
                        else if (userChoice == "3")
                        {
                            Console.WriteLine("User chose Scissors");
                            Console.WriteLine("Computer chose Rock");
                            Console.WriteLine("Computer wins");
                            computerPoints++;
                        }
                        break;
                    case 3:
                        if (userChoice == "1")
                        {
                            Console.WriteLine("User chose Rock");
                            Console.WriteLine("Computer chose Scissors");
                            Console.WriteLine("User wins.");
                            userPoints++;
                        }

                        else if (userChoice == "2")
                        {
                            Console.WriteLine("User chose Paper");
                            Console.WriteLine("Computer chose Scissors");
                            Console.WriteLine("Computer wins");
                            computerPoints++;
                        }
                        else if (userChoice == "3")
                        {
                            Console.WriteLine("User chose Scissors");
                            Console.WriteLine("Computer chose Scissors");
                            Console.WriteLine("It is a tie");
                        }
                        break;
                
                }

                string playAgain = "";

                Console.WriteLine("Do you wish to play again?");
                Console.WriteLine("Enter Y or N");
                Console.ReadLine();

                if (playAgain == "N" || playAgain == "n" || playAgain == "no")
                {
                    gameLoop = false;
                    Console.WriteLine($"User has {userPoints} points - Computer has {computerPoints} points");
                }
            }
        }
    }
}
