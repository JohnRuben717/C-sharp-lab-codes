using System;

// Custom exception class
public class InvalidVowelException : Exception
{
    public InvalidVowelException(char character)
        : base($"Invalid input: '{character}' is a consonant.")
    {
    }
}

public class Program
{
    public static void Main()
    {
        try
        {
            // Initialize character array for vowels
            char[] vowels = new char[5]; // Assuming we want to store 5 vowels

            Console.WriteLine("Enter 5 vowels:");

            for (int i = 0; i < vowels.Length; i++)
            {
                Console.Write($"Enter vowel {i + 1}: ");
                char inputChar = Console.ReadKey().KeyChar;
                Console.WriteLine();

                // Validate input
                if (!IsVowel(inputChar))
                {
                    throw new InvalidVowelException(inputChar);
                }

                vowels[i] = inputChar;
            }

            Console.WriteLine("Vowels entered successfully:");
            foreach (char vowel in vowels)
            {
                Console.Write(vowel + " ");
            }
        }
        catch (InvalidVowelException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Program execution completed.");
        }
    }

    // Method to check if a character is a vowel
    public static bool IsVowel(char c)
    {
        // Convert character to lowercase to simplify comparison
        char lowerChar = char.ToLower(c);

        return lowerChar == 'a' || lowerChar == 'e' || lowerChar == 'i' || lowerChar == 'o' || lowerChar == 'u';
    }
}
