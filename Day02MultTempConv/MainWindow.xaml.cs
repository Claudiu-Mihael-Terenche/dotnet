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

        private void TempConv()

        {
            if (string.IsNullOrEmpty(TbxInput.Text)){ return;}
            if (!double.TryParse(TbxInput.Text, out double inputTemperature))
            {
                MessageBox.Show("Invalid input temperature");
                
                return;
            }
            


            if (RbnInputCelsius.IsChecked == true && RbnOutputFahrenheit.IsChecked == true)
            {
                TbxOutput.Text = (inputTemperature + 273).ToString();
            } else if (RbnInputCelsius.IsChecked ==true  &&)
            /*
             if (string.IsNullOrEmpty(TbxInput.Text)){ return;}
               if (!double.TryParse(TbxInput.Text, out double inputTemperature))
               {
                   MessageBox.Show("Invalid input temperature");
                   
                   return;
               }
               if ( inputTemperature < -273.15) { MessageBox.Show("Temperature cannot be smaller than 0 Kelvin"); }
               
               
               if (RbnInputCelsius.IsChecked == true && RbnOutputFahrenheit.IsChecked == true)
               {
                   TbxOutput.Text = (inputTemperature + 273).ToString();
               } else if (R)
             */
        }

        

        private void RbnOutputFahrenheit_Checked(object sender, RoutedEventArgs e)
        {
            TempConv();
        }

        private void RbnInputCelsius_Checked(object sender, RoutedEventArgs e)
        {
            TempConv();
        }

        private void RbnOutputKelvin_Checked(object sender, RoutedEventArgs e)
        {
            TempConv();
        }
    }
}
