// Benz Lenard Culanggo, RGB Techno, Sprint 01
// Date: 12/09/2024
// Version: 1.02
// Astronomical Processing
// Stores neutrino data which can be edited, sorted and searched 

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
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
        #region Constant Values, Data Arrays & Methods
        //NeoData Value and Array
        const int max = 24;
        int[] neoArray = new int[max];
        //Temporary array holder for "hour" combobox option
        int[] tempneoArray = new int[max];

        //Timestamp clock values: 12hour-clock
        const int timemax = 24;
        object[] clock = new object[timemax];


        //Initializing clock values: 12hour-clock
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
        //Font for example datastream
        public void ExampleFont()
        {
            lboxDisplay.Items.Add("Example data. Input a value.");
            lboxDisplay.FontStyle = FontStyles.Italic;
            lboxDisplay.Foreground = new SolidColorBrush(Colors.Gray);
        }
        //Change font to default
        public void DefaultFont()
        {
            lboxDisplay.FontStyle = FontStyles.Normal;
            lboxDisplay.Foreground = new SolidColorBrush(Colors.Black);
        }
        //Disable Edit & Search Function until sorted
        public void DisableFunction()
        {
            //Search elements
            searchValue.IsEnabled = false;
            searchButton.IsEnabled = false;

            //Edit elements
            editValue.IsEnabled = false;
            editValueButton.IsEnabled = false;
        }
        //Bubble Sort Method
        public void BubbleSort()
        {
            //Bubble Sort Algo
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = 0; j < max - 1 - i; j++)
                {

                    if (neoArray[j] > neoArray[j + 1])
                    {

                        int temp = neoArray[j];
                        neoArray[j] = neoArray[j + 1];
                        neoArray[j + 1] = temp;
                    }
                }
            }
        }
        //Binary Search Method
        public static int BinarySearch(int[] arr, int key)
        {
            int minNum = 0;
            int maxNum = arr.Length - 1;

            while (minNum <= maxNum)
            {
                int mid = (minNum + maxNum) / 2;
                if (key == arr[mid])
                {
                    return mid;
                }
                else if (key < arr[mid])
                {
                    maxNum = mid - 1;
                }
                else
                {
                    minNum = mid + 1;
                }
            }
            return -1;
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
            Array.Clear(neoArray, 0, neoArray.Length);
            //Disabling Edit and Search Function until data is sorted
            DisableFunction();
        }

        #region UserEaseChanges

        //Default text clears on focus (mouse click) 
        private void neoInput_GotFocus(object sender, RoutedEventArgs e)
        {
            neoInput.Clear();
        }
        //"enter" key function on textbox input
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
            //Clearing any randomize data based on the last user input
            Array.Clear(neoArray, ptr, neoArray.Length);
            Array.Clear(tempneoArray, ptr, neoArray.Length);
            //Input parsing and display to listbox
            int x;
            bool success = int.TryParse(neoInput.Text, out x);
            switch (success)
            {
                case true:
                    if (ptr < max)
                    {
                        neoArray[ptr] = x;
                        tempneoArray[ptr] = x;
                        dsplymax = ptr + 1;
                        for (int i = 0; i < dsplymax; i++)
                        {
                            lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
                        }
                        ptr++;
                    }
                    //Error Message if array is full
                    else
                    {
                        MessageBox.Show("Reached max amount of items. Can't Input Anymore!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                //Error message if input value is not an integer
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

            if (ptr == max)
            {
                MessageBox.Show("Data storage is full! No more space to randomize data!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Random number from 10-90 as an example
                Random rand = new Random();
                for (int i = ptr; i < 24; i++)
                {
                    int rInt = rand.Next(10, 90);
                    neoArray[i] = rInt;
                    tempneoArray[i] = rInt;
                }
                for (int j = 0; j < neoArray.Length; j++)
                {
                    lboxDisplay.Items.Add(clock[j] + ": " + neoArray[j]);
                }
            }

        }
        #endregion

        #region Bubble Sort
        //Storing combobox selected item to be called later
        string itemselected;
        private void bblSortChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemselected = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            smethodlabel.Text = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            DisableFunction();
        }

        private void bblsortButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            DefaultFont();
            ClockInitializer();
            DisableFunction();
            //Clearing Array according to the last user input
            
            //Sorting data based on ComboBox Selection
            if (itemselected == "Data")
            {
                //Bubble Sort Algo
                BubbleSort();
                //Display newly sorted array
                for (int i = 0; i < max; i++)
                {
                    lboxDisplay.Items.Add(neoArray[i] + " ");
                }
                // Enable Search function
                searchValue.IsEnabled = true;
                searchButton.IsEnabled = true;
            }
            else if (itemselected == "Hour")
            {
                ClockInitializer();
                for (int i = 0; i < max; i++)
                {
                    lboxDisplay.Items.Add(clock[i] + ": " + tempneoArray[i]);
                }
            }
            else
            {
                MessageBox.Show("No Sorting method selected!", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }
        #endregion

        #region Binary Search

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (itemselected == "Data")
            {
                int x;
                bool success = int.TryParse(searchValue.Text, out x);
                switch (success)
                {
                    case true:
                        int result = BinarySearch(neoArray, x);
                        if (result != -1)
                        {
                            MessageBox.Show("Data Found!", "Successful Search", MessageBoxButton.OK, MessageBoxImage.Information);

                            //Highlighting target data
                            lboxDisplay.SelectedIndex = result;
                            lboxDisplay.ScrollIntoView(lboxDisplay.Items[result]);

                            ListBoxItem item = lboxDisplay.ItemContainerGenerator.ContainerFromIndex(result) as ListBoxItem;

                            if (item != null)
                            {
                                item.Background = System.Windows.Media.Brushes.LightBlue;
                            }

                            //Enable edit button after target is found
                            editValue.IsEnabled = true;
                            editValueButton.IsEnabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Fail! Data not found", "Failed Search", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        break;
                    default:
                        MessageBox.Show("Value entered is not a number or empty. Please input a number!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            else if (itemselected == "Hour")
            {
                DisableFunction();
            }


        }
        #endregion

        #region Edit Function
        private void lboxDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            editValue.IsEnabled = true;
            editValueButton.IsEnabled = true;
        }

        private void editValueButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = lboxDisplay.SelectedItem as string;

            if (itemselected == "Data")
            {
                if (selectedItem != null)
                {
                    // Find the index of the selected item
                    int index = lboxDisplay.Items.IndexOf(selectedItem);

                    // Editing the value
                    neoArray[index] = int.Parse(editValue.Text);
                    lboxDisplay.Items.Clear();
                    BubbleSort();

                    //Display newly edited array 
                    for (int i = 0; i < max; i++)
                    {
                        lboxDisplay.Items.Add(neoArray[i]);
                    }
                }
                else
                {
                    MessageBox.Show("No input value found. Please input a value.");
                }
            }
            else
            {
                // Find the index of the selected item
                int index = lboxDisplay.Items.IndexOf(selectedItem);

                // Editing the value
                tempneoArray[index] = int.Parse(editValue.Text);
                lboxDisplay.Items.Clear();
                for (int i = 0; i < max; i++)
                {
                    lboxDisplay.Items.Add(clock[i] +": "+tempneoArray[i]);
                }
            }
           
        }
        #endregion

        
    }
}
   
