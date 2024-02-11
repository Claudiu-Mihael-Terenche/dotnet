using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01PeopleListInFile
{
    internal class Program
    {
        private const string DataFileName = @"..\..\people.txt";

        static List<Person> People = new List<Person>();

        private static int getMenuChoice()
        {
            try
            {
                Console.Write("What do you want to do?\n" + 
                              "1. Add person info\n" + 
                              "2. List persons info\n" + 
                              "3. Find and list a person by name\n" + 
                              "4. Find and list persons younger than age\n" + 
                              "0. Exit\n" + "Choice: ");
                string line = Console.ReadLine();
                int choice = int.Parse(line); // ex
                return choice;
            }
            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                return -1;
            }
        }

        static void Main(string[] args)
        {
            // Person p1 = new Person { Name = "John", Age = 33, City = "Montreal" };
            ReadAllPeopleFromFile();
            while (true)
            {
                int choice = getMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddPersonInfo();
                        break;
                    case 2:
                        ListAllPersonsInfo();
                        break;
                    case 3:
                        FindPersonByName();
                        break;
                    case 4:
                        FindPersonYoungerThan();
                        break;
                    case 0:
                        SaveAllPeopleToFile();
                        return; // exit the program
                    default:
                        Console.WriteLine("No such option, try again");
                        break;
                }
                Console.WriteLine();
            }

        }

        private static void FindPersonYoungerThan()
        {
            Console.Write("Enter maximum age: ");
            if (!int.TryParse(Console.ReadLine(), out int maxAge))
            {
                Console.WriteLine("Invalid input");
                return;
            }
            // using a foreach loop with a condition
            Console.WriteLine("People at that age or younger: ");
            foreach (Person p in People)
            {
                if (p.Age <= maxAge)
                {
                    Console.WriteLine(p);
                }
            }
            // using LINQ
            Console.WriteLine("People at that age or younger (using LINQ): ");
            var youngerList = People.Where(p => (p.Age <= maxAge)); // Method syntax
            var youngerList2 = from p in People where p.Age <= maxAge select p; // Query syntax
            foreach (Person p in youngerList)
            {
                Console.WriteLine(p);
            }
        }

        private static void FindPersonByName()
        {
            Console.Write("Enter partial person name: ");
            string searchStr = Console.ReadLine();
            var matchesList = People.Where(p => p.Name.Contains(searchStr)).ToList(); // LINQ
            if (matchesList.Count > 0)
            {
                Console.WriteLine("Matches found: ");
                foreach (Person p in matchesList)
                {
                    Console.WriteLine(p);
                }
            }
        }

        private static void ListAllPersonsInfo()
        {
            Console.WriteLine("Listing all persons");
            foreach (Person person in People)
            {
                Console.WriteLine(person);
            }
        }

        private static void AddPersonInfo()
        {
            try
            {
                Console.WriteLine("Adding a person.");
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                Console.Write("Enter age: ");
                int age = int.Parse(Console.ReadLine()); // ex. FormatException, OverflowException
                Console.Write("Enter city: ");
                string city = Console.ReadLine();
                // Person person = new Person { Name = name, Age = age, City = city };
                Person person = new Person(name, age, city); // ex ArgumentException
                People.Add(person);
                Console.WriteLine("Person added.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                Console.WriteLine("Error: invalid numerical input");
            }
        }

        private static void SaveAllPeopleToFile()
        {
            try
            {
                List<string> linesList = new List<string>();
                foreach (Person person in People)
                {
                    linesList.Add($"{person.Name}; {person.Age}; {person.City}");
                }
                File.WriteAllLines(DataFileName, linesList); // ex IOException, SystemException
            }
            // catch (Exception ex) when (ex is IOException || ex is SystemException)
            catch (SystemException ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
                throw;
            }
        }

        private static void ReadAllPeopleFromFile()
        {
            try
            {
                if (!File.Exists(DataFileName))
                {
                    Console.WriteLine("Warning: no data file found at " + DataFileName);
                    return; // it's ok if the file doesn't exist yet
                }
                string[] linesArray = File.ReadAllLines(DataFileName); // exIOException, SystemException
                foreach (string line in linesArray)
                {
                    try
                    {
                        string[] data = line.Split(';');
                        if (data.Length != 3)
                        {
                            throw new FormatException("Invalid number of items");
                            // or: Console.WriteLine("Error..."); continue;
                        }
                        string name = data[0];
                        int age = int.Parse(data[1]); // ex FormatException
                        string city = data[2];
                        Person person = new Person(name, age, city); // ex ArgumentException
                    }
                    catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
                    {
                        Console.WriteLine($"Error (skipping line): {ex.Message} in:\n {line}");
                        throw;
                    }
                }
            }
            // catch (Exception ex) when (ex is IOException || ex is SystemException)
            catch (SystemException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }
    }
}
