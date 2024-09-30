using System;

public class MyCalculator
{
    // Method to compute n^p with exception handling
    public long power(int n, int p)
    {
        // Check if both n and p are zero
        if (n == 0 && p == 0)
        {
            throw new ArgumentException("n and p should not be zero.");
        }
        // Check if either n or p is negative
        else if (n < 0 || p < 0)
        {
            throw new ArgumentException("n or p should not be negative.");
        }
        else
        {
            // Calculate and return n^p
            return (long)Math.Pow(n, p);
        }
    }

    public static void Main(string[] args)
    {
        MyCalculator calculator = new MyCalculator();
        
        try
        {
            Console.WriteLine("Enter base (n): ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter exponent (p): ");
            int p = int.Parse(Console.ReadLine());

            long result = calculator.power(n, p);
            Console.WriteLine($"Result of {n}^{p} is: {result}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid input. Please enter an integer.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
