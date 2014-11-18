using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace GamersClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeTracker _source;
        private DispatcherTimer timer;
        private DateTime _lastDate;

        public MainWindow()
        {
            InitializeComponent();

            _source = new TimeTracker();

            LoadUserSettings(); //Load the user's settings

            Update(); //Run the time update the first time as a primer for all of the labels

            SetupTimer(); //Setup the timer with its events and delegates to run each second
        }

        /// <summary>
        /// Attempt to load the user's settings from the settings file. If the setting is 
        /// corrupt or doesn't exist then a default is loaded.
        /// </summary>
        private void LoadUserSettings()
        {
            try
            {
                _source.SetFormat(new TimeTracker.Format()
                {
                    DateFormat = UserSettings.Default.FormatDatePart,
                    TimeFormat = UserSettings.Default.FormatTimePart
                });
            }
            catch
            {
                _source.SetFormat(TimeTracker.GetDefaultFormat());
            }

            try
            {
                SetBackgroundColor(ConvertToBrush(UserSettings.Default.ColorBackground));
            }
            catch
            {
                SetBackgroundColor(SettingsData.DefaultBackground);
            }

            try
            {
                SetForegroundColor(ConvertToBrush(UserSettings.Default.ColorForeground));
            }
            catch
            {
                SetForegroundColor(SettingsData.DefaultForeground);
            }

            try
            {
                SetFont(new FontFamily(UserSettings.Default.DateTimeFont));
            }
            catch
            {
                SetFont(SettingsData.DefaultFont);
            }

            try
            {
                ToggleElapsedTimeLabelVisibility(UserSettings.Default.ShowElapsedTimeLabel);
            }
            catch
            {
                ToggleElapsedTimeLabelVisibility(true);
            }

            miToggleElapsed.IsChecked = lblElapsedTime.Visibility == Visibility.Visible;
        }

        /// <summary>
        /// The main delegate used to update everything related to a clock tick.
        /// All labels and statistics are updated here.
        /// </summary>
        private void Update()
        {
            //If the last date collected is less than the current date
            //then it is tomorrow. Alert the user
            if (_lastDate < _source.SourceDate.Date)
            {
                //Alert the user that it is now tomorrow
            }

            //Update the Date Label
            lblCurrentDate.Content = _source.CurrentDate;

            //Update the Time Label
            lblCurrentTime.Content = _source.CurrentTime;

            //Update the amount of time Elapsed Label
            lblElapsedTime.Content = _source.ElapsedTime.ToString(@"dd\:hh\:mm\:ss");

            //Store the last Date (Day) for comparison next time
            _lastDate = _source.SourceDate.Date;
        }
        
        /// <summary>
        /// Setting up the dispatch timer to run every second
        /// </summary>
        private void SetupTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        /// <summary>
        /// Event handler for the Dispatch Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            Update(); //Update all of the labels
        }

        /// <summary>
        /// Menu item (Menu Bar at the top of the form) click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSettings_Click(object sender, RoutedEventArgs e)
        {
            //Collect all of the Form's Current settings in preparation for the Settings Form
            SettingsData data = new SettingsData();
            data.DateTimeFormat = _source.GetCurrentFormat(); //Get the current Date/Time formats
            data.Font = lblCurrentDate.FontFamily; //The font family for one of the labels (will be used for all of the labels)
            data.Foreground = lblCurrentDate.Foreground; //The foreground for one of the labels (will be used for all of the labels)
            data.Background = this.Background; //This form's background color
            data.TabIndex = GetSettingsTabIndex(((MenuItem)sender)); //Get the destination tab index

            //Create new settings window instance
            SettingsWindow win = new SettingsWindow(data);

            //Register the settings window applied event 
            win.Applied += new SettingsWindow.FormatApplied(SettingsWindow_OnApplied);
            
            //This line both opens the dialog and when the window is closed will unregister the applied event
            if(win.ShowDialog() == true)
                win.Applied -= new SettingsWindow.FormatApplied(SettingsWindow_OnApplied);
        }

        /// <summary>
        /// Get the destination Tab Index based on the mapping defined.
        /// The mapping is Menu Item to Tab Index
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private int GetSettingsTabIndex(MenuItem target)
        {
            //Example: Date Time Format menu item is mapped to tab index zero
            if (target.Name == miDateTimeFormat.Name)
                return 0;

            if (target.Name == miColor.Name)
                return 1;

            if (target.Name == miFont.Name)
                return 2;

            return -1;
        }

        /// <summary>
        /// Event handler for when an applied button (or other control of the same nature) is invoked.
        /// New settings are sent over as an argument and are saved to the user's setting while being applied
        /// to this form's controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsWindow_OnApplied(object sender, SettingsDataArgs e)
        {
            //Each if block will test to make sure that the incoming property is not null
            //These properties do not all have to be set as they can be sent piece meal
            //Each block will set the user's settings and ultimately be saved

            //Make sure the incoming (new) setting is not null
            if (e.NewSettings.DateTimeFormat != null)
            {
                //Update the Form's control
                _source.SetFormat(e.NewSettings.DateTimeFormat);

                //Save the new setting to the user's settings file
                UserSettings.Default.FormatDatePart = e.NewSettings.DateTimeFormat.DateFormat;
                UserSettings.Default.FormatTimePart = e.NewSettings.DateTimeFormat.TimeFormat;
            }

            if (e.NewSettings.Background != null)
            {
                SetBackgroundColor(e.NewSettings.Background);
                
                UserSettings.Default.ColorBackground = e.NewSettings.Background.ToString();
            }

            if (e.NewSettings.Foreground != null)
            {
                SetForegroundColor(e.NewSettings.Foreground);
                
                UserSettings.Default.ColorForeground = e.NewSettings.Foreground.ToString();
            }

            if (e.NewSettings.Font != null)
            {
                SetFont(e.NewSettings.Font);

                UserSettings.Default.DateTimeFont = e.NewSettings.Font.ToString();
            }

            //Ultimately save the user's settings
            UserSettings.Default.Save();
        }

        /// <summary>
        /// Convert the string representation of a brush to the object equivalent
        /// </summary>
        /// <param name="brushData">Name or code of brush as text</param>
        /// <returns>object equivalent of name or code</returns>
        private Brush ConvertToBrush(string brushData)
        { 
            return (SolidColorBrush)new BrushConverter().ConvertFromString(brushData);
        }

        /// <summary>
        /// Set the background of this form to the provided brush
        /// </summary>
        /// <param name="background"></param>
        private void SetBackgroundColor(Brush background)
        {
            this.Background = background;
        }

        /// <summary>
        /// Set the foreground of all participating labels to the provided brush
        /// </summary>
        /// <param name="foreground"></param>
        private void SetForegroundColor(Brush foreground)
        { 
            lblCurrentDate.Foreground = foreground;
            lblCurrentTime.Foreground = foreground;
            lblElapsedTime.Foreground = foreground;
        }

        /// <summary>
        /// Set the Font Family of all participating labels to the provided Font Family
        /// </summary>
        /// <param name="font"></param>
        private void SetFont(FontFamily font)
        {
            lblCurrentDate.FontFamily = font;
            lblCurrentTime.FontFamily = font;
            lblElapsedTime.FontFamily = font;
        }

        /// <summary>
        /// Open the about dialogue - beg the user for money
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutGamersClock().ShowDialog();
        }

        /// <summary>
        /// This event handles both the Checked and Unchecked events from the Menu Item Toggle Elapsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miToggleElapsed_OnCheckChanged(object sender, RoutedEventArgs e)
        {
            ToggleElapsedTimeLabelVisibility(miToggleElapsed.IsChecked);

            //Save the user's choice
            UserSettings.Default.ShowElapsedTimeLabel = miToggleElapsed.IsChecked;
            UserSettings.Default.Save();
        }

        /// <summary>
        /// Toggle the visibility of the Elapsed Time label with the provided boolean
        /// </summary>
        /// <remarks>
        /// true = Elapsed time is Visible
        /// false = Elapsed time is Hidden
        /// </remarks>
        /// <param name="visible">true = Elapsed Time is visible</param>
        private void ToggleElapsedTimeLabelVisibility(bool visible)
        {
            //Toggle the Elapsed Time visibility
            lblElapsedTime.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// When the user double clicks on the elapsed time, this is to reset the elapsed time. A confirmation window is shown first.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblElapsedTime_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Reset elapsed time?", "Are you sure?", System.Windows.MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                _source.ResetElapsedTime();
        }
    }
}
