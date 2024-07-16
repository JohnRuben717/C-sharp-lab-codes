using System;

class ex7
{
    static void Main(string[] args)
    {
        Console.WriteLine("How many registration numbers would you like to enter?");
        int size = int.Parse(Console.ReadLine());
        string[] regNos = new string[size];

        // Input registration numbers
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Enter registration number {i + 1}: ");
            regNos[i] = Console.ReadLine();
        }

        // Display all registration numbers
        Console.WriteLine("\nAll entered registration numbers:");
        foreach (string regNo in regNos)
        {
            Console.WriteLine(regNo);
        }

        // Search for a registration number
        Console.WriteLine("\nEnter a registration number to search:");
        string searchRegNo = Console.ReadLine();
        bool found = false;
        
        foreach (string regNo in regNos)
        {
            if (regNo == searchRegNo)
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            Console.WriteLine($"{searchRegNo} is present in the array.");
        }
        else
        {
            Console.WriteLine($"{searchRegNo} is not present in the array.");
        }
        Console.ReadLine();
    }
}
