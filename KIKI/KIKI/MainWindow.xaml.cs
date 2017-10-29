using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Windows.Media;
using Hardcodet.Wpf.TaskbarNotification;
using System.Reflection;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.ObjectModel;
using KIKIXmlProcessor;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;

namespace KIKI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool login = true;
        private System.Timers.Timer timer;

        // initialize mainwindow and internal logic, set timer.
        public MainWindow()
        {
            XMLProcessor p = new XMLProcessor(App.id);
            InitializeComponent();
            App.Initialize();
            NotifyIcon ni = new NotifyIcon();
            ni.Icon = new System.Drawing.Icon(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/icon.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
            initializeGoogleInfo();
            initializeMeetingInfo();
            initializeFileInfo();
            initializeTimer();
    }

        // set logic for periodically refresh
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            XMLProcessor p = new XMLProcessor(App.id);
            App.meetingList = new LinkedList<MeetingNode>();
            App.fileList = new LinkedList<FileNode>();
            App.UpdateGoogleList();
            App.UpdateMeetingList();
            App.UpdateFileList();
            App.InitializeCalendar();
            App.fetchFromGoogle(p.GetLastUpdateTime());
            
            if (App.meetingList.Count > 0){
                App.UpdateCore();
                App.InitializeMeetingTab();
                App.InitializeFileTab();
            }
          
            // Update UI thread seperately
            this.Dispatcher.Invoke(() =>
            {
                initializeGoogleInfo();
                initializeFileInfo();
                initializeMeetingInfo();
            });
          
            
        }

        // Initialize Today's event module
        private void initializeGoogleInfo()
        {
            List<string> eventData = App.getGoogleBuffer();
            ObservableCollection<todayEvent> items = new ObservableCollection<todayEvent>();
            
            for (int i = 0; i < eventData.Count; i = i + 3)
            {
                items.Add(new todayEvent() { Date = eventData[i], Time = eventData[i], Name = eventData[i + 1], Attendee = eventData[i + 2] });
                mlistView.ItemsSource = items;
            }
        }

        // Initialize Meeting tab
        private void initializeMeetingInfo()
        {
            string buffer = "";
            List<string> meetingData = App.getMeetingBuffer();
            ObservableCollection<previousMeeting> items = new ObservableCollection<previousMeeting>();
            for (int i = 0; i < meetingData.Count; i = i + 5)
            {
                if(i == 0)
                {
                    buffer = meetingData[0];
                    items.Add(new previousMeeting() { Date = meetingData[i] });
                    mlistView4.ItemsSource = items;
                    items.Add(new previousMeeting() { Time = meetingData[i + 1], Name = meetingData[i + 2], Attendee = meetingData[i + 3], Docs = meetingData[i + 4] });
                    mlistView4.ItemsSource = items;
                }

                else if (i != 0)
                {
                    if (meetingData[i].Equals(buffer))
                    {
                    
                        items.Add(new previousMeeting() { Time = meetingData[i + 1], Name = meetingData[i + 2], Attendee = meetingData[i + 3], Docs = meetingData[i + 4] });
                        mlistView4.ItemsSource = items;

                    } else {
                        buffer = meetingData[i];
                        items.Add(new previousMeeting() { Date = meetingData[i] });
                        mlistView4.ItemsSource = items;
                        items.Add(new previousMeeting() { Time = meetingData[i + 1], Name = meetingData[i + 2], Attendee = meetingData[i + 3], Docs = meetingData[i + 4] });
                        mlistView4.ItemsSource = items;
                    }
                }
            }
        }

        // Initialize File tab
        private void initializeFileInfo()
        {
            XMLProcessor processor = new XMLProcessor(App.id);
            XMLSearcher searcher = new XMLSearcher(processor.GetWorkingPath(),App.id);
            List<string> FileData = App.getFileBuffer();
            LinkedList<MeetingNode> MeetingData = new LinkedList<MeetingNode>();
            ObservableCollection<recentFile> items = new ObservableCollection<recentFile>();
            for (int i = 0; i < FileData.Count; i = i + 3)
            {
                items.Add(new recentFile() { Name = FileData[i], URL = FileData[i+1], Meetings = FileData[i+2]});
                RecentFile.ItemsSource = items;
                MeetingData = searcher.FindMeetingsByMeetingIDs(FileData[i + 2]);
                
                if (MeetingData.Count != 0)
                {
                    foreach (MeetingNode item in MeetingData)
                    {
                        items.Add(new recentFile() { Time = item.GetStartTimeS(), Title = item.GetMeetingTitle(), Attendee = item.GetAttendents(), Files = item.GetFileListS()});
                        RecentFile.ItemsSource = items;
                    }
                } else {
                    items.Add(new recentFile() { Time = "No Records", Title = "  ", Attendee = "    " });
                    RecentFile.ItemsSource = items;
                } 
            }
        }

        // Initialize timer everytime refreshing
        private void initializeTimer()
        {
            int wait = 10 * 1000;
            timer = new System.Timers.Timer(wait);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();
        }

    

        // Initialize login button
        public void loginClick(object sender, RoutedEventArgs e)
        {
            if (login == true)
            {
                App.revoke();
                App.Clean();
                ObservableCollection<todayEvent> items2 = new ObservableCollection<todayEvent>();
                mlistView.ItemsSource = items2;
                mlistView4.ItemsSource = null;
                login = false;
                timer.Stop();
                loginButton.Content = "Log In";
            }
            else
            {
                App.Initialize();
                initializeTimer();
                login = true;
                loginButton.Content = "Log Out";
                initializeGoogleInfo();
                
                initializeMeetingInfo();
                initializeFileInfo();
            }   
        }

        // Initialize file title click event
        public void fileClick(object sender, EventArgs e)
        {
            if (SystemParameters.SwapButtons) // Or use SystemInformation.MouseButtonsSwapped
            {}
            else
            {
                string str = sender.ToString();
                str = str.Substring(str.LastIndexOf(' ') + 1);
                if (str != "System.Windows.Controls.Button")
                {
                    clickShowFiles newWindow = new clickShowFiles(str);
                    newWindow.Show();
                }
                else
                {
                    System.Windows.MessageBox.Show("No file modified in this meeting.");
                }

            }
         
        }

        // Initialize meeting title click event
        private void MeetingClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            if (button.Tag.ToString() != "")
            {
                clickShowFiles newWindow = new clickShowFiles(button.Tag.ToString());
                newWindow.Show();
            }else
            {
                System.Windows.MessageBox.Show("No file modified in this meeting.");
            }
        }

        // Initialize Hyperlink to open the file
        private void Hyperlink_RequestNavigate(object sender,
                                     System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(e.Uri.AbsoluteUri);
            }catch{
                System.Windows.MessageBox.Show("The file may be removed or moved to another path.");
            }
        }

        // System tray minimization
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();
            base.OnStateChanged(e);
        }  
    }

    // class for different data bindings
    public class todayEvent
    {
        public string Time { get; set; }
        public string Name { get; set; }
        public string Attendee { get; set; }
        public string Date { get; set; }
    }

    public class previousMeeting
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
        public string Attendee { get; set;}
        public string Docs { get; set; }
    }

    public class recentFile
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Attendee { get; set; }
        public string Meetings { get; set; }
        public string Files { get; set; }
    }
    
    public class searchFile
    {
        public string Time { get; set; }
        public string Title { get; set; }
        public string Attendee { get; set; }
    }

    public class searchMeeting
    {
        public string Time { get; set; }
        public string Name { get; set; }
        public string Attendee { get; set; }
        public string Docs { get; set; }
    }

    public class clickMeeting {
        public string Name { get; set; }
        public string createdTime { get; set; }
        public string lastModified { get; set; }
    }



}
