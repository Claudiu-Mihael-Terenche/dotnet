using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World");
                Console.Write("What is your name? ");
                string nameStr = Console.ReadLine();
                Console.Write("What is your age? ");
                string ageStr = Console.ReadLine();
                int age = int.Parse(ageStr); // ex FormatException, OverFlowException
                Console.WriteLine("Hello {0}, you are {1} y/o", nameStr, ageStr);
                string greeting = $"Hello {nameStr}, you are {age} y/o";
                Console.WriteLine(greeting);
                // Console.WriteLine("Hello {0}, you are {1} y/o", name, age);
                int nameLen = nameStr.Length;

                if (nameStr.ToLower() == "santa")
                {
                    Console.WriteLine("Santa!!! Can't believe it's you!");
                }

            } catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                Console.WriteLine($"Error: you must enter an integer number ( {ex.ToString()} )");
            } finally
            {
                    Console.WriteLine("Press any key to finish");
                    Console.ReadKey();
            }

    }
    }
}
