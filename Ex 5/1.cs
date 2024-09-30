using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Custom Exceptions
public class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string message) : base(message) { }
}

public class ExcessiveTransferException : Exception
{
    public ExcessiveTransferException(string message) : base(message) { }
}

public class InsufficientInitialBalanceException : Exception
{
    public InsufficientInitialBalanceException(string message) : base(message) { }
}

// Account Class
public class Account
{
    public string AccountNumber { get; private set; }
    public string Password { get; private set; }
    public decimal Balance { get; private set; }
    private const decimal MinimumInitialBalance = 500;
    private const decimal MaximumTransferLimit = 10000;

    public Account(string accountNumber, string password, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        SetPassword(password);
        SetInitialBalance(initialBalance);
    }

    private void SetPassword(string password)
    {
        if (!IsValidPassword(password))
        {
            throw new InvalidPasswordException("Invalid password. Password must be max 8 characters long, contain at least one capital letter, one small letter, and one digit.");
        }
        Password = password;
    }

    private bool IsValidPassword(string password)
    {
        return password.Length <= 8 &&
               Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");
    }

    private void SetInitialBalance(decimal initialBalance)
    {
        if (initialBalance < MinimumInitialBalance)
        {
            throw new InsufficientInitialBalanceException($"Initial balance must be at least {MinimumInitialBalance:C}.");
        }
        Balance = initialBalance;
    }

    public void Transfer(decimal amount, Account recipient)
    {
        if (amount > MaximumTransferLimit)
        {
            throw new ExcessiveTransferException($"Transfer amount exceeds the maximum limit of {MaximumTransferLimit:C}.");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds for transfer.");
        }

        Balance -= amount;
        recipient.Balance += amount;
    }
}

// Main Program
class Program
{
    private static List<Account> accounts = new List<Account>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nBanking Application Menu:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Perform Transfer");
            Console.WriteLine("3. View Account Details");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    PerformTransfer();
                    break;
                case "3":
                    ViewAccountDetails();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateAccount()
    {
        try
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            // Handle the password first to trigger InvalidPasswordException if necessary
            Account tempAccount = new Account(accountNumber, password, 0);

            Console.Write("Enter Initial Balance: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                Console.WriteLine("Invalid balance input. Please enter a valid number.");
                return;
            }

            // Now that the initial balance is entered, create the full account
            Account newAccount = new Account(accountNumber, password, initialBalance);
            accounts.Add(newAccount);
            Console.WriteLine("Account created successfully.");
        }
        catch (InvalidPasswordException e)
        {
            Console.WriteLine($"Password Error: {e.Message}");
        }
        catch (InsufficientInitialBalanceException e)
        {
            Console.WriteLine($"Balance Error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    static void PerformTransfer()
    {
        try
        {
            Console.Write("Enter Sender's Account Number: ");
            string senderAccountNumber = Console.ReadLine();
            Account sender = FindAccount(senderAccountNumber);

            if (sender == null)
            {
                Console.WriteLine("Sender account not found.");
                return;
            }

            Console.Write("Enter Recipient's Account Number: ");
            string recipientAccountNumber = Console.ReadLine();
            Account recipient = FindAccount(recipientAccountNumber);

            if (recipient == null)
            {
                Console.WriteLine("Recipient account not found.");
                return;
            }

            Console.Write("Enter Transfer Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount input. Please enter a valid number.");
                return;
            }

            sender.Transfer(amount, recipient);
            Console.WriteLine("Transfer successful.");
        }
        catch (ExcessiveTransferException e)
        {
            Console.WriteLine($"Transfer Error: {e.Message}");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Transfer Error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    static void ViewAccountDetails()
    {
        try
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();
            Account account = FindAccount(accountNumber);

            if (account != null)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Balance: {account.Balance:C}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    static Account FindAccount(string accountNumber)
    {
        return accounts.Find(a => a.AccountNumber == accountNumber);
    }
}
