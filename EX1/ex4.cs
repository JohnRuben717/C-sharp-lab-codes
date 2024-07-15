using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Addition of numbers:");
        double sum = Add(1.2, 2.3, 3.4, 4.5, 5.6);
        Console.WriteLine($"Sum: {sum}");

        Console.WriteLine("\nSubtraction of numbers:");
        double result = Subtract(20.5, 2.3, 3.4, 4.5);
        Console.WriteLine($"Result: {result}");
    }

    static double Add(params double[] numbers)
    {
        double sum = 0;
        foreach (double number in numbers)
        {
            sum += number;
        }
        return sum;
    }

    static double Subtract(double first, params double[] numbers)
    {
        double result = first;
        foreach (double number in numbers)
        {
            result -= number;
        }
        return result;
    }
}
