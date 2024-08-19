using System;

namespace TimeOperatorOverloading
{
    // Interface for operations
    public interface IOperations
    {
        void Display();
    }

    // Time class implementing IOperations
    public class Time : IOperations
    {
        private int hour;
        private int minute;
        private int second;

        // Constructor
        public Time(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            Normalize();
        }

        // Overloaded constructor for initialization
        public Time(int totalSeconds)
        {
            hour = totalSeconds / 3600;
            minute = (totalSeconds % 3600) / 60;
            second = totalSeconds % 60;
        }

        // Normalize the time values
        private void Normalize()
        {
            if (second >= 60)
            {
                minute += second / 60;
                second %= 60;
            }
            if (minute >= 60)
            {
                hour += minute / 60;
                minute %= 60;
            }
        }

        // Display function to print time in hh:mm:ss format
        public void Display()
        {
            Console.WriteLine($"{hour:D2}:{minute:D2}:{second:D2}");
        }

        // Overload + operator
        public static Time operator +(Time t1, Time t2)
        {
            return new Time(t1.hour + t2.hour, t1.minute + t2.minute, t1.second + t2.second);
        }

        // Overload - operator
        public static Time operator -(Time t1, Time t2)
        {
            int totalSeconds1 = t1.hour * 3600 + t1.minute * 60 + t1.second;
            int totalSeconds2 = t2.hour * 3600 + t2.minute * 60 + t2.second;
            return new Time(Math.Max(0, totalSeconds1 - totalSeconds2));
        }

        // Overload == operator
        public static bool operator ==(Time t1, Time t2)
        {
            return t1.hour == t2.hour && t1.minute == t2.minute && t1.second == t2.second;
        }

        // Overload != operator
        public static bool operator !=(Time t1, Time t2)
        {
            return !(t1 == t2);
        }

        // Overload <= operator
        public static bool operator <=(Time t1, Time t2)
        {
            return (t1.hour < t2.hour) || 
                   (t1.hour == t2.hour && t1.minute < t2.minute) || 
                   (t1.hour == t2.hour && t1.minute == t2.minute && t1.second <= t2.second);
        }

        // Overload < operator
        public static bool operator <(Time t1, Time t2)
        {
            return (t1.hour < t2.hour) || 
                   (t1.hour == t2.hour && t1.minute < t2.minute) || 
                   (t1.hour == t2.hour && t1.minute == t2.minute && t1.second < t2.second);
        }

        // Overload >= operator
        public static bool operator >=(Time t1, Time t2)
        {
            return !(t1 < t2);
        }

        // Overload > operator
        public static bool operator >(Time t1, Time t2)
        {
            return !(t1 <= t2);
        }

        // Implicit conversion from Time to int (total seconds)
        public static implicit operator int(Time t)
        {
            return t.hour * 3600 + t.minute * 60 + t.second;
        }

        // Explicit conversion from int to Time
        public static explicit operator Time(int totalSeconds)
        {
            return new Time(totalSeconds);
        }

        // Override Equals and GetHashCode
        public override bool Equals(object obj)
        {
            if (obj is Time t)
            {
                return this == t;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (hour, minute, second).GetHashCode();
        }
    }

    // TimeTest class for demonstration
    public class TimeTest
    {
        public static void Main(string[] args)
        {
            Time time1 = new Time(2, 30, 45);
            Time time2 = new Time(1, 45, 30);

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Display Time 1");
                Console.WriteLine("2. Display Time 2");
                Console.WriteLine("3. Add Time 1 and Time 2");
                Console.WriteLine("4. Subtract Time 2 from Time 1");
                Console.WriteLine("5. Compare Time 1 and Time 2 (==)");
                Console.WriteLine("6. Compare Time 1 and Time 2 (<=)");
                Console.WriteLine("7. Convert Time 1 to total seconds");
                Console.WriteLine("8. Convert total seconds to Time");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Time 1: ");
                        time1.Display();
                        break;
                    case 2:
                        Console.Write("Time 2: ");
                        time2.Display();
                        break;
                    case 3:
                        Time sum = time1 + time2;
                        Console.Write("Sum of Time 1 and Time 2: ");
                        sum.Display();
                        break;
                    case 4:
                        Time difference = time1 - time2;
                        Console.Write("Difference (Time 1 - Time 2): ");
                        difference.Display();
                        break;
                    case 5:
                        Console.WriteLine($"Time 1 == Time 2: {time1 == time2}");
                        break;
                    case 6:
                        Console.WriteLine($"Time 1 <= Time 2: {time1 <= time2}");
                        break;
                    case 7:
                        int totalSeconds = time1;
                        Console.WriteLine($"Total seconds in Time 1: {totalSeconds}");
                        break;
                    case 8:
                        Console.Write("Enter total seconds: ");
                        int seconds = int.Parse(Console.ReadLine());
                        Time convertedTime = (Time)seconds;
                        Console.Write("Converted Time: ");
                        convertedTime.Display();
                        break;
                    case 9:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}