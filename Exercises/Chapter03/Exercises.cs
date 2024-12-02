using System;

namespace Exercises.Chapter3
{
   // 1. Write a console app that calculates a user's Body-Mass Index:
   //   - prompt the user for her height in metres and weight in kg
   //   - calculate the BMI as weight/height^2
   //   - output a message: underweight(bmi<18.5), overweight(bmi>=25) or healthy weight
   // 2. Structure your code so that structure it so that pure and impure parts are separate
   // 3. Unit test the pure parts
   // 4. Unit test the impure parts using the HOF-based approach

   public static class Bmi
   {
        public static void Run()
        {
            var bmi = CalculateBMI(PromptWeight(), PromptHeight());
            var result = CalculateHealthy(bmi);
            Console.WriteLine($"\nYou {result}");
            Run();
        }

        public static decimal CalculateBMI(decimal weight, decimal height)
            => weight / (height * height);

        public static string CalculateHealthy(decimal bmi)
            => bmi switch
            {
                var x when x < 18.5m => "underweight",
                var x when x >= 25m => "overweight",
                var x when x == 0 => "undefined",
                _ => "healthy",
            };

        public static decimal PromptHeight()
        {
            Console.WriteLine("Please enter your height (metres)");
            decimal.TryParse(Console.ReadLine(), out decimal height);
            return height;
        }

        public static decimal PromptWeight()
        {
            Console.WriteLine("Please enter your weight (kg)");
            decimal.TryParse(Console.ReadLine(), out decimal weight);
            return weight;
        }
    }
}
