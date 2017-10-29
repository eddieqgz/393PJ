using System;
using System.Collections.Generic;

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

            if (sTime == null)
            {
                StartTime = DateTime.MinValue;
            }
            if (sTime != null)
            {
                if (sTime.Length == 10)
                {
                    StartTime = this.WholeDayMeetingStringToTime(sTime);
                }
                else
                {
                    StartTime = Tools.StringToTime(sTime);
                }
            }

            if (eTime == null)
            {
                EndTime = DateTime.MinValue;
            }
            if (eTime != null)
            {
                if (eTime.Length == 10)
                {
                    EndTime = this.WholeDayMeetingStringToTime(eTime);
                    EndTime.AddHours(23);
                    EndTime.AddMinutes(59);
                    EndTime.AddSeconds(59);
                }
                else
                {
                    EndTime = Tools.StringToTime(eTime);
                }
            }

            Duration = Tools.ComputeDuration(StartTime, EndTime);
        }

        public MeetingNode(String MT, String MID, DateTime sTimeD, DateTime eTimeD, String PID, String Attend)
        {
            MeetingTitle = MT;
            MeetingID = MID;
            Attendents = Attend;
            ParentID = PID;

            if (sTimeD == null)
            {
                StartTime = DateTime.MinValue;
            }
            if (sTimeD != null)
            {
                StartTime = sTimeD;
            }

            if (eTimeD == null)
            {
                EndTime = DateTime.MinValue;
            }
            if (eTimeD != null)
            {
                EndTime = eTimeD;
            }

            Duration = Tools.ComputeDuration(StartTime, EndTime);
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
            if (sTime.Length == 10)
            {
                StartTime = this.WholeDayMeetingStringToTime(sTime);
            }
            else
            {
                StartTime = Tools.StringToTime(sTime);
            }
        }

        public void SetEndTime(String eTime)
        {

            if (eTime.Length == 10)
            {
                EndTime = this.WholeDayMeetingStringToTime(eTime);
                EndTime.AddHours(23);
                EndTime.AddMinutes(59);
                EndTime.AddSeconds(59);
            }
            else
            {
                EndTime = Tools.StringToTime(eTime);
            }
        }

        public DateTime WholeDayMeetingStringToTime(String s)
        {
            if (s == "N / A")
            {
                DateTime na = DateTime.MinValue;
                return na;
            }
            if (s == "")
            {
                DateTime empty = DateTime.MinValue;
                return empty;
            }
            else
            {
                String[] r1 = s.Split(' ');
                String[] r2 = r1[0].Split('/');
                int year = Convert.ToInt32(r2[0]);
                int month = Convert.ToInt32(r2[1]);
                int day = Convert.ToInt32(r2[2]);

                DateTime d = new DateTime(year, month, day, 0, 0, 0);
                return d;
            }
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

        //May need adjustment
        public void SetFiles(String fileString)
        {
            if (fileString == "")
            {
                FileList = new LinkedList<int>();
                return;
            }
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
                String StartTimeS = Tools.TimeToString(StartTime);
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
                String EndTimeS = Tools.TimeToString(EndTime);
                return EndTimeS;
            }
            else
            {
                return "";
            }
        }

        public TimeSpan GetDuration()
        {
            Duration = Tools.ComputeDuration(StartTime, EndTime);
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
    }
}