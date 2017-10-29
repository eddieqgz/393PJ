using System;
using System.Collections.Generic;
using System.Windows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Diagnostics;
using KIKIXmlProcessor;

namespace KIKI
{
    public partial class App : Application
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        static List<string> bufferGoogle = new List<string>();
        static List<string> bufferMeeting = new List<string>();
        static List<string> bufferFile = new List<string>();
        public static LinkedList<FileNode> fileList = new LinkedList<FileNode>();
        public static LinkedList<MeetingNode> meetingList = new LinkedList<MeetingNode>();
        static UserCredential credential;
        public static string id;

        // Initialize everything
        public static void Initialize()
        {
            InitializeGoogle();
            InitializeCalendar();
            InitializeCore();
            InitializeMeetingTab();
            InitializeFileTab();
        }

        // Initialize google authorization
        public static void InitializeGoogle()
        {
            using (var stream =
              new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                      System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");
                ClientSecrets a = GoogleClientSecrets.Load(stream).Secrets;
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                  a,
                  Scopes,
                  "user",
                  CancellationToken.None).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
        }

        // Initilalize Google Calendar Sevice and get data from calendar
        public static void InitializeCalendar()
        {
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.TimeMax = DateTime.Today.AddHours(24);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            var calendars = service.CalendarList.List().Execute().Items;
            id = Tools.convertEmailtoID(calendars[0].Id);

            // List events.
            try
            {
                Events events = request.Execute();
                if (events.Items != null && events.Items.Count > 0)
                {

                    foreach (var eventItem in events.Items)
                    {
                        // handle no attendee problem
                        string attendee = "";
                        string when = eventItem.Start.DateTime.ToString();
                        if (eventItem.Attendees != null)
                        {
                            EventAttendee[] attendeeData = new EventAttendee[eventItem.Attendees.Count];
                            string[] attendeeString = new string[eventItem.Attendees.Count];
                            eventItem.Attendees.CopyTo(attendeeData, 0);
                            List<String> attendeelist = new List<String>();

                            for (int i = 0; i < eventItem.Attendees.Count; i++)
                            {
                                if ((attendeeData[i].DisplayName != null) && (attendeeData[i].DisplayName.Replace(" ", "") != ""))
                                {
                                    attendeelist.Add(attendeeData[i].DisplayName);
                                }
                            }

                            // handle one attendee problem
                            if (attendeelist.Count >= 2)
                            {
                                for (int i = 0; i < attendeelist.Count; i++)
                                {
                                    attendee = attendee + attendeelist[i];
                                    if (i != attendeelist.Count - 1)
                                    {
                                        attendee = attendee + "; ";
                                    }
                                }
                            }
                            else
                            {
                                attendee = "N/A";
                            }
                        }
                        else
                        {
                            attendee = "N/A";
                        }

                        if (String.IsNullOrEmpty(when))
                        {
                            when = eventItem.Start.Date;
                        }
                        bufferGoogle.Add(when);
                        bufferGoogle.Add(eventItem.Summary);
                        bufferGoogle.Add(attendee);
                    }
                }
                else
                {
                    Debug.WriteLine("No upcoming events found.");
                }
            }

            catch
            {
                MessageBox.Show("No Internet Connection");
            }

            Console.Read();
        }

        // Initialize xmls using google info
        public static void InitializeCore()
        {
            XMLProcessor p = new XMLProcessor(id);
            p.Read();
            fetchFromGoogle(p.GetLastUpdateTime());
            Debug.Print("Initialize Core");
            foreach (FileNode n in fileList)
            {
                Debug.Print("File name is " + n.GetFileName());
            }
            foreach (MeetingNode n in meetingList)
            {
                Debug.Print("Meeting name is " + n.GetMeetingTitle());
            }

            p.ProcessFileWithMeetingList(meetingList, fileList);
            Debug.Print("between two method");
            p.Write();
            Debug.Print("after two method");
        }

        // Read meeting data from xml
        public static void InitializeMeetingTab()
        {
            meetingList = returnMeeting();
            foreach (MeetingNode item in meetingList)
            {
                string date = item.GetStartTime().ToString().Split(' ')[0];
                string start = item.GetStartTime().ToString().Split(' ')[1];
                string end = item.GetEndTime().ToString().Split(' ')[1];
                bufferMeeting.Add(date);
                bufferMeeting.Add(start + "-" + end);
                bufferMeeting.Add(item.GetMeetingTitle());
                bufferMeeting.Add(item.GetAttendents());
                bufferMeeting.Add(item.GetFileListS() + "");
            }
        }

        // Read file data from xml
        public static void InitializeFileTab()
        {
            fileList = returnFile();
            foreach (FileNode item in fileList)
            {
                bufferFile.Add(item.GetFileName());
                bufferFile.Add(item.GetFilePath());
                bufferFile.Add(item.GetMeetingListS());
            }
        }

        // Update xmls using new google info
        public static void UpdateCore()
        {
            XMLProcessor p = new XMLProcessor(id);
            p.Read();
            p.ProcessFileWithMeetingList(meetingList, fileList);
            p.Write();
        }

        // Get google info from last update time
        public static void fetchFromGoogle(DateTime minTime)
        {
            MeetingNode meeting = new MeetingNode();
            FileNode file = new FileNode();

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = minTime;
            request.TimeMax = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;

            // List events.
            try
            {
                Events events = request.Execute();
                if (events.Items != null && events.Items.Count > 0)
                {

                    foreach (var eventItem in events.Items)
                    {
                        meeting = new MeetingNode();
                        string attendee = "";
                        string when = eventItem.Start.DateTime.ToString();
                        if (eventItem.Attendees != null)
                        {
                            EventAttendee[] attendeeData = new EventAttendee[eventItem.Attendees.Count];
                            eventItem.Attendees.CopyTo(attendeeData, 0);
                            List<String> attendeelist = new List<String>();
                            for (int i = 0; i < eventItem.Attendees.Count; i++)
                            {
                                if ((attendeeData[i].DisplayName != null) && (attendeeData[i].DisplayName != ""))
                                {
                                    attendeelist.Add(attendeeData[i].DisplayName);
                                }
                            }
                            if (attendeelist.Count >= 2)
                            {
                                for (int i = 0; i < attendeelist.Count; i++)
                                {
                                    attendee = attendee + attendeelist[i];
                                    if (i != attendeelist.Count - 1)
                                    {
                                        attendee = attendee + "; ";
                                    }
                                }
                            }
                            else
                            {
                                attendee = "N/A";
                            }
                        }
                        else
                        {
                            attendee = "N/A";
                        }
                        // add new meeting info to linkedlist
                        meeting.SetAttendents(attendee);
                        meeting.SetMeetingID(eventItem.Id);
                        meeting.SetParentID(eventItem.ICalUID.ToString());
                        meeting.SetStartTime(when);
                        meeting.SetEndTime(eventItem.End.DateTime.ToString());
                        meeting.SetMeetingTitle(eventItem.Summary);
                        if (DateTime.Compare(meeting.GetEndTime(), DateTime.Now) <= 0)
                        {
                            meetingList.AddLast(meeting);
                        }
                        if (eventItem.Attachments != null)
                        {

                            for (int i = 0; i < eventItem.Attachments.Count; i++)
                            {
                                // add new file info to linkedlist
                                file = new FileNode();
                                file.SetModifiedTime(eventItem.Start.DateTime.ToString());
                                file.SetFileID(" ");
                                file.SetExtension("GoogleDrive");
                                file.SetFileName(eventItem.Attachments[i].Title);
                                file.SetFilePath(eventItem.Attachments[i].FileUrl);
                                file.AddMeetings(eventItem.Id);
                                file.SetMissing("No");
                                fileList.AddLast(file);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Network connection lost");
            }
        }

        // Get method for new google meeting info
        public static LinkedList<MeetingNode> getGoogleMeetingList()
        {
            return meetingList;
        }

        // Get method for new google file info
        public static LinkedList<FileNode> getGoogleFileList()
        {
            return fileList;
        }

        // Get method for google info buffer used by initializing UI
        public static List<string> getGoogleBuffer()
        {
            return bufferGoogle;
        }

        // Get method for meeting buffer used by initializing UI
        public static List<string> getMeetingBuffer()
        {
            return bufferMeeting;
        }

        // Get method for file buffer used by initializing UI
        public static List<string> getFileBuffer()
        {
            return bufferFile;
        }

        // Empty all things when synchronizing periodically
        public static void Clean()
        {
            bufferGoogle = new List<string>();
            bufferMeeting = new List<string>();
            bufferFile = new List<string>();
            meetingList = new LinkedList<MeetingNode>();
            fileList = new LinkedList<FileNode>();
            credential = null;
        }

        // Empty meeting buffer when synchronizing periodically
        public static void UpdateMeetingList()
        {
            bufferMeeting = new List<string>();
        }

        // Empty file buffer when synchronizing periodically
        public static void UpdateFileList()
        {
            bufferFile = new List<string>();
        }

        // Empty Google info buffer when synchronizing periodically
        public static void UpdateGoogleList()
        {
            bufferGoogle = new List<string>();
        }

        // Return the meeting list from xml
        public static LinkedList<MeetingNode> returnMeeting()
        {
            Debug.Print("before reading meeting");
            LinkedList<MeetingNode> k = new LinkedList<MeetingNode>();
            XMLProcessor processor = new XMLProcessor(id);
            XMLSearcher searcher = new XMLSearcher(processor.GetWorkingPath(), id);
            k = searcher.FindMeetingsByMeetingTitleKeywords("");
            return k;
        }

        // Return the file list from xml
        public static LinkedList<FileNode> returnFile()
        {
            LinkedList<FileNode> k = new LinkedList<FileNode>();
            XMLProcessor processor = new XMLProcessor(id);
            XMLSearcher searcher = new XMLSearcher(processor.GetWorkingPath(), id);
            k = searcher.FindFilesByFileNameKeywords("");
            return k;

        }

        // Revoke token when logout
        public static async void revoke()
        {
            try
            {
                await credential.RevokeTokenAsync(CancellationToken.None);
            }
            catch
            {
                MessageBox.Show("Network connection lost");
            }
        }
    }
}
