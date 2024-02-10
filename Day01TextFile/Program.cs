using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01TextFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string filePath
                = @"..\..\data.txt";

            Console.Write("What is your name? ");
            string name = Console.ReadLine();

            // Approach 1: one-step write all
            try
            { // write an array of strings
                string[] namesArray = { name, name, name };
                File.WriteAllLines(filePath, namesArray); // SystemException (8 child types)
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error writing to file: " + ex.Message);
            }
            
            // Approach 2: Java-like, open-write-close in steps
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // Write a line of text
                    sw.WriteLine(name);
                    sw.WriteLine(name);
                    sw.WriteLine(name);
                } /*
                finally

                {
                    if (StreamWriter != null) { sw.Close(); }
                } */
            }
            /*
            catch (NotSupportedException ex)
            {
                Console.WriteLine("NSE: " + ex.Message);
            } */
            catch (SystemException ex)
            {
                Console.WriteLine("File writing exception: " + ex.Message); // + " : " + ex.GetType().FullName);
            }


            // READ IT
            try
            {
                {
                    // most common way
                    string[] linesArray = File.ReadAllLines(filePath); // ex
                    foreach (string line in linesArray)
                    {
                        Console.WriteLine(line);
                    }
                }
                {
                    string allContent = File.ReadAllText(filePath); // ex
                    Console.WriteLine(allContent);
                }
            }
            catch (SystemException ex) // (IOException ex)
            {
                Console.WriteLine("Error writing to file: " + ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
