using System;
using BouncyBallDropTest;

namespace BouncyBallDropTest
{
    class Ball
    {
        //In meters
        public double DropHeight { get; set;}

        //This is the bounciness factor of the ball e.g. 0.6 means that the ball will bounce to 60% of the height of the previous drop
        public double Bounciness { get; set;}

        //Measurements are complete when the calculated bounce height reaches below this threshold.
        public double ThresholdHeight { get; set;}

        public Ball(double dropHeight, double bounciness, double thresholdHeight)
        {
            DropHeight = dropHeight;
            Bounciness = bounciness;
            ThresholdHeight = thresholdHeight;
        }

        public (int, List<double>) BounceCalculation()
        {
            //Stores the heights of each bounce.
            List<double> bounceStorage = new List<double>();

            double currentHeight = DropHeight;

                //Tracks the number of bounces
            int bounceIterator = 0;

            while (currentHeight > ThresholdHeight)
            {
                currentHeight *= Bounciness;
                bounceStorage.Add(currentHeight);
                bounceIterator++;
            }

            return (bounceIterator, bounceStorage);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int indexer = 1;
        int ballSelection = 0;
        string ballName;
        double dropHeightInput = 0;
        double thresholdInput = 0;
        double bouncinessInput;
        bool isValid = false;

        Console.Clear();
        Console.Write("Welcome to the bouncy ball calculator! Please select a ball by entering the corresponding number. e.g. \"1\" for a Rubber bouncy ball.\n\n");
        Console.Write("1: Rubber bouncy ball.\n2: Tennis ball.\n3: Bowling ball.\n");

        //Get the ball selection.
        while(isValid == false)
        {
            string userInput = Console.ReadLine();

            try
            {
                ballSelection = Convert.ToInt32(userInput);
                
                if(ballSelection < 4 && ballSelection > 0)
                {
                    isValid = true; 
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer between 1 and 3.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid integer between 1 and 3.");
            }
        }

        //resetting to reuse.
        isValid = false;

        Console.Clear();

        Console.WriteLine("Please enter the drop height in meters and centimeters, e.g. 10,63 would be a drop height of 10 meters 63 centimeters.");
        
        //Get the initial drop height. Depending on your configuration it will accept either decimals with a period or a comma.
        while(isValid == false)
        {
            string userInput = Console.ReadLine();

            try
            {
                dropHeightInput = Convert.ToDouble(userInput);
                isValid = true; 
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid value, e.g. 10 or 6,35.");
            }
        }

        isValid = false;

        Console.Clear();

        Console.WriteLine("Please set the threshold height in meters and centimeters, e.g. 0,25. When the ball bounces below this height, measurements will stop.");

        //Get threshold height.
        while(isValid == false)
        {
            string userInput = Console.ReadLine();

            try
            {
                thresholdInput = Convert.ToDouble(userInput);
                isValid = true; 
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid value, e.g. 10 or 0,35.");
            }
        }

        Console.Clear();

        //Assign bounciness characteristic.
        if(ballSelection == 1)
        {
            bouncinessInput = 0.8;
            ballName = "Rubber bouncy ball";
        }
        else if(ballSelection == 2)
        {
            bouncinessInput = 0.6;
            ballName = "Tennis ball";
        }
        else
        {
            bouncinessInput = 0.3;
            ballName = "Bowling ball";
        }

        //Create ball simulation using the users inputs.
        Ball bouncyball = new Ball(dropHeightInput, bouncinessInput, thresholdInput);

        //Get the number of bounces and the list of bounce heights from the tuple.
        var simulationResult = bouncyball.BounceCalculation();
        int bounceCount = simulationResult.Item1;
        List<double> bounceLogging = simulationResult.Item2;

        //Summary
        Console.WriteLine($"Ball: {ballName}\nInitial drop height: {dropHeightInput} meter(s)\nThreshold height: {thresholdInput} meter(s)\n");
        Console.WriteLine($"The ball bounced {bounceCount} times before reaching the threshold value. Bounce height breakdown is below:\n");

        foreach (var measurement in bounceLogging)
            {
                Console.WriteLine($"Bounce {indexer}: {measurement} meters.");
                indexer++;
            }
    }
}
