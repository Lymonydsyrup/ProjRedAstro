// Benz Lenard Culanggo, RGB Techno, Sprint 01
// Date: 12/09/2024
// Version: 2.03
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
using System.Linq;
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

            //Random number from 10-90 as an example
            Random rand = new Random();
            for (int i = 0; i < 24; i++)
            {
                int rInt = rand.Next(10, 90);
                neoArray[i] = rInt;
                lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
            }
            //Clearing the sample data for user input
            Array.Clear(neoArray);
            //Disabling Edit and Search Function until data is sorted
            DisableFunction();

            //Adding items on combobox
            for (int i = 0; i < max; i++)
            {
                timeselection.Items.Add(new ComboBoxItem() { Content = clock[i] });
            }
        }

        #region Constant Values, Arrays and Dictionary
        
        //NeoData Value, Array and Dictionary
        const int max = 24;
        int[] neoArray = new int[max];
        Dictionary<string, int> clockData = new();


        //Timestamp clock values: 12hour-clock
        const int timemax = 24;
        object[] clock = new object[timemax];

        // Display arrays for timestamp and user input
        object[] data = new object[max];
        object[] time = new object[max];
        object[,] combinedarray = new object[1, 24];
        #endregion

        //Methods, aside from Algorithms, that will be used in this app
        #region Methods

        //Display the data and time to listbox
        public void DisplayData(object[] time, object[] data, int max)
        {
            for (int i = 0; i < max; i++)
            {
                lboxDisplay.Items.Add(time[i] + " : " + data[i]);
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

        //Clearing Array based on the last input of the user
        //For readability and allows me to show input data that are in the array and not zeroes
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
            BinarysearchButton.IsEnabled = false;
            seqsearchbutton.IsEnabled = false;
            modebutton.IsEnabled = false;
            fillbutton.IsEnabled = false;

        }
        #endregion
        #endregion

        #region Search and Sorting Algo Methods
        //Bubble Sort Method for data together with the clock
        //Its for pairing the changes maid on array
        //If the neoArray(data) is sorted, so is the clock to match and pair them on display
        public void BubbleSort(int[] array, object[] clock)
        {
            //Bubble Sort Algo
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = 0; j < max - 1 - i; j++)
                {

                    if (array[j] > array[j + 1])
                    {
                        //Switches the array if the number is greater
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        //Match the the switch with the data 
                        object tempClock = clock[j];
                        clock[j] = clock[j + 1];
                        clock[j + 1] = tempClock;

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

        // Sequential Search Method
        public static int LinearSearch(int[] numbers, int target)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == target)
                {
                    return i;
                }
            }
            return -1;
        }

        //Method when searching through the array if the target of search is clock
        public void LinearSearchHour(string[] clock, string target)
        {
            for (int i = 0; i < clock.Length; i++)
            {
                if (clock[i] == target)
                {
                    var items = lboxDisplay.Items
                                .OfType<ListBoxItem>()
                                .Select(item => item.Content.ToString())
                                .ToList();

                    // Select the found item
                    lboxDisplay.SelectedIndex = i;
                    // Scroll to the selected item
                    lboxDisplay.ScrollIntoView(lboxDisplay.SelectedItem);
                }
            }
        }
        #endregion

        #region Mathematical Calculations

        //Converstion and Rounding Up Numbers
        static double[] ConvertAndRound(int[] intArray)
        {
            // Create a new array to hold the rounded double values
            double[] roundedArray = new double[intArray.Length];

            // Convert and round each integer value
            for (int i = 0; i < intArray.Length; i++)
            {
                // Convert integer to double and round to two decimal places
                roundedArray[i] = Math.Round((double)intArray[i], 2);
            }

            return roundedArray;
        }


        //Mid Extreme Method
        public static double MidExtreme(int[] array)
        {
            //Low and high numbers intialized
            double low = array[0];
            double high = array[0];

            //Loops through the array
            //Swaps the low and high if each of the conditional statement are true
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

            //Mid Extreme Calculation
            double result = (high - low) / 2;
            //Rounding up the result of calculation
            double roundedresult = Math.Round(result, 2);
            return roundedresult;
        }

        //Mode Calculation
        public static int[] ModeFunction(int[] array)
        {
            // Handle empty array case
            if (array.Length == 0)
                throw new InvalidOperationException("Cannot find modes of an empty array.");

            //Counters and Arrays to hold the most frequent numbers
            int maxCount = 0;
            int[] frequency = new int[array.Length];
            int freqindex = 0;

            // Iterate through each number in the array
            for (int i = 0; i < array.Length; i++)
            {
                int count = 0;

                // Count occurrences of the current number
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] == array[i])
                    {
                        count++;
                    }
                }

                // Update frequency list if new maxCount found
                if (count > maxCount)
                {
                    maxCount = count;
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

            // Resize frequency array to contain only found modes 
            Array.Resize(ref frequency, freqindex);
            return frequency;
        }

        //Average Calculations
        public static double FindAverage(int[] array)
        {
            //Sum and count variable initialized
            double sum = 0;
            double count = 0;

            //Adding the number each time it passes through and increases the count 
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
                if (!(array[i] == 0))
                {
                    count++;
                }
            }

            //Calculations and rounding up the result
            double average = sum / count++;
            double roundedaverage = Math.Round(average, 2);
            return roundedaverage;
        }

        //Range Calculations
        public static double FindRange(int[] array)
        {
            //Low and high numbers intialized
            double low = array[0];
            double high = array[0];

            //Getting the low and high values by replacing them if the conditional statements are true
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

            //Calculations and rounding up
            double result = (high - low);
            double roundedresult = Math.Round(result, 2);
            return roundedresult;
        }
        #endregion

        //App Functions

        #region Input Button 

        //Variable and arrays
        int lboxmax;// Current total of items displayed
        int ptr = 0;// Pointer


        private void neoInputButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            DefaultFont();
            DisableFunction();
            ClockInitializer(clock);

            //Clearing Array from the last data input upto the max length of array
            ClearArrayFromPtr(neoArray, max, ptr);

            //Input parsing and display to listbox
            int x;
            bool success = int.TryParse(neoInput.Text, out x);

            switch (success)
            {
                case true:
                    // To catch error message - max items or empty/invalid input
                    if (ptr != max)
                    {
                        //Store user input to empty space of array
                        neoArray[ptr] = x;
                        lboxmax = ptr + 1;
                        clockData.Add(clock[ptr].ToString(), neoArray[ptr]);
                    }

                    //Error Message if array is full
                    else
                    {
                        MessageBox.Show("Reached max amount of items. Can't Input Anymore!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                    //Combining clock array with user input to be displayed on successful parse
                    object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

                    for (int i = 0; i < lboxmax; i++)
                    {
                        data[i] = combinedArray[0, i];
                        time[i] = combinedArray[1, i];
                    }

                    DisplayData(time, data, lboxmax);
                    ptr++;
                    break;

                //Error message if input value is not an integer
                default:
                    MessageBox.Show("Value entered is not a number or empty. Please input a number!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    //Still Display the current array
                    DisplayData(time, data, lboxmax - 1);
                    break;
            }
            fillbutton.IsEnabled = true;
            neoInput.Clear();
        }

        private void resetbutton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer(clock);

            //Example font after reset
            lboxDisplay.Items.Add("Example data. Input a value.");
            lboxDisplay.FontStyle = FontStyles.Italic;
            lboxDisplay.Foreground = new SolidColorBrush(Colors.Gray);


            // Random Data again for example on display
            Random rand = new Random();
            for (int i = 0; i < 24; i++)
            {
                int rInt = rand.Next(10, 90);
                neoArray[i] = rInt;
                lboxDisplay.Items.Add(clock[i] + ": " + neoArray[i]);
            }

            //Resetting and every array
            //Ready for new user input data
            clockData.Clear();
            Array.Clear(neoArray);
            Array.Clear(data);
            Array.Clear(time);
            Array.Clear(combinedarray);
            ptr = 0;

            
        }
        #endregion

        #region Random Data Simulation

        //Random button that randomizes the whole array between 10-90
        private void randDataButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            DefaultFont();

            //Resetting every array to prep for inserting new random data
            modebutton.IsEnabled = false;
            clockData.Clear();
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
                //Inserting the input data to the array
                int rInt = rand.Next(10, 90);
                neoArray[i] = rInt;

                //Adding the values of two arrays(clock and neoArray) into the dictionary
                //Clock as key and neoArray as value
                clockData.Add(clock[i].ToString(), neoArray[i]);
            }

            
            clock = clockData.Keys.Cast<object>().ToArray();
            clockData.Values.CopyTo(neoArray, 0);

            //Combining clock array with user input to be displayed on successful parse
            object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

            for (int i = 0; i < max; i++)
            {
                data[i] = combinedArray[0, i];
                time[i] = combinedArray[1, i];
            }

            DisplayData(time, data, max);
        }

        //Filling up the unused spaced in the array
        private void fillbutton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            ClockInitializer(clock);
            DefaultFont();

            if (!(neoArray[0] == 0))//Fill button won't perform unless there is a user input
            {
                //Randomizing the empty space of the array
                Random rand = new Random();
                for (int i = ptr; i < max; i++)
                {
                    int rInt = rand.Next(10, 90);
                    neoArray[i] = rInt;

                    //Adds the array to the dictionary 
                    clockData.Add(clock[i].ToString(), neoArray[i]);
                }

                //Copying the Dicitionary values and keys to their respective array
                clock = clockData.Keys.Cast<object>().ToArray();
                clockData.Values.CopyTo(neoArray, 0);

                //Display Data
                object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

                for (int i = ptr; i < max; i++)
                {
                    data[i] = combinedArray[0, i];
                    time[i] = combinedArray[1, i];
                }
                DisplayData(time, data, max);
            }
            else
            {
                //Error for when there is no input value yet
                MessageBox.Show("No value input yet. Please Input a value first!", "Input Failure!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            DisableFunction();
        }
        #endregion

        #region Bubble Sort
        //Enabling the edit function when an item in listbox is selected
        private void lboxDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editValue.IsEnabled = true;
            editValueButton.IsEnabled = true;

        }

        //string variable to get the selection on the combobox
        string itemselected;
        private void bblSortChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the combobox selection and convert to string
            itemselected = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();

            //Change the label text to what is the current selection
            //To make the user aware that the data is being sorted by: hour or data
            smethodlabel.Text = "By " + ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            DisableFunction();

            //Changing the Search textbox to a combobox item that has all the hours for selection
            if (itemselected == "Hour")
            {
                DisableFunction();
                timeselection.IsEnabled = true;
                timeselection.Visibility = Visibility.Visible;
            }
            //Disables and hides the clock combobox and renables search function when sorting by Data
            else
            {
                DisableFunction();
                timeselection.IsEnabled = false;
                timeselection.Visibility = Visibility.Hidden;
            }
        }

        //On Bubble Sort - Button Click Events
        private void bblsortButton_Click(object sender, RoutedEventArgs e)
        {
            lboxDisplay.Items.Clear();
            timeselection.IsEnabled = true;
            DefaultFont();
            DisableFunction();

            //Checking if the array is empty or not
            int arrayValue = 0;

            for (int i = 0; i < neoArray.Length; i++)
            {
                arrayValue = neoArray[i];
            }

            //Sorting data based on ComboBox Selection
            if (arrayValue != 0)
            {
               
                // Catching error if the array is empty
                if (itemselected != null)
                {

                    //Separating two combobox items each with separate actions/event to run

                    if (itemselected == "Data")
                    {
                        //The values has already been copied to their respective array
                        //So we can just call the Bubble sort method for both the arrays 
                        BubbleSort(neoArray, clock);
                        modebutton.IsEnabled = true;
                        
                        // Enable Search function
                        searchValue.IsEnabled = true;
                        BinarysearchButton.IsEnabled = true;
                        seqsearchbutton.IsEnabled = true;


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
                        
                        //Copy the untouched dictionary values and keys to their respective array
                        //This help in reverting back the original position of values which by the time they are stored
                        clock = clockData.Keys.Cast<object>().ToArray();
                        clockData.Values.CopyTo(neoArray, 0);

                        //Disabling Search functions 
                        searchValue.IsEnabled = false;
                        BinarysearchButton.IsEnabled = false;
                        seqsearchbutton.IsEnabled = false;

                        //Display newly sorted array
                        object[,] combinedArray = CombineArrayAsRows(neoArray, clock);

                        for (int i = 0; i < max; i++)
                        {
                            data[i] = combinedArray[0, i];
                            time[i] = combinedArray[1, i];
                        }
                        DisplayData(time, data, max);
                    }

                }

                //Error when there is no sorting method selected
                else
                {
                    MessageBox.Show("Please select a sorting type first.", "Sorting Failure!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Error event when there is no value in the array
            else
            {
                MessageBox.Show("No value input yet. Please Input a value first!", "Input Failure!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }
        #endregion 

        #region Binary Search & Sequential Search

        //Binary Search

        //String variable for the selection on the timecombobox when sorted by the hour
        string timeselected;
        private void timeselection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Getting the timeselection and converted to string
            timeselected = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            
            //Unpacking the object type clock[] arrat into a string and store it inside a new string[] array
            string[] clockstring = new string[max];
            for (int i = 0; i < max; i++)
            {
                clockstring[i] = clock[i].ToString();
            }

            //Search through the new string to look for the target selected by the user
            //This method already has an auto selection on display box when the hour selected is found
            LinearSearchHour(clockstring, timeselected);
            Array.Clear(clockstring);
        }


        //Binary Search Function
        private void BinarysearchButton_Click(object sender, RoutedEventArgs e)
        {
            //Using if statement to seperate events that happents when selection is either Data or Hour
            if (itemselected == "Data")
            {
                //Ensuring that input is int and not a string
                int x;
                bool success = int.TryParse(searchValue.Text, out x);

                //Running conditional code if parsing is successful
                switch (success)
                {
                    case true:
                        
                        int result = BinarySearch(neoArray, x);
                        //If statement when target value is found
                        if (result != -1)
                        {
                            //Message Box to confirm that the value is found
                            MessageBox.Show("Data Found!", "Successful Search", MessageBoxButton.OK, MessageBoxImage.Information);
                            
          
                            // Select the found item on List Box
                            lboxDisplay.SelectedIndex = result;
                            // Scroll to the selected item
                            lboxDisplay.ScrollIntoView(lboxDisplay.SelectedItem);


                            //Enable edit button after target is found
                            editValue.IsEnabled = true;
                            editValueButton.IsEnabled = true;
                        }
                        //Error event
                        else
                        {
                            MessageBox.Show("Fail! Data not found", "Failed Search", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        break;
                 //When the parsing is unsuccessful
                    default:
                        MessageBox.Show("Value entered is not a number or input box is empty. Please input a number!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
        }


        //Sequential Search
        private void seqsearchbutton_Click(object sender, RoutedEventArgs e)
        {
            
            //Ensuring that input is int and not a string
            int x;
            bool success = int.TryParse(searchValue.Text, out x);

            //Running conditional code if parsing is successful
            switch (success)
            {
                
                case true:
                    //If statement when target value is found
                    int result = LinearSearch(neoArray, x);

                    if (result != -1)
                    {
                        //Message Box to confirm that the value is found
                        MessageBox.Show("Data Found!", "Successful Search", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Select the found item
                        lboxDisplay.SelectedIndex = result;
                        // Scroll to the selected item
                        lboxDisplay.ScrollIntoView(lboxDisplay.SelectedItem);
                   

                        //Enable edit button after target is found
                        editValue.IsEnabled = true;
                        editValueButton.IsEnabled = true;
                    }

                    //Error event when target is not found
                    else
                    {
                        MessageBox.Show("Fail! Data not found", "Failed Search", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    break;

            }

        }
        #endregion

        #region Edit Function

        private void editValueButton_Click(object sender, RoutedEventArgs e)
        {
            int inputData;

            //Input paring to int if it can be converted to an integer
            bool success = Int32.TryParse(editValue.Text, out inputData);

            if (success)
            {
                
                //Try catch method to capture no selection error
                //When there is no selected item on display box to edit
                try
                {
                    // Find the index of the selected item
                    var selectedIndex = lboxDisplay.SelectedIndex;

                    // Editing the value
                    neoArray[selectedIndex] = inputData;
                    clockData[clock[selectedIndex].ToString()] = neoArray[selectedIndex];

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
                catch
                {
                    MessageBox.Show("No data selected on display! Select data to edit.", "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Error event when it fails to parse and the textbox is null
            else
            {
                MessageBox.Show("No input or invalid value input!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            editValue.Clear();



        }
        #endregion

        #region Math Calculations

        //Mid Extreme Calculation
        private void midexbutton_Click_1(object sender, RoutedEventArgs e)
        {
            //Check if there is value on the array 
            int arrayValue = 0;

            for (int i = 0; i < neoArray.Length; i++)
            {
                arrayValue = neoArray[i];
            }

            //If the value is present run this code
            if (arrayValue != 0)
            {
                //Mid-extreme Calculations
                double result = MidExtreme(neoArray);

                //Rounding the result value to 2 decimal places (F2) and showing it to a message box
                MessageBox.Show($"Mid Extreme: {result:F2}", "Mid-Extreme", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            //Error event when there is no value on the array
            else
            {
                MessageBox.Show("No value input yet. Please Input a value first!", "Calculations Fail!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Mode Calculations
        private void modebutton_Click_1(object sender, RoutedEventArgs e)
        {
            int[] modes = ModeFunction(neoArray);

            //Converting Int values from array and Rounding up
            double[] convertedmodes = ConvertAndRound(modes);
            string modesText = string.Join(", ", convertedmodes.Select(mode => mode.ToString("F2")));
            MessageBox.Show($"Modes: {modesText}", "Modes");
        }

        //Average Calculations
        private void averagebutton_Click_1(object sender, RoutedEventArgs e)
        {
            //Mid-extreme Calculations
            //Converting the int values from the array and rounding up
            double result = Convert.ToDouble(FindAverage(neoArray));
            MessageBox.Show($"Average: {result:F2}", "Average!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Range Calculations
        private void rangebutton_Click_1(object sender, RoutedEventArgs e)
        {
            //Converting the int values from the array and rounding up
            double result = Convert.ToDouble(FindRange(neoArray));
            MessageBox.Show($"Range: {result:F2}", "Range", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion


    }
}