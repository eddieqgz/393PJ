using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KIKIXmlProcessor;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace KIKI
{

    public partial class clickFileShowMeeting : Window
    {
        private bool whetherThrows = false;

        // Constructor
        public clickFileShowMeeting(string fileID)
        {
                        InitializeComponent();
            XMLProcessor processor = new XMLProcessor(App.id);
            XMLSearcher searcher = new XMLSearcher(processor.GetWorkingPath(),App.id);


            LinkedList<MeetingNode> meetingList = searcher.FindMeetingsByFileID(fileID);
            FileName.Text = searcher.FindFilesByFileIDs(fileID).Last().GetFileName();
           try
            {
                FileLink.NavigateUri = new System.Uri(searcher.FindFilesByFileIDs(fileID).Last().GetFilePath());
            }
            catch
            {
                whetherThrows = true;
            }
            ObservableCollection<clickFile> items = new ObservableCollection<clickFile>();
            foreach (MeetingNode meeting in meetingList)
            {
                items.Add(new clickFile() { Time = meeting.GetStartTimeS(), Name = meeting.GetMeetingTitle(), Attendee = meeting.GetAttendents()});
                MeetingList.ItemsSource = items;
            }
        }

        private void Hyperlink_RequestNavigate(object sender,
                                      System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(e.Uri.AbsoluteUri);
            }
            catch
            {
                MessageBox.Show("The file may be removed or moved to another path.");
            }
        }
    }

    public class clickFile
    {
        public string Time { get; set; }
        public string Name { get; set; }
        public string Attendee { get; set; }
        public string Docs { get; set; }
    }
}
