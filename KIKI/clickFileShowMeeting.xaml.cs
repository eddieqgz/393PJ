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
using System.Windows.Shapes;
using KIKIXmlProcessor;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace KIKI
{
    /// <summary>
    /// Interaction logic for clickFileShowMeeting.xaml
    /// </summary>
    public partial class clickFileShowMeeting : Window
    {
        private bool whetherThrows = false;
        public clickFileShowMeeting(string fileID)
        {
                        InitializeComponent();
            XMLProcessor processor = new XMLProcessor();
            XMLSearcher searcher = new XMLSearcher(processor.GetWorkingPath());


            LinkedList<MeetingNode> meetingList = searcher.FindMeetingsByFileID(fileID);
            FileName.Text = searcher.FindFilesByFileIDs(fileID).Last().GetFileName();
           try
            {
                FileLink.NavigateUri = new System.Uri(searcher.FindFilesByFileIDs(fileID).Last().GetFilePath());
            }
            catch(System.UriFormatException ex)
            {
                whetherThrows = true;
            }
            ObservableCollection<clickFile> items = new ObservableCollection<clickFile>();
            foreach (MeetingNode meeting in meetingList)
            {
                //Debug.Print(meeting.GetMeetingID()+""+ meeting.GetStartTimeS());
                items.Add(new clickFile() { Time = meeting.GetStartTimeS(), Name = meeting.GetMeetingTitle(), Attendee = meeting.GetAttendents()});
                MeetingList.ItemsSource = items;
            }
        }

        private void listView_SelectionChanged(Object sender, EventArgs e)
        {

        }

        private void Hyperlink_RequestNavigate(object sender,
                                      System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(e.Uri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("The file may be removed or moved to another path.");
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
