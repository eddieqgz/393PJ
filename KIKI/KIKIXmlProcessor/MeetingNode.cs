using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KIKIXmlProcessor
{
    public class MeetingNode
    {
        private string MeetingTitle = "";
        private String MeetingID = "";
        private DateTime StartTime;
        private DateTime EndTime;
        private TimeSpan Duration;
        private Int32 ParentID;
        private string Attendents; //Names are seperated by ";"
        //private FileNode Files;
        private LinkedList<Int32> FileList;


        public MeetingNode() { }
        public MeetingNode(String MT, String MID, String sTime, String eTime, String PID, String Attend, Int32 FileID)
        {
            MeetingTitle = MT;
            MeetingID = MID;
            Attendents = Attend;
            ParentID = Convert.ToInt32(PID);
            StartTime = this.StringToTime(sTime);
            EndTime = this.StringToTime(eTime);
            //Files = new FileNode("", FileID, "", "", "", "", "");
            this.AddFiles(FileID);
            Duration = this.computeDuration(StartTime, EndTime);
        }

        public MeetingNode(String MT, String MID, DateTime sTimeD, DateTime eTimeD, String PID, String Attend, Int32 FileID)
        {
            MeetingTitle = MT;
            MeetingID = MID;
            Attendents = Attend;
            ParentID = Convert.ToInt32(PID);
            StartTime = sTimeD;
            EndTime = eTimeD;
            this.AddFiles(FileID);
            Duration = this.computeDuration(StartTime, EndTime);
        }

        public void SetMeetingTitle(String MT)
        {
            MeetingTitle = MT;
        }

        public void SetMeetingID(String MID)
        {
            MeetingID = MID;
        }

        public void SetStartTime(String sTime)
        {
            StartTime = this.StringToTime(sTime);
        }

        public void SetEndTime(String eTime)
        {
            EndTime = this.StringToTime(eTime);
        }

        public void SetParentID(Int32 PID)
        {
            ParentID = PID;
        }

        public void SetAttendents(String Attend)
        {
            Attendents = Attend;
        }

        public void AddFiles(Int32 FileID)
        {
            FileList.AddLast(FileID);
        }

        public void SetDuration(String sTime, String eTime)
        {
            Duration = this.computeDuration(this.StringToTime(sTime), this.StringToTime(eTime));
        }

        public String GetMeetingTitle()
        {
            return MeetingTitle;
        }

        public String GetMeetingID()
        {
            return MeetingID;
        }

        public DateTime GetStartTime()
        {
            return StartTime;
        }

        public String GetStartTimeS(DateTime StartTime)
        {
            String year = Convert.ToString(StartTime.Year);
            String month = Convert.ToString(StartTime.Month);
            String date = Convert.ToString(StartTime.Date);
            String hour = Convert.ToString(StartTime.Hour);
            String minute = Convert.ToString(StartTime.Minute);
            String second = Convert.ToString(StartTime.Second);

            String StartTimeS = year + "/" + month + "/" + date + " " + hour + ":" + minute + ":" + second;
            return StartTimeS;
        }

        public DateTime GetEndTime()
        {
            return EndTime;
        }

        public String GetEndTimeS(DateTime EndTime)
        {
            String year = Convert.ToString(EndTime.Year);
            String month = Convert.ToString(EndTime.Month);
            String date = Convert.ToString(EndTime.Date);
            String hour = Convert.ToString(EndTime.Hour);
            String minute = Convert.ToString(EndTime.Minute);
            String second = Convert.ToString(EndTime.Second);

            String EndTimeS = year + "/" + month + "/" + date + " " + hour + ":" + minute + ":" + second;
            return EndTimeS;
        }

        public TimeSpan GetDuration()
        {
            return Duration;
        }

        public Int32 GetParentID()
        {
            return ParentID;
        }

        public String GetAttendents()
        {
            return Attendents;
        }

        public LinkedList<Int32> GetFileList()
        {
            return FileList;
        }

        public DateTime StringToTime(String s)
        {
            String[] s1 = s.Split(' ');
            String[] s2 = s1[0].Split('/');
            String[] s3 = s1[1].Split(':');
            int year = Convert.ToInt32(s2[0]);
            int month = Convert.ToInt32(s2[1]);
            int day = Convert.ToInt32(s2[2]);
            int hour = Convert.ToInt32(s3[0]);
            int minute = Convert.ToInt32(s3[1]);
            int second = Convert.ToInt32(s3[2]);

            DateTime x = new DateTime(year, month, day, hour, minute, second);
            return x;
        }

        public TimeSpan computeDuration(DateTime StartTime, DateTime EndTime)
        {
            TimeSpan duration = EndTime - StartTime;
            return duration;
        }
    }
}
