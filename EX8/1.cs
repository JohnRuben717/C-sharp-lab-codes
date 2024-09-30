using System;
using System.Threading;

class Program
{
    static int counter = 1;
    static int limit = 50;
    static readonly object locker = new object();

    static void Main(string[] args)
    {
        Thread evenThread = new Thread(PrintEven);
        Thread oddThread = new Thread(PrintOdd);
        Thread multipleOfFiveThread = new Thread(PrintMultipleOfFive);

        evenThread.Start();
        oddThread.Start();
        multipleOfFiveThread.Start();

        evenThread.Join();
        oddThread.Join();
        multipleOfFiveThread.Join();

        Console.WriteLine("Finished printing numbers.");
    }

    static void PrintEven()
    {
        while (true)
        {
            lock (locker)
            {
                while (counter <= limit && (counter % 2 != 0 || counter % 5 == 0))
                {
                    Monitor.Wait(locker);
                }
                if (counter > limit)
                {
                    Monitor.PulseAll(locker);
                    break;
                }
                Console.WriteLine("Even Thread: " + counter);
                counter++;
                Monitor.PulseAll(locker);
            }
        }
    }

    static void PrintOdd()
    {
        while (true)
        {
            lock (locker)
            {
                while (counter <= limit && (counter % 2 == 0 || counter % 5 == 0))
                {
                    Monitor.Wait(locker);
                }
                if (counter > limit)
                {
                    Monitor.PulseAll(locker);
                    break;
                }
                Console.WriteLine("Odd Thread: " + counter);
                counter++;
                Monitor.PulseAll(locker);
            }
        }
    }

    static void PrintMultipleOfFive()
    {
        while (true)
        {
            lock (locker)
            {
                while (counter <= limit && counter % 5 != 0)
                {
                    Monitor.Wait(locker);
                }
                if (counter > limit)
                {
                    Monitor.PulseAll(locker);
                    break;
                }
                Console.WriteLine("Multiple of Five Thread: " + counter);
                counter++;
                Monitor.PulseAll(locker);
            }
        }
    }
}
