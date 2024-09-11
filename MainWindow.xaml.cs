// Benz Lenard Culanggo, RGB Techno, Sprint 01
// Date: 12/09/2024
// Version: 1.0
// Astronomical Processing
// Stores neutrino data which can be edited, sorted and searched 

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sprint1ProjRGBTechno
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constant Values and Data Array
        //NeoData Value and Array
        const int max = 24;
        int[] neoArray = new int[max];
        const int timemax = 24;
        object[] clock = new object[timemax];
        
        public void ClockInitializer()
        {
            int am = 1;
            int pm = 1;
            for (int h = 0; h < timemax; h++)
            {
                if (h == 0) clock[h] = 12 + " " + "AM";
                else if (h < 12)
                {
                    clock[h] = am + " " + "AM";
                    am++;
                }
                else if (h == 12) clock[h] = 12 + " " + "PM";
                else
                {
                    clock[h] = pm + " " + "PM";
                    pm++;
                }
            }
        }
        public void ExampleFont()
        {
            lboxDisplay.Items.Add("Example data. Input a value.");
            lboxDisplay.FontStyle = FontStyles.Italic;
            lboxDisplay.Foreground = new SolidColorBrush(Colors.Gray);
        }

        public void DefaultFont()
        {
            lboxDisplay.FontStyle = FontStyles.Normal;
            lboxDisplay.Foreground = new SolidColorBrush(Colors.Black);
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            ExampleFont();
            ClockInitializer();
            //Random number from 10-90 as an example
            Random rand = new Random();
            for (int i = 0; i < 24; i++)
            {
                int rInt = rand.Next(10, 90);
                neoArray[i] = rInt;
                lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
            }

            //ComboBox Items
            bblSortChoice.Items.Add("Hour");
            bblSortChoice.Items.Add("Data");
        }

        #region UserEaseChanges

        //Default text clears on focus (mouse click) 
        private void neoInput_GotFocus(object sender, RoutedEventArgs e)
        {
            neoInput.Clear();
        }
        private void neoInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                neoInputButton_Click(sender, e);
            }
        }

        #endregion


        #region Input Button 

        //ListBox Display
        //Store input value in listbox array
        int dsplymax;
        int ptr = 0;
        private void neoInputButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer();
            DefaultFont();
            //Input parsing and display to listbox
            int x;
            bool success = int.TryParse(neoInput.Text, out x);
            switch (success)
            {
                case true:
                    if (ptr < max)
                    {
                        neoArray[ptr] = x;
                        dsplymax = ptr + 1;
                        for (int i = 0; i < dsplymax; i++)
                        {
                            lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
                        }
                        ptr++;
                    }
                    else
                    {
                        MessageBox.Show("Reached max amount of items. Can't Input Anymore!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                default:
                    MessageBox.Show("Value entered is not a number or empty. Please input a number!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            neoInput.Clear();

        }
        #endregion

        #region Simulate Random Data 
        private void randDataButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer();
            DefaultFont();
            //Random number from 10-90 as an example
            Random rand = new Random();
            for (int i = 0; i < 24; i++)
            {
                int rInt = rand.Next(10, 90);
                neoArray[i] = rInt;
                lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
            }
        }
        #endregion

        #region Bubble Sort Button
        private void bblsortButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer();
            DefaultFont();
            for (int i = 0;i < max -1;i++)
            {
                
                for (int j = 0; j < max - 1 - i; j++)
                {
                    int swaps = 0;
                    if (neoArray[j] > neoArray[j + 1])
                    {
                        
                        int temp = neoArray[j];
                        neoArray[j] = neoArray[j + 1];
                        neoArray[j + 1] = temp;
                        

                        object tempClock = clock[j];
                        clock[j] = clock[j + 1];
                        clock[j + 1] = tempClock;

                        swaps = 1;
                    }
                }
            }
            for (int i = 0; i < max-1;i++)
            {
                lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
            }

        }
        #endregion


    }
}   
