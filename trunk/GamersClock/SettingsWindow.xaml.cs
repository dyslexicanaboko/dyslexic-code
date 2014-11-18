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
using System.Windows.Shapes;

namespace GamersClock
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsData _initialData;

        //Public event that is used to tie this Form's instance to an external event handler
        /// <summary>
        /// Event that will be raised every time a change is Applied from this form.
        /// </summary>
        public event FormatApplied Applied;

        //Public event delegate format that must be supplied when binding to this Form's Applied event
        /// <summary>
        /// Data provided when the Applied event is raised. Contains the changes that were made from this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">This form's changes supplied to the subscriber</param>
        public delegate void FormatApplied(object sender, SettingsDataArgs e);

        public SettingsWindow(SettingsData data)
        {
            InitializeComponent();

            //Load the incoming (existing) settings from the main form
            LoadSettingsData(data);
        }

        /// <summary>
        /// Setup and configure this form's controls using the provided settings
        /// </summary>
        /// <param name="data">Settings to use when setting up this form</param>
        public void LoadSettingsData(SettingsData data)
        {
            //Keep the existing settings so that rollbacks can be performed via the Revert Buttons.
            _initialData = data;

            //Setup the Format tab
            SetDateTimeFormat(data.DateTimeFormat);

            //Setup the Colors tab
            SetColors(data.Foreground, data.Background);

            //Setup the Font tab
            SetListBoxFont(data.Font);

            //Put focus on the appropriate tab via index
            tcSettings.SelectedIndex = data.TabIndex;
        }

        #region Format Tab
        /// <summary>
        /// Get the user entered Date and Time formats and return as a new Format object
        /// </summary>
        /// <returns>New Format object</returns>
        private TimeTracker.Format GetEnteredFormat()
        {
            return new TimeTracker.Format()
            {
                DateFormat = txtDateFormat.Text,
                TimeFormat = txtTimeFormat.Text,
            };
        }

        /// <summary>
        /// Apply the user supplied Date and Time formats to the Main Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFormatApply_Click(object sender, RoutedEventArgs e)
        {
            SettingsData data = new SettingsData();
            data.DateTimeFormat = GetEnteredFormat();

            Applied(sender, new SettingsDataArgs() { NewSettings = data });
        }

        /// <summary>
        /// Use the default Date and Time format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFormatDefault_Click(object sender, RoutedEventArgs e)
        {
            SetDateTimeFormat(TimeTracker.GetDefaultFormat());
        }

        /// <summary>
        /// Revert the user's current Date and Time format choice back to the Main Form's current Date and Time formats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFormatRevert_Click(object sender, RoutedEventArgs e)
        {
            SetDateTimeFormat(_initialData.DateTimeFormat);
        }

        /// <summary>
        /// Every time the user enters a new character into the Date Format TextBox update the Date Format preview Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDateFormat_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFormatPreviewLabel(txtDateFormat, lblDatePreview);
        }

        /// <summary>
        /// Every time the user enters a new character into the Time Format TextBox update the Time Format preview Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTimeFormat_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFormatPreviewLabel(txtTimeFormat, lblTimePreview);
        }

        /// <summary>
        /// Set the Date and Time format text boxes and preview labels accordingly for the Format Tab
        /// using the provided format
        /// </summary>
        /// <param name="format">Target formats</param>
        private void SetDateTimeFormat(TimeTracker.Format format)
        {
            txtDateFormat.Text = format.DateFormat;
            txtTimeFormat.Text = format.TimeFormat;

            SetFormatPreviewLabel(txtDateFormat, lblFontDatePreview);
            SetFormatPreviewLabel(txtTimeFormat, lblFontTimePreview);
        }

        /// <summary>
        /// Safely set the Format Preview Label's content with the provided TextBox text.
        /// If the provided format is not a valid DateTime format, the user's entered text is shown instead.
        /// </summary>
        /// <param name="target">Target text box with format text</param>
        /// <param name="previewLabel">Target label to show format result or format text</param>
        private void SetFormatPreviewLabel(TextBox target, Label previewLabel)
        {
            string strFormat;

            try
            {
                //Attempt to show the current DateTime using the provided string format
                strFormat = DateTime.Now.ToString(target.Text);
            }
            catch
            {
                //If there are ANY failure revert to the user's entered format
                strFormat = target.Text;
            }

            //Set the preview label's content
            previewLabel.Content = strFormat;
        }
        #endregion

        #region Color Tab
        /// <summary>
        /// Apply the user supplied Color Choices to the Main Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorApply_Click(object sender, RoutedEventArgs e)
        {
            SettingsData data = new SettingsData();
            data.Background = new SolidColorBrush(cpBackgroundColor.SelectedColor);
            data.Foreground = new SolidColorBrush(cpForegroundColor.SelectedColor);

            Applied(sender, new SettingsDataArgs() { NewSettings = data });
        }
        
        /// <summary>
        /// Revert the user's current Color choices back to the Main Form's current Colors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorRevert_Click(object sender, RoutedEventArgs e)
        {
            SetColors(_initialData.Foreground, _initialData.Background);
        }

        /// <summary>
        /// Use the default Colors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorDefault_Click(object sender, RoutedEventArgs e)
        {
            SetColors(Brushes.Black, Brushes.White);
        }

        /// <summary>
        /// Sometimes the color names are too long, therefore when the user hovers over this color
        /// picker - just show the current color selection's name in the tool tip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cpBackgroundColor_MouseEnter(object sender, MouseEventArgs e)
        {
            cpBackgroundColor.ToolTip = cpBackgroundColor.SelectedColorText;
        }

        /// <summary>
        /// Sometimes the color names are too long, therefore when the user hovers over this color
        /// picker - just show the current color selection's name in the tool tip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cpForegroundColor_MouseEnter(object sender, MouseEventArgs e)
        {
            cpForegroundColor.ToolTip = cpForegroundColor.SelectedColorText;
        }

        /// <summary>
        /// Set the Color Pickers with the provided Colors
        /// </summary>
        /// <param name="foreground">Color for the foreground</param>
        /// <param name="background">Color for the background</param>
        private void SetColors(Brush foreground, Brush background)
        {
            cpBackgroundColor.SelectedColor = (background as SolidColorBrush).Color;
            cpForegroundColor.SelectedColor = (foreground as SolidColorBrush).Color;
        }
        #endregion

        #region Font Tab
        /// <summary>
        /// Set the selected list box item to the provided font
        /// </summary>
        /// <param name="font">Target font to set as the selected item</param>
        private void SetListBoxFont(FontFamily font)
        {
            lbFonts.SelectedItem = font; //Set the target font
            lbFonts.ScrollIntoView(lbFonts.SelectedItem); //Scroll to that font

            SetFontPreviewLabels(font); //Set the preview labels

            lbFonts.Focus(); //Finally put the List Box into focus
        }

        /// <summary>
        /// Update the preview labels with the provided font
        /// </summary>
        /// <param name="font">Target font</param>
        private void SetFontPreviewLabels(FontFamily font)
        {
            lblFontDatePreview.FontFamily = font;
            lblFontTimePreview.FontFamily = font;
        }

        /// <summary>
        /// Every time the user selects a new Font from the List Box, preview the change on the labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFontPreviewLabels((FontFamily)lbFonts.SelectedItem);
        }

        /// <summary>
        /// Use the default Font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFontDefault_Click(object sender, RoutedEventArgs e)
        {
            SetListBoxFont(SettingsData.DefaultFont);
        }

        /// <summary>
        /// Revert the user's current Font choice back to the Main Form's current Font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFontRevert_Click(object sender, RoutedEventArgs e)
        {
            SetListBoxFont(_initialData.Font);
        }

        /// <summary>
        /// Apply the user supplied Font choice to the Main Form when the Apply Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFontApply_Click(object sender, RoutedEventArgs e)
        {
            ApplyFontSettings(sender);
        }

        /// <summary>
        /// Apply the user supplied Font choice to the Main Form when the List Box Item is double Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbFonts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ApplyFontSettings(sender);
        }

        /// <summary>
        /// Apply the user supplied Font choice to the Main Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyFontSettings(object sender)
        {
            SettingsData data = new SettingsData();
            data.Font = (FontFamily)lbFonts.SelectedItem;

            Applied(sender, new SettingsDataArgs() { NewSettings = data });
        }
        #endregion
    }

    public class SettingsDataArgs : EventArgs
    {
        public SettingsData NewSettings { get; set; }
    }
}
