using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace KIKI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Initialize();

            List<string> eventData = App.getBuffer();

            List<todayEvent> items = new List<todayEvent>();
            
            for (int i = 0; i < eventData.Count; i = i + 3)
            {
                items.Add(new todayEvent() { Time = eventData[i], Name = eventData[i+1], Attendee =  eventData[i+2]});
                mlistView.ItemsSource = items;

            }

        }

        private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        public void OnClick1(object sender, RoutedEventArgs e)
        {

            Process.Start("https://accounts.google.com/Logout");
            string credPath = System.Environment.GetFolderPath(
                      System.Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

            File.Delete(credPath);
            List<todayEvent> items2 = new List<todayEvent>();
            mlistView.ItemsSource = items2;
            App.InitializeGoogle();
        }
    }

    public class todayEvent
    {
        public string Time { get; set; }

        public string Name { get; set; }

        public string Attendee { get; set; }
    }


}
