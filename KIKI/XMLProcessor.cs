using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace KIKIXmlProcessor
{
    class XMLProcessor
    {
        private String mfile = "meetings.xml";
        private String ffile = "files.xml";
        private String workingPath = "";
        private String tempPath = "temp.xml";
        private String RSFile = "records.xml";
        private LinkedList<FileNode> fileList = new LinkedList<FileNode>();
        Byte sync = 0;

        public XMLProcessor()
        {
        }

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
                sync = 0;
                return false;

            }
            Console.WriteLine("Fetching Finished...");
            return true;
        }

        public void ReadRecords()
        {
            Console.WriteLine("Program start reading recent file history...");
            XmlTextReader reader = null;

            try
            {

                // Load the reader with the data file and ignore all white space nodes.         
                reader = new XmlTextReader(RSFile);
                sync = 1;
                int filter = 0; //the bit used to filter out entries
                reader.WhitespaceHandling = WhitespaceHandling.None;
                String fileName = "";
                String extension = "";
                String filePath = "";
                String mTime = "";
                String cTime = "";
                String eTime = "";
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
                            //Console.WriteLine("<{0}>", reader.Name);
                            break;
                        case XmlNodeType.Text:
                            if (tag == 1) { filePath = reader.Value; } // full name of the file indicate the path of the file
                            if (tag == 8) { fileName = reader.Value; } //the file_only property indicate the name of the file
                            if (tag == 7) { extension = reader.Value; } // the extension of the file
                            if (tag == 2) { mTime = reader.Value; } //modified Time
                            if (tag == 3) { cTime = reader.Value; } //created Time
                            if (tag == 4) { eTime = reader.Value; } //execute Time
                            //Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            if ((filter == 0) && (reader.Name == "item"))
                            {
                                if (IsValid(fileName, filePath, extension, mTime, cTime, eTime))
                                {
                                    FileNode tempNode = new FileNode();
                                    tempNode.SetFileName(fileName);
                                    LinkedListNode<FileNode> node = new LinkedListNode<FileNode>(tempNode);
                                    // Console.WriteLine(tempNode.GetFilename());
                                    fileList.AddLast(node);
                                }

                            }
                            // Console.WriteLine("</{0}>", reader.Name);
                            break;
                        default:
                            break;
                            /*
                            case XmlNodeType.CDATA:
                                Console.WriteLine("3");
                                Console.WriteLine("<![CDATA[{0}]]>", reader.Value);
                                break;
                            case XmlNodeType.ProcessingInstruction:
                                Console.WriteLine("4");
                                Console.WriteLine("<?{0} {1}?>", reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                Console.WriteLine("5");
                                Console.WriteLine("<!--{0}-->", reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                                Console.WriteLine("6");
                                Console.WriteLine("<?xml version='1.0'?>");
                                break;
                            case XmlNodeType.Document:
                                Console.WriteLine("7");
                                break;
                            case XmlNodeType.DocumentType:
                                Console.WriteLine("8");
                                Console.WriteLine("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
                                break;
                            case XmlNodeType.EntityReference:
                                Console.WriteLine("9");
                                Console.WriteLine(reader.Name);
                                break;*/

                    }
                }
            }

            finally
            {
                if (reader != null)
                    reader.Close();
            }
            sync = 0;
            Console.WriteLine("Reading Finished...");
        }

        public void RemoveXMLdeclaration()
        {
            Console.WriteLine("Program start formatting raw recent file data...");
            sync = 1;
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
            File.Delete(tempPath);
            Console.WriteLine("Fomatting Finished...");
            sync = 0;
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

        //filter, checking the validity of the information
        public Boolean IsValid(String fName, String fPath, String ext, String mTime, String cTime, String eTime)
        {
            if (fName == "")
            {
                return false;
            }
            if (ext == "")
            {
                return false;
            }
            return true;
        }

        //for test main method
        public static void Main(string[] args)
        {

            XMLProcessor x = new XMLProcessor();
            x.GetRFXML(); //date and time not implemented 
            while (!File.Exists(x.tempPath)) ;
            Thread.Sleep(1000); //temporary solution for process conflicts
            x.RemoveXMLdeclaration();
            while (x.sync == 1) ;
            x.ReadRecords();

            for (LinkedListNode<FileNode> node = x.fileList.First; node != x.fileList.Last.Next; node = node.Next)
            {
                Console.WriteLine(node.Value.GetFileName());

            }


        }
    }
}
