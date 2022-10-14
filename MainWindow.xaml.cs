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
using System.IO;
using Microsoft.Win32;

namespace TrackingSRTGenerator
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

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            double howManySeconds = double.Parse(length_txt.Text);
            double stepSize = double.Parse(step_txt.Text);
            string prefix = prefix_txt.Text;

            string theFile = "";

            int index = 1;
            for(double seconds = 0; seconds<howManySeconds; seconds += stepSize)
            {
                TimeSpan timeFormatter = TimeSpan.FromSeconds(seconds);
                TimeSpan timeFormatterPlusStep = TimeSpan.FromSeconds(seconds+stepSize);

                theFile += index + "\n" + timeFormatter.Hours.ToString("00") + ":" + timeFormatter.Minutes.ToString("00") + ":" + timeFormatter.Seconds.ToString("00") + "," + timeFormatter.Milliseconds.ToString("000")
                    + " --> "
                    + timeFormatterPlusStep.Hours.ToString("00") + ":" + timeFormatterPlusStep.Minutes.ToString("00") + ":" + timeFormatterPlusStep.Seconds.ToString("00") + "," + timeFormatterPlusStep.Milliseconds.ToString("000")
                    + "\n"
                    + prefix + timeFormatter.Hours.ToString("00") + ":" + timeFormatter.Minutes.ToString("00") + ":" + timeFormatter.Seconds.ToString("00") + ":" + (timeFormatter.Milliseconds/10).ToString("00")
                    + "\n\n";
                index++;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SubRip subtitle file (.srt)|*.srt";
            sfd.FileName = "tracker.srt";
            if(sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, theFile);
                MessageBox.Show("done.");
            }
        }
    }
}
