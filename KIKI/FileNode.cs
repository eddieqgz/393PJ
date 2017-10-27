using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIKIXmlProcessor
{
    public class FileNode
    {
        private String fileName = "";
        private Int32 fileID = 0;
        private DateTime modifiedTime;
        private DateTime createdTime;
        private DateTime executeTime;
        private Boolean missing = true;
        private String extension = "";
        private String filePath = "";
        public LinkedList<String> MeetingList = new LinkedList<String>();

        public FileNode() { }
        public FileNode(String fN, String FID, String mTime, String cTime, String eTime, String ext, String fPath, String MeetingID)
        {
            fileName = fN;
            filePath = fPath;
            extension = ext;
            fileID = Convert.ToInt32(FID);
            modifiedTime = this.StringToTime(mTime);
            createdTime = this.StringToTime(cTime);
            executeTime = this.StringToTime(eTime);
            this.AddMeetings(MeetingID);
        }

        public FileNode(String fN, String FID, DateTime mTime, DateTime cTime, DateTime eTime, String ext, String fPath, String MeetingID)
        {
            fileName = fN;
            filePath = fPath;
            extension = ext;
            fileID = Convert.ToInt32(FID);
            modifiedTime = mTime;
            createdTime = cTime;
            executeTime = eTime;
            this.AddMeetings(MeetingID);
        }

        public void SetFileName(String fN)
        {
            fileName = fN;
        }

        public void SetFileID(Int32 FID)
        {
            fileID = FID;
        }

        public void SetFileID(String FID)
        {
            fileID = Convert.ToInt32(FID);
        }

        public void SetFilePath(String fPath)
        {
            filePath = fPath;
        }

        public void SetModifiedTime(String mTime)
        {
            modifiedTime = this.StringToTime(mTime);
        }

        public void SetCreatedTime(String cTime)
        {
            createdTime = this.StringToTime(cTime);
        }

        public void SetExecuteTime(String eTime)
        {
            executeTime = this.StringToTime(eTime);
        }

        public void SetExtension(String ext)
        {
            extension = ext;
        }

        public void SetMissing(String ms)
        {
            if (ms == "Yes")
            {
                missing = true;
            }
            else
            {
                missing = false;
            }
        }

        //May need adjustment
        public void SetMeetings(String MeetingID)
        {
            String[] meet = MeetingID.Split(';');
            MeetingList.Clear();
            for (int i = 0; i < meet.Length; i++)
            {
                MeetingList.AddLast(meet[i]);
            }
        }

        public void AddMeetings(String MeetingID)
        {
            MeetingList.AddLast(MeetingID);
        }

        public void AddMeetings(Int32 MeetingID)
        {
            MeetingList.AddLast(Convert.ToString(MeetingID));
        }

        public String GetFileName()
        {
            return fileName;
        }

        public Int32 GetFileID()
        {
            return fileID;
        }

        public DateTime GetModifiedTime()
        {
            return modifiedTime;
        }

        public String GetModifiedTimeS()
        {
            if (modifiedTime.Year != 1)
            {
                String ModifiedTimeS = this.TimeToString(modifiedTime);
                return ModifiedTimeS;
            }
            else
            {
                return "";
            }
        }

        public DateTime GetCreatedTime()
        {
            return createdTime;
        }

        public String GetCreatedTimeS()
        {
            if (createdTime.Year != 1)
            {
                String CreatedTimeS = this.TimeToString(createdTime);
                return CreatedTimeS;
            }
            else
            {
                return "";
            }
        }

        public DateTime GetExecuteTime()
        {
            return executeTime;
        }

        public String GetExecuteTimeS()
        {
            if (executeTime.Year != 1)
            {
                String ExecutedTimeS = this.TimeToString(executeTime);
                return ExecutedTimeS;
            }
            else
            {
                return "";
            }
        }

        public Boolean GetMissing()
        {
            return missing;
        }

        public String GetExtension()
        {
            return extension;
        }

        public String GetFilePath()
        {
            return filePath;
        }

        public LinkedList<String> GetMeetingList()
        {
            return MeetingList;
        }

        public String GetMeetingListS()
        {
            String s = "";
            foreach (String n in MeetingList)
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

        public Boolean AddToMeetinglist(String s)
        {
            if (MeetingList.Find(s) == null)
            {
                MeetingList.AddLast(s);
                return true;
            }
            else
            {
                return false;
            }
        }

        public DateTime StringToTime(String s)
        {
            if (s == "N / A")
            {
                DateTime empty = DateTime.MinValue;
                return empty;
            }
            else
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
    }

}
