using System;
using System.Collections.Generic;
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

        private static void SaveAllPeopleToFile()
        {
            // throw new NotImplementedException();
        }

        private static void FindPersonYoungerThan()
        {
            throw new NotImplementedException();
        }

        private static void FindPersonByName()
        {
            throw new NotImplementedException();
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

        private static void ReadAllPeopleFromFile()
        {
            // throw new NotImplementedException();
        }
    }
}
