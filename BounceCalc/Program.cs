using System;
using System.Linq;
using BouncyBallDropTest;
using System.Collections.Generic;

namespace BouncyBallDropTest
{
	class Ball
	{
		//In meters
		public double DropHeight { get; set; }
		//This is the bounciness factor of the ball e.g. 0.6 means that the ball will bounce to 60% of the height of the previous drop
		public double Bounciness { get; set; }
		//Measurements are complete when the calculated bounce height reaches below this threshold.
		public double ThresholdHeight { get; set; }

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
		int ballSelection = 0;
		string ballName = string.Empty;
		double dropHeightInput = 0;
		double thresholdInput = 0;
		double bouncinessInput = default;
		
		PrintBallSelectionText();
		//Get the ball selection.
		while (true)
		{
			try
			{
				ballSelection = Convert.ToInt32(Console.ReadLine()); //Convert.ToInt32(userInput);
				if (Enumerable.Range(1, 3).Contains(ballSelection)) // Same as "if x > 0 and x < 4"
				{
					break; //makes the bool 'isValid' unnecessary
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

		PrintHeightSelectionText();
		//Get the initial drop height. Depending on your configuration it will accept either decimals with a period or a comma.
		while (true)
		{
			try
			{
				dropHeightInput = Convert.ToDouble(Console.ReadLine());
				break;
			}
			catch (FormatException)
			{
				Console.WriteLine("Please enter a valid value, e.g. 10 or 6,35.");
			}
		}

		PrintThresholdSelectionText();
		//Get threshold height.
		while (true)
		{
			try
			{
				thresholdInput = Convert.ToDouble(Console.ReadLine());
				break;
			}
			catch (FormatException)
			{
				Console.WriteLine("Please enter a valid value, e.g. 10 or 0,35.");
			}
		}

		//Assign bounciness characteristic.
		switch (ballSelection)
		{
			case 1:
			{
				bouncinessInput = 0.8;
				ballName = "Rubber bouncy ball";
			}

				break;
			case 2:
			{
				bouncinessInput = 0.6;
				ballName = "Tennis ball";
			}

				break;
			case 3:
			{
				bouncinessInput = 0.3;
				ballName = "Bowling ball";
			}

				break;
		}

		//Create ball simulation using the users inputs.
		Ball bouncyball = new Ball(dropHeightInput, bouncinessInput, thresholdInput);
		//Get the number of bounces and the list of bounce heights from the tuple.
		var simulationResult = bouncyball.BounceCalculation();
		List<double> bounceLogging = simulationResult.Item2;
		//Summary
		PrintSummary(ballName, dropHeightInput, thresholdInput, simulationResult.Item1);
        //idx + 1 to start at 1
		foreach (var(measurement, idx)in bounceLogging.Select((measurement, idx) => (measurement, idx + 1)))
		{
			Console.WriteLine($"Bounce {idx}: {measurement} meters");
		}
	}

	public static void PrintSummary(string ballName, double dropHeight, double threshold, int bounceCount)
	{
		Console.Clear();
		Console.WriteLine($"Ball: {ballName}\nInitial drop height: {dropHeight} meter(s)\nThreshold height: {threshold} meter(s)\n");
		Console.WriteLine($"The ball bounced {bounceCount} times before reaching the threshold value. Bounce height breakdown is below:\n");
	}

	public static void PrintThresholdSelectionText()
	{
		Console.Clear();
		Console.WriteLine("Please set the threshold height in meters and centimeters, e.g. 0,25. When the ball bounces below this height, measurements will stop.");
	}

	public static void PrintHeightSelectionText()
	{
		Console.Clear();
		Console.WriteLine("Please enter the drop height in meters and centimeters, e.g. 10,63 would be a drop height of 10 meters 63 centimeters.");
	}

	public static void PrintBallSelectionText()
	{
		Console.Clear();
		Console.Write("Welcome to the bouncy ball calculator! Please select a ball by entering the corresponding number. e.g. \"1\" for a Rubber bouncy ball.\n\n");
		Console.Write("1: Rubber bouncy ball.\n2: Tennis ball.\n3: Bowling ball.\n");
	}
}
