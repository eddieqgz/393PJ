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
        private String ParentID;
        private string Attendents; //Names are seperated by ";"
        public LinkedList<Int32> FileList = new LinkedList<Int32>();


        public MeetingNode() { }
        public MeetingNode(String MT, String MID, String sTime, String eTime, String PID, String Attend)
        {
            MeetingTitle = MT;
            MeetingID = MID;
            Attendents = Attend;
            ParentID = PID;
            StartTime = this.StringToTime(sTime);
            EndTime = this.StringToTime(eTime);
            //Files = new FileNode("", FileID, "", "", "", "", "");
            //this.AddFiles(FileID);
            Duration = this.computeDuration(StartTime, EndTime);
        }

        public MeetingNode(String MT, String MID, DateTime sTimeD, DateTime eTimeD, String PID, String Attend)
        {
            MeetingTitle = MT;
            MeetingID = MID;
            Attendents = Attend;
            ParentID = PID;
            StartTime = sTimeD;
            EndTime = eTimeD;
            //this.AddFiles(FileID);
            Duration = this.computeDuration(StartTime, EndTime);
        }

        public void SetMeetingTitle(String MT)
        {
            MeetingTitle = MT;
        }

        public void SetMeetingTitle(Int32 MT)
        {
            MeetingTitle = Convert.ToString(MT);
        }

        public void SetMeetingID(String MID)
        {
            MeetingID = MID;
        }

        public void SetMeetingID(Int32 MID)
        {
            MeetingID = Convert.ToString(MID);
        }

        public void SetStartTime(String sTime)
        {
            StartTime = this.StringToTime(sTime);
        }

        public void SetEndTime(String eTime)
        {
            EndTime = this.StringToTime(eTime);
        }

        public void SetParentID(String PID)
        {
            ParentID = PID;
        }

        public void SetParentID(Int32 PID)
        {
            ParentID = Convert.ToString(PID);
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

        public void SetDuration(String duration)
        {
            Duration = TimeSpan.Parse(duration);
        }

        //May need adjustment
        public void SetFiles(String fileString)
        {
            String[] num = fileString.Split(';');
            FileList.Clear();
            for (int i = 0; i < num.Length; i++)
            {
                FileList.AddLast(Convert.ToInt32(num[i]));
            }
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

        public String GetStartTimeS()
        {
            if (StartTime.Year != 1)
            {
                String StartTimeS = this.TimeToString(StartTime);
                return StartTimeS;
            }
            else
            {
                return "";
            }
        }

        public DateTime GetEndTime()
        {
            return EndTime;
        }

        public String GetEndTimeS()
        {
            if (EndTime.Year != 1)
            {
                String EndTimeS = this.TimeToString(EndTime);
                return EndTimeS;
            }
            else
            {
                return "";
            }
        }

        public TimeSpan GetDuration()
        {
            Duration = this.computeDuration(StartTime, EndTime);
            return Duration;
        }

        public String GetParentID()
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

        public String GetFileListS()
        {
            String s = "";
            foreach (int n in FileList)
            {
                s = s + n.ToString();
                s = s + ";";
                //Console.Write(s);
            }
            if (s != "")
            {
                s = s.Remove(s.Length - 1);
            }
            return s;
        }

        public Boolean AddToFilelist(Int32 i)
        {
            if (FileList.Find(i) == null)
            {
                FileList.AddLast(i);
                return true;
            }
            else
            {
                return false;
            }
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

        public String TimeToString(DateTime dt)
        {
            String year = dt.Year.ToString("0000");
            String month = dt.Month.ToString("00");
            String day = dt.Day.ToString("00");
            String hour = dt.Hour.ToString("00");
            String minute = dt.Minute.ToString("00");
            String second = dt.Second.ToString("00");

            String dtString = year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
            return dtString;
        }

        public TimeSpan computeDuration(DateTime sTime, DateTime eTime)
        {
            TimeSpan duration = eTime - sTime;
            return duration;
        }
    }
}
