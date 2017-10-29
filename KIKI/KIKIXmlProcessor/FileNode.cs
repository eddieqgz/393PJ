using System;
using System.Collections.Generic;

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
        public FileNode(String fN, String FID, String mTime, String cTime, String eTime, String ext, String fPath)
        {
            fileName = fN;
            filePath = fPath;
            extension = ext;
            fileID = Convert.ToInt32(FID);

            if (mTime == null)
            {
                modifiedTime = DateTime.MinValue;
            }
            if (mTime != null)
            {
                modifiedTime = Tools.StringToTime(mTime);
            }

            if (cTime == null)
            {
                createdTime = DateTime.MinValue;
            }
            if (cTime != null)
            {
                createdTime = Tools.StringToTime(cTime);
            }

            if (eTime == null)
            {
                executeTime = DateTime.MinValue;
            }
            if (eTime != null)
            {
                executeTime = Tools.StringToTime(eTime);
            }

        }

        public FileNode(String fN, String FID, DateTime mTime, DateTime cTime, DateTime eTime, String ext, String fPath)
        {
            fileName = fN;
            filePath = fPath;
            extension = ext;
            fileID = Convert.ToInt32(FID);

            if (mTime == null)
            {
                modifiedTime = DateTime.MinValue;
            }
            if (mTime != null)
            {
                modifiedTime = mTime;
            }

            if (cTime == null)
            {
                createdTime = DateTime.MinValue;
            }
            if (cTime != null)
            {
                createdTime = cTime;
            }

            if (eTime == null)
            {
                executeTime = DateTime.MinValue;
            }
            if (eTime != null)
            {
                executeTime = eTime;
            }
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
            try
            {
                fileID = Convert.ToInt32(FID);
            }
            catch { }
        }

        public void SetFilePath(String fPath)
        {
            filePath = fPath;
        }

        public void SetModifiedTime(String mTime)
        {
            modifiedTime = Tools.StringToTime(mTime);
        }

        public void SetCreatedTime(String cTime)
        {
            createdTime = Tools.StringToTime(cTime);
        }

        public void SetExecuteTime(String eTime)
        {
            executeTime = Tools.StringToTime(eTime);
        }

        public void SetExtension(String ext)
        {
            extension = ext;
        }

        public void SetMissing(String ms)
        {
            if (ms == "Yes" || ms == "True")
            {
                missing = true;
            }
            else
            {
                missing = false;
            }
        }

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
                String ModifiedTimeS = Tools.TimeToString(modifiedTime);
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
                String CreatedTimeS = Tools.TimeToString(createdTime);
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
                String ExecutedTimeS = Tools.TimeToString(executeTime);
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
            if (MeetingList.Count == 0)
            {
                return "";
            }
            else
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
    }
}