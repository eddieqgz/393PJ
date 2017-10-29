using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace KIKIXmlProcessor
{
    //The class reads and writes xml with the information from the google calendar and recent files view
    public class XMLProcessor
    {
        private String mfile = "meetings.xml";
        private String ffile = "files.xml";
        private String sfile = "Settings.xml";
        private String workingPath = "";
        private String tempPath = "temp.xml";
        private String RSFile = "records.xml";
        private LinkedList<FileNode> fileList = new LinkedList<FileNode>();
        private LinkedList<MeetingNode> meetingList = new LinkedList<MeetingNode>();
        private LinkedList<FileNode> AttachmentList = new LinkedList<FileNode>();
        private LinkedList<FileNode> fileFinalList = new LinkedList<FileNode>();
        private DateTime lastUpdate = new DateTime();
        private int lastID = 0;
        private int trackID = 0;
        private TimeSpan minDuration = new TimeSpan(0, 1, 0);
        private String user = "";

        //Constructor of XMLProcessor, get basic settings information
        public XMLProcessor(String userID)
        {
            user = userID;
            mfile = "meetings" + user + ".xml";
            ffile = "files" + user + ".xml";
            sfile = "Settings" + user + ".xml";
            if (!File.Exists(sfile))
            {
                WriteSettings();
                Thread.Sleep(100);
                DateTime time = DateTime.Now - new TimeSpan(30, 0, 0, 0);
                lastUpdate = time;
                WriteNewFilesFile();
                WriteNewMeetingsFile(new LinkedList<MeetingNode>());
            }
            else
            {
                GetInfoFromSettings();
            }
        }

        #region Basic settings read, update and get
        // ---------------------------Read Basic Settings -------------------------------------

        //Read settings information from the Settings.xml
        public void GetInfoFromSettings()
        {
            XElement settingsFile = XElement.Load(sfile);
            workingPath = settingsFile.Element("Working_Path").Value;
            if (settingsFile.Element("Last_Update").Value != "Uninitialized")
            {
                lastUpdate = Tools.StringToTime(settingsFile.Element("Last_Update").Value);
            }
            else
            {
                DateTime time = DateTime.Now - new TimeSpan(30, 0, 0, 0);
                lastUpdate = time;
            }
            lastID = Convert.ToInt32(settingsFile.Element("Last_File_ID").Value);
            trackID = lastID;
            if (settingsFile.Element("Minimum_Duration").Value != "")
            {
                String[] s1 = settingsFile.Element("Minimum_Duration").Value.Split(';');
                TimeSpan newDuration = new TimeSpan(Convert.ToInt32(s1[0]),
                    Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                minDuration = newDuration;
            }
            UpdateFilePath();
        }

        //Get the minimum duration of a meeting should have
        public TimeSpan GetMinimumDuration()
        {
            return minDuration;
        }

        //Get the working path of the files set by the user
        public String GetWorkingPath()
        {
            return workingPath;
        }

        //Get the last update time 
        public int GetLastUpdateId()
        {
            return lastID;
        }

        //Get the datetime Type of Last Update Time
        public DateTime GetLastUpdateTime()
        {
            return lastUpdate;
        }

        //Update the path in this processor with the information read
        public void UpdateFilePath()
        {
            mfile = workingPath + mfile;
            ffile = workingPath + ffile;
            tempPath = workingPath + "temp.xml";
            RSFile = workingPath + "records.xml";
        }


        //Check if the file is in first use, if so, initialize basic files
        public Boolean FirstUse()
        {
            if (!((File.Exists(mfile)) || (File.Exists(ffile))))
            {
                if (!File.Exists(ffile))
                {
                    WriteNewFilesFile();
                }
                return true;
            }
            return false;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Read file list data from recent files view
        // ------------Read Recent File Records From the System----------------------------------
        // Post condition: A linked list of roughly filtered file data
        public void Read()
        {
            GetRFXML();
            FileInfo i = new FileInfo(tempPath);
            while (Tools.IsFileLocked(i)) { };
            RemoveXMLdeclaration();
            FileInfo i2 = new FileInfo(RSFile);
            while (Tools.IsFileLocked(i2)) { };
            ReadRecords();
        }

        //Call Recent Files View and Generate temporary data
        public Boolean GetRFXML()
        {
            Console.WriteLine("Program start fetching recent file history from the system...");
            String command = "/sxml temp.xml";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "RecentFilesView.exe";
            startInfo.Arguments = "/C " + command;
            //startInfo.Arguments = command;
            process.StartInfo = startInfo;
            try
            {
                process.Start();
            }
            catch
            {
                Environment.Exit(-1);
                return false;

            }
            Console.WriteLine("Fetching Finished...");
            return true;
        }

        //Read Records and process with the temporary data
        public void ReadRecords()
        {
            Console.WriteLine("Program start reading recent file history...");
            XmlTextReader reader = null;
            try
            {
                // Load the reader with the data file and ignore all white space nodes.         
                reader = new XmlTextReader(RSFile);
                int filter = 0; //the bit used to filter out entries
                reader.WhitespaceHandling = WhitespaceHandling.None;
                String fileName = "";
                String extension = "";
                String filePath = "";
                String mTime = "";
                String cTime = "";
                String eTime = "";
                String missing = "";
                String stored_in = "";
                //the flag used to indicate what is the current tag: 
                int tag = -1;
                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            tag = GetFlag(reader.Name);
                            if (tag == 0)
                            {
                                filter = 0;
                                fileName = "";
                                extension = "";
                                filePath = "";
                            }
                            if (tag == -2)
                            {
                                filter = 1;
                            }
                            break;
                        case XmlNodeType.Text:
                            if (tag == 1) { filePath = reader.Value; } // full name of the file indicate the path of the file
                            if (tag == 8) { fileName = reader.Value; } //the file_only property indicate the name of the file
                            if (tag == 7) { extension = reader.Value; } // the extension of the file
                            if (tag == 2) { mTime = reader.Value; } //modified Time
                            if (tag == 3) { cTime = reader.Value; } //created Time
                            if (tag == 4) { eTime = reader.Value; } //execute Time
                            if (tag == 5) { missing = reader.Value; } //missing?
                            if (tag == 6) { stored_in = reader.Value; } //stored_in
                            break;
                        case XmlNodeType.EndElement:
                            if ((filter == 0) && (reader.Name == "item"))
                            {
                                if ((Tools.IsValid(fileName, filePath, extension, stored_in)) && (Tools.IsValidTimeRange(mTime, cTime, eTime, lastUpdate, DateTime.Now) != -1))
                                {
                                    FileNode tempNode = new FileNode();
                                    tempNode.SetFileName(fileName);
                                    tempNode.SetCreatedTime(cTime);
                                    tempNode.SetExecuteTime(eTime);
                                    tempNode.SetModifiedTime(mTime);
                                    tempNode.SetFilePath(filePath);
                                    tempNode.SetMissing(missing);
                                    LinkedListNode<FileNode> node = new LinkedListNode<FileNode>(tempNode);
                                    fileList.AddLast(node);
                                }

                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            finally
            {
                if (reader != null)
                    reader.Close();
            }
            Console.WriteLine("Reading Finished...");
        }

        //Remove the unrecognization declaration line in the xml generated by the recent files view
        public void RemoveXMLdeclaration()
        {
            Console.WriteLine("Program start formatting raw recent file data...");
            StreamReader sr = new StreamReader(tempPath);
            sr.ReadLine();

            string body = null;
            string line = sr.ReadLine();
            while (line != null) // read file into body string
            {
                body += line + "\n";
                line = sr.ReadLine();
            }
            sr.Close(); //close file

            //Write all of the "body" to the same text file
            System.IO.File.WriteAllText(RSFile, body);
            FileInfo it = new FileInfo(tempPath);
            while (Tools.IsFileLocked(it)) { }
            File.Delete(tempPath);
            Console.WriteLine("Fomatting Finished...");
        }

        //The flag value of corresponding string tag
        public int GetFlag(String value)
        {
            if (value == "item") { return 0; }
            if (value == "filename") { return 1; }
            if (value == "modified_time") { return 2; }
            if (value == "created_time") { return 3; }
            if (value == "execute_time") { return 4; }
            if (value == "missing_file") { return 5; }
            if (value == "stored_in") { return 6; }
            if (value == "extension") { return 7; }
            if (value == "file_only") { return 8; }
            return -2;
        }

        #endregion

        #region Core Program
        public void ProcessFileWithMeetingNode(MeetingNode mNode)
        {
            //check if the meeting node is valid or not first
            if (!Tools.IsValidMeeting(mNode, minDuration))
            {
                return;
            }

            String meetingID = mNode.GetMeetingID();
            if (meetingID == "")
            {
                Console.WriteLine("Meeting ID for Meeting " + mNode.GetMeetingTitle() + "is missing.");
                return;
            }
            LinkedList<FileNode> filteredFileList = new LinkedList<FileNode>();
            DateTime startTime = mNode.GetStartTime();
            DateTime endTime = mNode.GetEndTime();
            foreach (FileNode item in fileList)
            {
                if (Tools.IsValidTimeRange(item.GetCreatedTime(), item.GetExecuteTime(), item.GetModifiedTime(), startTime, endTime) != -1)
                {
                    item.AddMeetings(meetingID);
                    filteredFileList.AddLast(item);
                }
            }
            foreach (FileNode Aitem in AttachmentList)
            {
                if (Aitem.GetMeetingListS() == mNode.GetMeetingID())
                {
                    filteredFileList.AddLast(Aitem);
                }
            }
            filteredFileList = AssignFileIDs(filteredFileList);
            foreach (FileNode node in filteredFileList)
            {
                fileFinalList.AddLast(node);
                mNode.AddFiles(node.GetFileID());
            }
            meetingList.AddLast(mNode);
        }

        //Assign IDs to files
        //If the file previously occured in the system, the method will fetch previous Ids
        //If the file is new, the method will assign the latest id to it
        public LinkedList<FileNode> AssignFileIDs(LinkedList<FileNode> fList)
        {
            int llid = trackID;
            LinkedList<FileNode> nList = fList;
            if (!File.Exists(ffile))
            {
                WriteNewFilesFile();
                FileInfo i2 = new FileInfo(ffile);
                while (Tools.IsFileLocked(i2)) { };
            }
            if (!File.Exists(mfile))
            {
                WriteNewMeetingsFile(new LinkedList<MeetingNode>());
                FileInfo i3 = new FileInfo(mfile);
                while (Tools.IsFileLocked(i3)) { };
            }
            XElement filesFile = XElement.Load(ffile);
            IEnumerable<XElement> fileNodes = filesFile.Elements();
            //deal with nodes already exist
            int nodeCount = nList.Count();
            int i = 0;
            foreach (XElement xEle in fileNodes)
            {
                String xEleName = xEle.Element("File_Name").Value;
                String xElePath = xEle.Element("File_Path").Value;
                foreach (FileNode node in nList)
                {
                    if (node.GetFileID() == 0)
                    {
                        String nodePath = node.GetFilePath();
                        String nodeName = node.GetFileName();
                        //if the node name and node path matches with each other -- same file
                        if ((xEleName == nodeName) && (xElePath == nodePath))
                        {
                            node.SetFileID(xEle.Attribute("ID").Value);
                            i = i + 1;
                            break;
                        }
                    }
                }
                if (i == nodeCount)
                {
                    break;
                }
            }

            if (i != nodeCount)
            {
                //check if the node is in final list 
                foreach (FileNode node in nList)
                {
                    foreach (FileNode nNode in fileFinalList)
                    {
                        //if the node is in the final list
                        if ((node.GetFileName() == nNode.GetFileName()) && (node.GetFilePath() == nNode.GetFilePath()))
                        {
                            node.SetFileID(nNode.GetFileID());
                        }
                    }
                    if (node.GetFileID() == 0)
                    {
                        node.SetFileID(llid + 1);
                        llid++;
                    }
                }
            }
            trackID = llid;
            return nList;
        }

        //The method takes a meeting list and an attachment file list, 
        //process the list with the local file list read from the recent files view
        //The method stores the process filelist and meeting filelist
        public void ProcessFileWithMeetingList(LinkedList<MeetingNode> mList, LinkedList<FileNode> attachmentList)
        {
            foreach (MeetingNode m in mList)
            {
            }
            AttachmentList = attachmentList;
            lastUpdate = DateTime.Now;
            foreach (MeetingNode meeting in mList)
            {
                ProcessFileWithMeetingNode(meeting);
            }
        }
        #endregion

        #region Settings.xml Write and Update

        //Initialize a settings file
        public void WriteSettings()
        {
            XElement s = new XElement("Settings",
                new XElement("Working_Path", ""),
                new XElement("Last_Update", "Uninitialized"),
                new XElement("Last_File_ID", "0"),
                new XElement("Minimum_Duration", ""));
            XDocument xDoc = new XDocument(
            new XDeclaration("1.0", "UTF-16", null), s);
            StringWriter sw = new StringWriter();
            XmlWriter xWrite = XmlWriter.Create(sw);
            xDoc.Save(xWrite);
            xWrite.Close();

            // Save to Disk
            xDoc.Save(sfile);
            Console.WriteLine("New Settings.xml Created...");
        }

        //----------------------------------Update Settings File------------------------------
        //Update the last_update_time in the settings file
        //The field indicates the last meeting the program processed
        //Used for next update
        public void UpdateSettingsTime(DateTime time)
        {
            String year = time.Year.ToString("0000");
            String month = time.Month.ToString("00");
            String day = time.Day.ToString("00");
            String hour = time.Hour.ToString("00");
            String minute = time.Minute.ToString("00");
            String second = time.Second.ToString("00");

            String timeS = year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;

            XElement settingsFile = XElement.Load(sfile);
            settingsFile.Element("Last_Update").ReplaceNodes(timeS);
            settingsFile.Save(sfile);
        }

        //Currently this method is not used
        //Intend to be used for let user change file path for data storage
        public void UpdateSettingsPath(String WP)
        {
            workingPath = WP;
            XElement settingsFile = XElement.Load(sfile);
            settingsFile.Element("Working_Path").ReplaceNodes(WP);
            settingsFile.Save(sfile);
        }

        //Get the last file ID in the database
        public void UpdateLastID(int lastID)
        {

            XElement settingsFile = XElement.Load(sfile);
            settingsFile.Element("Last_File_ID").ReplaceNodes(lastID.ToString());
            settingsFile.Save(sfile);
        }

        //Update the minimum duration for "Meeting"
        public void UpdateSettingsDuration(TimeSpan duration)
        {

            minDuration = duration;
            XElement settingsFile = XElement.Load(sfile);
            String s = duration.Hours.ToString() + ";" +
                duration.Minutes.ToString() + ";" +
                duration.Seconds.ToString();
            settingsFile.Element("Minimum_Duration").ReplaceNodes(s);
            settingsFile.Save(sfile);
        }

        //-------------------------------------------------------------------------------------
        #endregion

        //Write the Update into the Database
        public void Write()
        {
            FileInfo i = new FileInfo(mfile);
            FileInfo i2 = new FileInfo(ffile);
            FileInfo i3 = new FileInfo(sfile);

            while (Tools.IsFileLocked(i)) { };
            WriteMeetings(meetingList);

            while (Tools.IsFileLocked(i2)) { };
            WriteFiles(fileFinalList);

            while (Tools.IsFileLocked(i3)) { };
            UpdateSettingsTime(lastUpdate);
            UpdateLastID(lastID);
            Console.WriteLine("Finished...");
        }

        #region Meetings.xml Write and Update
        //A method store the linked list of meetings into the database
        public void WriteMeetings(LinkedList<MeetingNode> mts)
        {
            if (File.Exists(mfile))
            {
                UpdateMeetings(mts);
            }
            else
            {
                WriteNewMeetingsFile(mts);
            }

        }

        //Write a new xml file with name specified in mfile with input list of meeting nodes
        //preassumption: all the attributes are stored in the meeting nodes, and empty string implies null value
        public void WriteNewMeetingsFile(LinkedList<MeetingNode> mts)
        {
            XElement m = new XElement("Meeting");
            foreach (MeetingNode temp in mts)
            {
                XElement mt = new XElement("Meeting_Title", temp.GetMeetingTitle()); //meeting title element
                XElement st = new XElement("Start_Time", temp.GetStartTimeS());
                XElement et = new XElement("End_Time", temp.GetEndTimeS());
                XElement pID = new XElement("Parent_ID", temp.GetParentID());
                XElement atds = new XElement("Attendents", temp.GetAttendents());
                XElement f = new XElement("Files", temp.GetFileListS());
                String mID = temp.GetMeetingID();
                XElement item = new XElement("Item", mt, st, et, pID, atds, f, new XAttribute("ID", mID));
                m.Add(item);
            }
            XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null), m);
            StringWriter sw = new StringWriter();
            XmlWriter xWrite = XmlWriter.Create(sw);
            xDoc.Save(xWrite);
            xWrite.Close();

            // Save to Disk
            xDoc.Save(mfile);
            Console.WriteLine("New Meeting File Saved");
        }

        //Update the current xml file with name specified in mfile with input list of meeting nodes
        //preassumption: all the attributes are stored in the meeting nodes, and empty string implies null value
        public void UpdateMeetings(LinkedList<MeetingNode> mts)
        {
            XElement meetingFile = XElement.Load(mfile);
            foreach (MeetingNode temp in mts)
            {
                XElement mt = new XElement("Meeting_Title", temp.GetMeetingTitle()); //meeting title element
                XElement st = new XElement("Start_Time", temp.GetStartTimeS());
                XElement et = new XElement("End_Time", temp.GetEndTimeS());
                XElement pID = new XElement("Parent_ID", temp.GetParentID());
                XElement atds = new XElement("Attendents", temp.GetAttendents());
                XElement f = new XElement("Files", temp.GetFileListS());
                String mID = temp.GetMeetingID();
                XElement item = new XElement("Item", mt, st, et, pID, atds, f, new XAttribute("ID", mID));
                meetingFile.Add(item);
            }
            meetingFile.Save(mfile);
            Console.WriteLine("Meeting File Updated");
        }

        #endregion

        #region Files.xml Write and Update
        public void WriteFiles(LinkedList<FileNode> fts)
        {
            if (!File.Exists(ffile))
            {
                WriteNewFilesFile();
            }
            UpdateFiles(fts);
        }

        //Initialize a files file with the name specified in xml
        public void WriteNewFilesFile()
        {
            XElement f = new XElement("File");
            XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null), f);
            StringWriter sw = new StringWriter();
            XmlWriter xWrite = XmlWriter.Create(sw);
            xDoc.Save(xWrite);
            xWrite.Close();
            // Save to Disk
            xDoc.Save(ffile);
            Console.WriteLine("New Files.xml Created...");
        }

        //Update the current xml file with name specified in ffile with input list of new file nodes and list of file nodes already existed
        //Preassumption: all the attributes are stored in the file nodes, the new file node list has increasing file id
        public void UpdateFiles(LinkedList<FileNode> fts)
        {
            XElement filesFile = XElement.Load(ffile);
            int currentLast = lastID;
            //deal with nodes already exist
            foreach (FileNode temp in fts)
            {

                if (temp.GetFileID() <= currentLast)
                {
                    //previous node
                    var previousNode = from id in filesFile.Elements("Item")
                                       where (string)id.Attribute("ID") == Convert.ToString(temp.GetFileID())
                                       select id;
                    foreach (XElement pN in previousNode)
                    {

                        pN.Element("File_Name").ReplaceNodes(temp.GetFileName());
                        pN.Element("File_Path").ReplaceNodes(temp.GetFilePath());
                        pN.Element("Modified_Time").ReplaceNodes(temp.GetModifiedTimeS());
                        pN.Element("Execute_Time").ReplaceNodes(temp.GetExecuteTimeS());
                        pN.Element("Missing").ReplaceNodes(temp.GetMissing().ToString());
                        if (pN.Element("Meetings").Value == "")
                        {
                            pN.Element("Meetings").ReplaceNodes(temp.GetMeetingListS());
                        }
                        else if (temp.GetMeetingListS() != "")
                        {
                            String s = pN.Element("Meetings").Value + ";" + temp.GetMeetingListS();
                            pN.Element("Meetings").ReplaceNodes(s);
                        }
                    }
                }
                else
                {
                    //new Node
                    XElement fn = new XElement("File_Name", temp.GetFileName()); //meeting title element
                    XElement fp = new XElement("File_Path", temp.GetFilePath());
                    XElement ct = new XElement("Created_Time", temp.GetCreatedTimeS());
                    XElement mt = new XElement("Modified_Time", temp.GetModifiedTimeS());
                    XElement et = new XElement("Execute_Time", temp.GetExecuteTimeS());
                    XElement ext = new XElement("Extension", temp.GetExtension());
                    XElement mis = new XElement("Missing", temp.GetMissing().ToString());
                    XElement ms = new XElement("Meetings", temp.GetMeetingListS());
                    XAttribute fID = new XAttribute("ID", temp.GetFileID());
                    XElement item = new XElement("Item", fn, fp, ct, mt, et, ext, mis, ms, fID);
                    currentLast = temp.GetFileID();
                    filesFile.Add(item);
                }
            }
            filesFile.Save(ffile);
            lastID = currentLast;
            Console.WriteLine("Files.xml Updated...");
        }
        #endregion

  
        //------------------------------Testing Area--------------------------------------------------
        //----------------------------For Developer Only -------------------------------------------------

        //For Convenient Testing Only
        public void SetFileList(LinkedList<FileNode> testList)
        {
            fileList = testList;
        }

        //For Convenient Testing Only
        public static void Main(string[] args)
        {
        }
    }
}

