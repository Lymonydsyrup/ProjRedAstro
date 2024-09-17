// Benz Lenard Culanggo, RGB Techno, Sprint 01
// Date: 12/09/2024
// Version: 2.02
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sprint1ProjRGBTechno
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ExampleFont();
            ClockInitializer(clock);
            ClockInitializer(clock2);
            //Random number from 10-90 as an example
            Random rand = new Random();
            for (int i = 0; i < 24; i++)
            {
                int rInt = rand.Next(10, 90);
                neoArray[i] = rInt;
                lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
            }

            Array.Clear(neoArray);
            //Disabling Edit and Search Function until data is sorted
            DisableFunction();

            //Adding items on combobox
            for (int i = 0; i < max; i++)
            {
                timeselection.Items.Add(new ComboBoxItem() { Content = clock[i] });
            }
        }
        #region Constant Values & Arrays
        //NeoData Value and Array
        const int max = 24;
        int[] neoArray = new int[max];
        int[] neoArray2 = new int[max];


        //Timestamp clock values: 12hour-clock
        const int timemax = 24;
        object[] clock = new object[timemax];


        #region Methods
        //Display the data and time to listbox
        public void DisplayData(object[] time, object[] data, int max)
        {
            for (int i = 0; i < max; i++)
            {
                lboxDisplay.Items.Add(time[i] + ": " + data[i]);
            }
        }

        //2D Aray Method to combine 2 arrays
        public static object[,] CombineArrayAsRows(int[] array1, object[] array2)
        {
            //Exception if arrays are not the same length
            if (array1.Length != array2.Length)
            {
                throw new InvalidOperationException("Arrays must be the same length");
            };

            //Creating 2 arrays as rows
            object[,] combineArray = new object[2, array1.Length];

            for (int i = 0; i < array1.Length; i++)
            {
                combineArray[0, i] = array1[i];
                combineArray[1, i] = array2[i];
            }

            return combineArray;
        }

        //Clear Array
        public void ClearArrayFromPtr(int[] array, int max, int ptr)
        {
            for (int i = ptr; i < max; i++)
            {
                array[i] = 0;
            }
        }

        //Initializing clock values: 12hour-clock
        public void ClockInitializer(object[] clock)
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
        #region Ease of use

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

                // Update frequency list if new maxCount found
                if (count > maxCount)
                {
                    maxCount = count;
                    Array.Clear(frequency); // Clear previous modes
                    freqindex = 0;
                    frequency[freqindex] = array[i];
                    freqindex++;
                }
                // Add to frequency list if current count matches maxCount
                else if (count == maxCount && !frequency.Contains(array[i]))
                {
                    frequency[freqindex] = array[i];
                    freqindex++;
                }
            }

            Array.Resize(ref frequency, freqindex);
            return frequency;
        }

        public static double FindAverage(int[] array)
        {
            double sum = 0;
            double count = 0;

            for (int i = 0;i < array.Length;i++)
            {
                sum +=array[i];
                if (!(array[i] == 0))
                {
                    count++;
                }
            }
            double average = sum / count++;
            double roundedaverage = Math.Round(average, 2);
            return roundedaverage;
        }

        public static double FindRange(int[] array)
        {
            //Low and high numbers intialized
            double low = array[0];
            double high = array[0];

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < low)
                {
                    low = array[i];
                }
                if (array[i] > high)
                {
                    high = array[i];
                }
            }

            double result = (high - low);
            double roundedresult = Math.Round(result, 2);
            return roundedresult;
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
                        neoArray2[ptr] = x;
                        lboxmax = ptr + 1;

                        //Combining clock array with user input to be displayed on successful parse
                        object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

                        for (int i = 0; i < lboxmax; i++)
                        {
                            data[i] = combinedArray[0, i];
                            time[i] = combinedArray[1, i];
                        }

                        DisplayData(time, data, lboxmax);
                        ptr++;

                    }

                    //Error Message if array is full
                    else
                    {
                        MessageBox.Show("Reached max amount of items. Can't Input Anymore!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        //Still Display the current array
                        DisplayData(time, data, lboxmax - 1);
                    }
                    break;
                //Error message if input value is not an integer
                default:
                    MessageBox.Show("Value entered is not a number or empty. Please input a number!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            neoInput.Clear();

        private void resetbutton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer(clock);
            Array.Clear(neoArray);
            Array.Clear(data);
            Array.Clear(time);
            Array.Clear(combinedarray);
            ptr = 0;
        }
        #endregion

        #region Simulate Random Data 
        private void randDataButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer();
            DefaultFont();
            modebutton.IsEnabled = false;
            Array.Clear(neoArray);
            Array.Clear(clock);
            Array.Clear(data);
            Array.Clear(time);
            Array.Clear(combinedarray);
            ClockInitializer(clock);

            //Random number from 10-90 as an example
            Random rand = new Random();
            for (int i = 0; i < max; i++)
            {
                int rInt = rand.Next(10, 90);
                neoArray[i] = rInt;
                neoArray2[i] = rInt;
            }
            //Combining clock array with user input to be displayed on successful parse
            object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

            if (ptr == max)
            {
                data[i] = combinedArray[0, i];
                time[i] = combinedArray[1, i];
            }

            DisplayData(time, data, max);
            ptr = 0;
        }

        private void fillbutton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer(clock);
            DefaultFont();
            if (!(neoArray[0] == 0))
            {
                //Random number from 10-90 as an example
                Random rand = new Random();
                for (int i = ptr; i < 24; i++)
                {
                    int rInt = rand.Next(10, 90);
                    neoArray[i] = rInt;
                    neoArray2[i] = rInt;
                }

                //Display Data
                object[,] combinedArray = CombineArrayAsRows(neoArray2, clock2);

                for (int i = ptr; i < max; i++)
                {
                    lboxDisplay.Items.Add(clock[j] + ": " + neoArray[j]);
                }
            }

        }
        #endregion

        #region Bubble Sort
        private void lboxDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editValue.IsEnabled = true;
            editValueButton.IsEnabled = true;

        }
        //Storing combobox selected item to be called later
        string itemselected;
        private void bblSortChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemselected = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            smethodlabel.Text = "By " + ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            DisableFunction();
            if (itemselected == "Hour")
            {
                DisableFunction();
                timeselection.IsEnabled = false;
                
                timeselection.Visibility = Visibility.Visible;
            }
            else
            {
                DisableFunction();
                timeselection.IsEnabled = false;
                timeselection.Visibility = Visibility.Hidden;
            }
        }

        private void bblsortButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            timeselection.IsEnabled = true;
            
            DefaultFont();
            ClockInitializer();
            DisableFunction();
            //Sorting data based on ComboBox Selection
            if (neoArray != null)
            {
                if (itemselected == "Data")
                {
                    //Bubble Sort Algo
                    BubbleSortWithClock(neoArray, clock);
                    modebutton.IsEnabled = true;
                    //Display newly sorted array
                    object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

                    for (int i = 0; i < max; i++)
                    {
                        data[i] = combinedArray[0, i];
                        time[i] = combinedArray[1, i];
                    }
                    DisplayData(time, data, max);

                    // Enable Search function
                    searchValue.IsEnabled = true;
                    BinarysearchButton.IsEnabled = true;
                    seqsearchbutton.IsEnabled = true;
                }
                else
                {
                    ClockInitializer(clock2);
                    //Display newly sorted array
                    object[,] combinedArray = CombineArrayAsRows(neoArray2, clock2);

                    for (int i = 0; i < max; i++)
                    {
                        data[i] = combinedArray[0, i];
                        time[i] = combinedArray[1, i];
                    }
                    DisplayData(time, data, max);
                }
            }
            else
            {
                MessageBox.Show("No value input yet. Please Input a value first!", "Input Failure!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
        #endregion // Problems with sorting and editing array

        #region Binary Search & Sequential Search

        //Binary Search
        string timeselected;
        private void timeselection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timeselected = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            string[] clockstring = new string[max];
            for (int i = 0; i < max; i++)
            {
                clockstring[i] = clock2[i].ToString();
            }

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
            int result;

            bool success = Int32.TryParse(editValue.Text, out result);
            if (success)
            {
                // Find the index of the selected item
                int index = lboxDisplay.Items.IndexOf(selectedItem);


                //try catch method to catch exception error of editing value after search function is clicked 
                try
                {
                    // Editing the value

                    for (int i = 0; i < neoArray.Length; i++)
                    {
                        for (int j = 0; i < neoArray2.Length; j++)
                        {
                            if (neoArray[i] == neoArray2[i])
                            {
                                neoArray2[i] = result;
                                break;
                            }
                        }
                    }
                    neoArray[index] = result;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("No item selected to be edited! Select item first.", "Failed Search", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                lboxDisplay.Items.Clear();
                //Display newly sorted array
                object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

                for (int i = 0; i < max; i++)
                {
                    data[i] = combinedArray[0, i];
                    time[i] = combinedArray[1, i];
                }
                DisplayData(time, data, max);
            }
            else
            {
                MessageBox.Show("No input or invalid value input!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            editValue.Clear();



        }


        #endregion

        #region Math Calculations
        private void midexbutton_Click(object sender, RoutedEventArgs e)
        {
            if (neoArray != null)
            {
                //Mid-extreme Calculations
                double result = MidExtreme(neoArray);
                MessageBox.Show($"Mid Extreme: {result:F2}", "Mid-Extreme", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }
            else
            {
                // Find the index of the selected item
                int index = lboxDisplay.Items.IndexOf(selectedItem);

        private void modebutton_Click(object sender, RoutedEventArgs e)
        {
            int[] modes = ModeFunction(neoArray);
            double[] convertedmodes = ConvertAndRound(modes);
            string modesText = string.Join(", ", convertedmodes.Select(mode => mode.ToString("F2")));
            MessageBox.Show($"Modes: {modesText}" , "Modes");
        }

        private void averagebutton_Click(object sender, RoutedEventArgs e)
        {
            //Mid-extreme Calculations
            double result = Convert.ToDouble(FindAverage(neoArray));
            MessageBox.Show($"Average: {result:F2}", "Average!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #endregion

        private void rangebutton_Click(object sender, RoutedEventArgs e)
        {
            double result = Convert.ToDouble(FindRange(neoArray));
            MessageBox.Show($"Range: {result:F2}", "Range", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
   
