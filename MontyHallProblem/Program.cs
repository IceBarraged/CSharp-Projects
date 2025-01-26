using System;
using MontyHallSimulation;

namespace MontyHallSimulation
{
    public class MontyHallProblem
    {
        //Initial contestant door choice.
        public int DoorChoice { get; set; }
        
        public MontyHallProblem(int doorChoice)
        {
            DoorChoice = doorChoice;
        }

        //Randomly seeds the car, outputs values required for displaying a door to the contestant.    
        public(int, int, int) SetStage()
        {
            Random cpuChoices = new();
            List<int> doorChoices = new List<int>{1,2,3};
            int lastDoor = 0;
            int showDoor = 0;
            
            //Makes the users door choice work with zero indexing.
            DoorChoice = DoorChoice - 1;

            //Assign the door number that the car is behind. Gets an index then assigns the value behind it to beat zero indexing.
            int tempStorage = cpuChoices.Next(doorChoices.Count);
            int carChoice = doorChoices[tempStorage];


            //Car door and users door match
            if (tempStorage == DoorChoice)
            {
                //Remove the users chosen door. removes car door from the hosts choice.
                doorChoices.RemoveAt(DoorChoice);  

                //Gets the hosts door, then removes it from the selection.
                tempStorage = cpuChoices.Next(doorChoices.Count);
                showDoor = doorChoices[tempStorage];
                doorChoices.RemoveAt(tempStorage);

                //Get remaining door (user can switch to this)
                lastDoor = doorChoices[0];
            }
            else
            {
                //This ensures the host can only show a door with a goat.
                doorChoices.RemoveAt(DoorChoice);

                //Locks the car behind the other door. removes car door from hosts choice.
                lastDoor = doorChoices[tempStorage];
                doorChoices.RemoveAt(tempStorage);
                showDoor = doorChoices[0];
            }

            return(carChoice, showDoor, lastDoor);           
        }
        
    }
}

class Program
{
    static void Main(string[] args)
    {
        //wait variables.
        int waitL = 3000;
        int waitM = 1500;

        bool isValid = false;
        int userDoor = 0;
        string userChoice = "";

        ///Do title.
        Console.Clear();
        Console.Write("Welcome to the...");
        Thread.Sleep(waitL);
        Console.Clear();
        Console.Write("MONTY!!!");
        Thread.Sleep(waitM);
        Console.Clear();
        Console.Write("MONTY HALL!!!");
        Thread.Sleep(waitM);
        Console.Clear();
        Console.Write("MONTY HALL GAME!!!");
        Thread.Sleep(waitL);
        Console.Clear();

        //Do intro.
        Console.WriteLine("There are 3 doors in front of you, and behind one is a BRAND NEW CAR!!!");
        Thread.Sleep(waitL);
        Console.WriteLine("-The audience cheers with excitement, as if they were about to collectively win the car!-");
        Thread.Sleep(waitL*2);
        Console.Clear();    

        //Get User door choice.
        Console.Write("All you have to do, is pick a door number between 1 and 3!\nYour door choice: ");

        //validation1
        while(isValid == false)
        {
            string userInput = Console.ReadLine();

            try
            {
                userDoor = Convert.ToInt32(userInput);
                
                if(userDoor > 3 || userDoor <= 0)
                {
                  Console.WriteLine("Please enter a valid integer between 1 and 3.");  
                }
                else
                {
                    isValid = true; 
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid integer between 1 and 3.");
            }
        }

        //Resetting to reuse.
        isValid = false;

        //Get values for game.
        MontyHallProblem setdoors = new MontyHallProblem(userDoor);
        var setdoorresult = setdoors.SetStage();
        int carChoice = setdoorresult.Item1;
        int showDoor = setdoorresult.Item2;
        int lastDoor = setdoorresult.Item3;

        Console.Clear();
        Console.WriteLine($"I am now going to show you what lies behind door number {showDoor}!");
        Thread.Sleep(waitM);
        Console.Write("behind the door is a goat. Baaaaa.");
        Thread.Sleep(waitL);
        Console.Clear();

        Console.WriteLine($"I am now going to give you a choice. You can either switch between your door ({userDoor}) and the remaining door ({lastDoor}), or stick with your door!");
        Console.Write("If you want to stick with your door, type \"Stick\", otherwise, type \"Switch\"!\nYour choice: ");

        //validation2
        while(isValid == false)
        {
            string userInput = Console.ReadLine();

            try
            {
                userChoice = userInput.ToUpper();
                
                if(userChoice == "SWITCH")
                {
                  Console.WriteLine("You have chosen to switch doors!");
                  userDoor = lastDoor;
                  isValid = true;  
                }
                else if(userChoice == "STICK")
                {
                    Console.WriteLine("You have chosen to stick with your current door!");
                    isValid = true;  
                }
                else
                {
                  Console.WriteLine("Please enter either \"Switch\" or \"Stick\"");  
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter either \"Switch\" or \"Stick\"");  
            }
        }

        Thread.Sleep(waitL);
        Console.Clear();

        //Do final calculations.

        Console.WriteLine($"It is time to see what is behind your chosen door, door {userDoor}!");  
        Thread.Sleep(waitL);

        if(userDoor == carChoice)
        {Console.WriteLine("Congratulations, you win a car!!!");}
        else
        {Console.WriteLine("Congratulations...you won a...goat?");}
    }
}