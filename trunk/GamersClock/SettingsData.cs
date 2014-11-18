using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GamersClock
{
    /// <summary>
    /// DTO to move around this applications user changeable settings
    /// </summary>
    public class SettingsData
    {
        //Default application settings
        public static Brush DefaultForeground { get { return Brushes.Black; } }
        public static Brush DefaultBackground { get { return Brushes.White; } }
        public static FontFamily DefaultFont { get { return new FontFamily("Calibri"); } }

        /// <summary>
        /// Destination Tab Index from the Main Window to the Settings Window
        /// </summary>
        public int TabIndex { get; set; }

        //These properties are all self explanatory - they are all DTO for what the application uses
        public TimeTracker.Format DateTimeFormat { get; set; }
        public FontFamily Font { get; set; } 
        public Brush Background { get; set; }
        public Brush Foreground { get; set; }
    }
}
