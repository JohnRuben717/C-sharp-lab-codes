using System;
using System.Threading;

class Program
{
    static volatile bool keepRunning = true;

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(() => PrintMessage("Good Morning", 1000));
        Thread thread2 = new Thread(() => PrintMessage("Hello", 2000));
        Thread thread3 = new Thread(() => PrintMessage("Welcome", 3000));

        thread1.Start();
        thread2.Start();
        thread3.Start();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        Console.WriteLine("Program ended.");
    }

    static void PrintMessage(string message, int interval)
    {
        while (keepRunning)
        {
            Console.WriteLine(message);
            Thread.Sleep(interval);
        }
    }
}
