using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Day02MultTempConv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertTemperature()
        {
            if (string.IsNullOrWhiteSpace(TbxInputName.Text))
            {

                return;
            }

            if (!double.TryParse(TbxInputName.Text, out double inputTemperature))
            {
                MessageBox.Show("Invalid input temperature");
                return;
            }


            double outputTemperature;
            if (RbnInputCelsius.IsChecked == true && RbnOutputCelsius.IsChecked == true)
            {
                outputTemperature = inputTemperature;
            }
            else if (RbnInputCelsius.IsChecked == true && RbnOutputFahrenheit.IsChecked == true)
            {
                outputTemperature = Math.Round((inputTemperature * 9 / 5) + 32, 2);
            }
            else if (RbnInputCelsius.IsChecked == true && RbnOutputKelvin.IsChecked == true)
            {
                outputTemperature = inputTemperature + 273.15;
            }
            else if (RbnInputFahrenheit.IsChecked == true && RbnOutputCelsius.IsChecked == true)
            {
                outputTemperature = Math.Round((inputTemperature - 32) * 5 / 9, 2);
            }
            else if (RbnInputFahrenheit.IsChecked == true && RbnOutputFahrenheit.IsChecked == true)
            {
                outputTemperature = inputTemperature;
            }
            else if (RbnInputFahrenheit.IsChecked == true && RbnOutputKelvin.IsChecked == true)
            {
                outputTemperature = Math.Round((inputTemperature - 32) * 5 / 9 + 273.15, 2);
            }
            else if (RbnInputKelvin.IsChecked == true && RbnOutputCelsius.IsChecked == true)
            {
                outputTemperature = inputTemperature - 273.15;
            }
            else if (RbnInputKelvin.IsChecked == true && RbnOutputFahrenheit.IsChecked == true)
            {
                outputTemperature = Math.Round((inputTemperature - 273.15) * 9 / 5 + 32, 2);
            }
            else if (RbnInputKelvin.IsChecked == true && RbnOutputKelvin.IsChecked == true)
            {
                outputTemperature = inputTemperature;
            }
            else
            {
                MessageBox.Show("Invalid conversion");
                return;
            }

            TbxOutputName.Text = outputTemperature.ToString();
        }

        private void RbnInputCelsius_Checked(object sender, RoutedEventArgs e)
        {
            ConvertTemperature();
        }

        private void RbnInputFahrenheit_Checked(object sender, RoutedEventArgs e)
        {
            ConvertTemperature();
        }

        private void RbnInputKelvin_Checked(object sender, RoutedEventArgs e)
        {
            ConvertTemperature();
        }

        private void RbnOutputCelsius_Checked(object sender, RoutedEventArgs e)
        {
            ConvertTemperature();
        }

        private void RbnOutputFahrenheit_Checked(object sender, RoutedEventArgs e)
        {
            ConvertTemperature();
        }

        private void RbnOutputKelvin_Checked(object sender, RoutedEventArgs e)
        {
            ConvertTemperature();
        }
    }
}
