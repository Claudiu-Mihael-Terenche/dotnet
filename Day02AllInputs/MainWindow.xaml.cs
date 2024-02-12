using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Day02AllInputs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"people.txt";
        public MainWindow()
        {
            InitializeComponent();
            List<string> listContinents = new List<string>()
            {
                "Africa", "Antarctica", "Asia", "Australia", "Europe","North America","South America"
            };
            this.CbxContinentNames.ItemsSource = listContinents;
        }




        private class Person
        {
            public Person(string name, string age, string pets, string continent, int temp)
            {
                Name = name;
                Age = age;
                Pets = pets;
                Continent = continent;
                Temp = temp;
            }

            private string _name;

            public string Name
            {
                get { return _name; }
                //Name3-100 characters long, not containing semicolons
                set
                {

                    if (!Regex.IsMatch(value, @"^[^;]{3,100}$")) //, RegexOptions.IgnoreCase))
                    {
                        throw new ArgumentException("Name must be 3-100 characters long, no semicolons");
                    }
                    _name = value;

                }
            }



            private string _age;

            public string Age
            {
                get { return _age; }

                set
                {
                    _age = value;

                }
            }

            private string _pets;

            public string Pets
            {
                get { return _pets; }

                set
                {
                    _pets = value;

                }
            }

            private string _continent;

            public string Continent
            {
                get { return _continent; }

                set
                {
                    _continent = value;

                }
            }

            private int _temp;

            public int Temp
            {
                get { return _temp; }
                set
                {
                    _temp = value;

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(TbxName.Text))
                {
                    MessageBox.Show("Please enter a name.");
                    return;
                }

                // Get age
                string age = "";
                if (RbnBelow18Age.IsChecked == true)
                {
                    age = "Below 18";
                }
                else if (Rbn18To35Age.IsChecked == true)
                {
                    age = "18-35";
                }
                else if (Rbn36UpAge.IsChecked == true)
                {
                    age = "36+";
                }

                // Get pets
                StringBuilder pets = new StringBuilder();
                if (CbxRaccoon.IsChecked == true)
                {
                    pets.Append("raccoon, ");
                }
                if (CbxDog.IsChecked == true)
                {
                    pets.Append("dog, ");
                }
                if (CbxOther.IsChecked == true)
                {
                    pets.Append("other, ");
                }
                string allPets = pets.ToString().TrimEnd(',', ' ');

                // Get continent
                string continent = CbxContinentNames.SelectedItem?.ToString();

                // Get temperature
                int temp = (int)SldrTemp.Value;

                // Create person object
                Person person = new Person(TbxName.Text, age, allPets, continent, temp);

                // Write person info to file
                using (StreamWriter sw = File.AppendText(DataFileName))
                {
                    sw.WriteLine($"{person.Name};{person.Age};{person.Pets};{person.Continent};{person.Temp}");
                }

                MessageBox.Show("Person registered successfully.");


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering person: {ex.Message}");
            }
        }


        private void LoadSavePeopleFile()
        {
            try
            {
                // Read all lines from the file
                string[] peopleList = File.ReadAllLines(DataFileName);

                // Clear the ListView
                LsvPeople.Items.Clear();


                foreach (string people in peopleList)
                {
                    string[] peopleItem = people.Split(';');
                    LsvPeople.Items.Add(new ListViewItem { Content = string.Join("/ ", peopleItem) });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadSavePeopleFile();
        }
    }

}