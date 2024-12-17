namespace Exercises.Chapter3;
using System;
using System.Transactions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

// 1. Write a console app that calculates a user's Body-Mass Index:
//   - prompt the user for her height in metres and weight in kg
//   - calculate the BMI as weight/height^2
//   - output a message: underweight(bmi<18.5), overweight(bmi>=25) or healthy weight
// 2. Structure your code so that structure it so that pure and impure parts are separate
// 3. Unit test the pure parts
// 4. Unit test the impure parts using the HOF-based approach

public enum BmiRange
{
    Healthy,
    Underweight,
    Overweight,
    Undefined
}

public static class Bmi
{
    public static void Run()
    {
        Run(Read, Write);
    }

    public static void Run(Func<string, double> read, Action<BmiRange> write)
    {
        // inputs
        double weight = read("Weight")
            , height = read("Height");

        // computation
        var result = CalculateBMI(weight, height).CalculateHealthy();

        // output
        write(result);
    }
        

    public static double CalculateBMI(double weight, double height)
        => Math.Round(weight / (height * height), 1);

    public static BmiRange CalculateHealthy(this double bmi)
        => bmi switch
        {
            var x when x < 18.5 => BmiRange.Underweight,
            var x when x >= 25 => BmiRange.Overweight,
            var x when x == 0 => BmiRange.Undefined,
            _ => BmiRange.Healthy,
        };

    public static double Read(string field)
    {
        Console.WriteLine($"Plesae enter your {field}");
        return double.Parse(Console.ReadLine());
    }

    public static void Write(BmiRange result)
        => Console.WriteLine($"\nBase your BMI, Result: {result}");
}


public class BmiTest
{
    [TestCase(70, 1.7, ExpectedResult = 24.2)]
    [TestCase(65, 1.6, ExpectedResult = 25.4)]
    [TestCase(50, 1.7, ExpectedResult = 17.3)]
    public double When_Input_WeightHeight_CalculateBMI_Resesult_Will_Be(double weight, double height)
        => Bmi.CalculateBMI(weight, height);

    [TestCase(24.2, ExpectedResult = BmiRange.Healthy)]
    [TestCase(25.4, ExpectedResult = BmiRange.Overweight)]
    [TestCase(17.3, ExpectedResult = BmiRange.Underweight)]
    public BmiRange When_Input_BMI_CalculateHealthy_Result_Be(double bmi)
        => Bmi.CalculateHealthy(bmi);

    [TestCase(70, 1.7, ExpectedResult = BmiRange.Healthy)]
    [TestCase(65, 1.6, ExpectedResult = BmiRange.Overweight)]
    [TestCase(50, 1.7, ExpectedResult = BmiRange.Underweight)]
    public BmiRange When_Inputs_CalculateBMIandHealthy_Result(double weight, double height)
    {
        // Arrange
        var result = default(BmiRange);
        Func<string, double> read = s => s == "Weight" ? weight : height;
        Action<BmiRange> write = r => result = r;

        // Action
        Bmi.Run(read, write);

        // Assert
        return result;
    }
}