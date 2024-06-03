using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    public class PaymentCard
    {
        private double balance;

        public PaymentCard(double openingBalance)
        {
            balance = openingBalance;
        }

        public void AddMoney(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                if (balance > 150)
                    balance = 150;
            }
        }

        public void EatLunch()
        {
            if (balance >= 10.60)
                balance -= 10.60;
        }

        public void DrinkCoffee()
        {
            if (balance >= 2.0)
                balance -= 2.0;
        }

        public override string ToString()
        {
            return $"The card has a balance of {balance} euros";
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public int Pages { get; set; }
        public int PublicationYear { get; set; }

        public Book(string title, int pages, int publicationYear)
        {
            Title = title;
            Pages = pages;
            PublicationYear = publicationYear;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Test PaymentCard class
            PaymentCard card = new PaymentCard(50);
            Console.WriteLine(card);
            card.EatLunch();
            Console.WriteLine(card);
            card.DrinkCoffee();
            Console.WriteLine(card);

            card = new PaymentCard(10);
            Console.WriteLine(card);
            card.EatLunch();
            Console.WriteLine(card);
            card.DrinkCoffee();
            Console.WriteLine(card);

            card = new PaymentCard(100);
            Console.WriteLine(card);
            card.AddMoney(49.99);
            Console.WriteLine(card);
            card.AddMoney(10000.0);
            Console.WriteLine(card);
            card.AddMoney(-10);
            Console.WriteLine(card);

            // Test Book class
            List<Book> books = new List<Book>();
            string input;
            do
            {
                Console.Write("Name: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                    break;

                Console.Write("Pages: ");
                int pages = int.Parse(Console.ReadLine());

                Console.Write("Publication year: ");
                int year = int.Parse(Console.ReadLine());

                books.Add(new Book(title, pages, year));

                Console.Write("Name: ");
                input = Console.ReadLine();
            } while (!string.IsNullOrWhiteSpace(input));

            Console.WriteLine("What information will be printed? (everything/title)");
            string option = Console.ReadLine();

            if (option == "everything")
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title}, {book.Pages} pages, {book.PublicationYear}");
                }
            }
            else if (option == "title")
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                }
            }

            // Write books information to a file
            WriteBooksToFile(books, "books.csv");

            // Read books information from the file
            List<Book> booksFromFile = ReadBooksFromFile("books.csv");
            foreach (var book in booksFromFile)
            {
                Console.WriteLine($"{book.Title}, {book.Pages} pages, {book.PublicationYear}");
            }
        }

        static void WriteBooksToFile(List<Book> books, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Title},{book.Pages},{book.PublicationYear}");
                }
            }
        }

        static List<Book> ReadBooksFromFile(string fileName)
        {
            List<Book> books = new List<Book>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string title = parts[0];
                    int pages = int.Parse(parts[1]);
                    int year = int.Parse(parts[2]);
                    books.Add(new Book(title, pages, year));
                }
            }
            return books;
        }
    }

}
