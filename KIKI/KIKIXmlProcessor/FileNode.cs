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
        private LinkedList<String> MeetingList;

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

        public FileNode(String fN, String FID, DateTime mTime, DateTime cTime, DateTime eTime, String ext, String fPath)
        {
            fileName = fN;
            filePath = fPath;
            extension = ext;
            fileID = Convert.ToInt32(FID);
            modifiedTime = mTime;
            createdTime = cTime;
            executeTime = eTime;
        }

        public void SetFileName(String fN)
        {
            fileName = fN;
        }

        public void SetFileID(Int32 FID)
        {
            fileID = FID;
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

        public void SetMissing(int ms)
        {
            if (ms == 1)
            {
                missing = true;
            }
            else
            {
                missing = false;
            }
        }

        public void AddMeetings(String MeetingID)
        {
            MeetingList.AddLast(MeetingID);
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

        public String GetModifiedTimeS(DateTime modifiedTime)
        {
            String year = Convert.ToString(modifiedTime.Year);
            String month = Convert.ToString(modifiedTime.Month);
            String date = Convert.ToString(modifiedTime.Date);
            String hour = Convert.ToString(modifiedTime.Hour);
            String minute = Convert.ToString(modifiedTime.Minute);
            String second = Convert.ToString(modifiedTime.Second);

            String ModifiedTimeS = year + "/" + month + "/" + date + " " + hour + ":" + minute + ":" + second;
            return ModifiedTimeS;
        }

        public DateTime GetCreatedTime()
        {
            return createdTime;
        }

        public String GetCreatedTimeS(DateTime createdTime)
        {
            String year = Convert.ToString(createdTime.Year);
            String month = Convert.ToString(createdTime.Month);
            String date = Convert.ToString(createdTime.Date);
            String hour = Convert.ToString(createdTime.Hour);
            String minute = Convert.ToString(createdTime.Minute);
            String second = Convert.ToString(createdTime.Second);

            String CreatedTimeS = year + "/" + month + "/" + date + " " + hour + ":" + minute + ":" + second;
            return CreatedTimeS;
        }

        public DateTime GetExecutedTime()
        {
            return executeTime;
        }

        public String GetExecutedTimeS(DateTime executeTime)
        {
            String year = Convert.ToString(executeTime.Year);
            String month = Convert.ToString(executeTime.Month);
            String date = Convert.ToString(executeTime.Date);
            String hour = Convert.ToString(executeTime.Hour);
            String minute = Convert.ToString(executeTime.Minute);
            String second = Convert.ToString(executeTime.Second);

            String ExecutedTimeS = year + "/" + month + "/" + date + " " + hour + ":" + minute + ":" + second;
            return ExecutedTimeS;
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
    }

}
