using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static Queue<string> messageQueue = new Queue<string>();
    static readonly object locker = new object();
    static bool isProducing = true;

    static void Main(string[] args)
    {
        Thread producerThread = new Thread(Producer);
        Thread consumerThread = new Thread(Consumer);

        producerThread.Start();
        consumerThread.Start();

        producerThread.Join();
        lock (locker)
        {
            isProducing = false;
            Monitor.PulseAll(locker);
        }
        consumerThread.Join();

        Console.WriteLine("Finished producing and consuming messages.");
    }

    static void Producer()
    {
        for (int i = 1; i <= 10; i++)
        {
            lock (locker)
            {
                string message = "Message " + i;
                messageQueue.Enqueue(message);
                Console.WriteLine("Produced: " + message);
                Monitor.PulseAll(locker);
            }
            Thread.Sleep(500); // Simulate work
        }
    }

    static void Consumer()
    {
        while (true)
        {
            string message = null;
            lock (locker)
            {
                while (messageQueue.Count == 0 && isProducing)
                {
                    Monitor.Wait(locker);
                }
                if (messageQueue.Count > 0)
                {
                    message = messageQueue.Dequeue();
                }
                else if (!isProducing)
                {
                    break;
                }
            }
            if (message != null)
            {
                Console.WriteLine("Consumed: " + message);
            }
            Thread.Sleep(1000); // Simulate work
        }
    }
}
