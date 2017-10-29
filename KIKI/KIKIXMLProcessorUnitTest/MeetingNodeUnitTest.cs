using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KIKIXmlProcessor;


namespace KIKIXMLProcessorUnitTest
{
    [TestClass]
    public class MeetingNodeUnitTest
    {
        String mTitle = "EECS395";
        String mID = "05_1";
        String sTime = "2017/04/01 17:30:05";
        String eTime = "2017/04/01 19:10:05";
        String PID = "05";
        String Attendents = "Steven, Eddie, Xiaoying";

        [TestMethod]
        public void GetandSetMeetingTitleTest()
        {
            MeetingNode meeting0 = new MeetingNode("", mID, sTime, eTime, PID, Attendents);
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            String actualmTitle0 = meeting0.GetMeetingTitle();
            Assert.AreEqual("", actualmTitle0, "Actual meeting title not equal to empty");

            //test get
            String actualmTitle1 = meeting.GetMeetingTitle();
            Assert.AreEqual(mTitle, actualmTitle1, "Actual meeting title not equal to mTitle");

            //test set string
            meeting.SetMeetingTitle("ECON 201");
            String actualmTitle2 = meeting.GetMeetingTitle();
            Assert.AreEqual("ECON 201", actualmTitle2, "Actual meeting title not equal to new mTitle");

            //test set int
            meeting.SetMeetingTitle(201);
            String actualmTitle3 = meeting.GetMeetingTitle();
            Assert.AreEqual("201", actualmTitle3, "Actual meeting title not equal to new mTitle 201");
        }

        [TestMethod]
        public void GetandSetMeetingID()
        {
            MeetingNode meeting0 = new MeetingNode(mTitle, "", sTime, eTime, PID, Attendents);
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            String actualmID0 = meeting0.GetMeetingID();
            Assert.AreEqual("", actualmID0, "Actual meeting ID not equal to empty");

            //test get
            String actualmID1 = meeting.GetMeetingID();
            Assert.AreEqual(mID, actualmID1, "Actual meeting ID not equal to mID");

            //test set string
            meeting.SetMeetingID("05_2");
            String actualmID2 = meeting.GetMeetingID();
            Assert.AreEqual("05_2", actualmID2, "Actual meeting ID not equal to 05_2");

            //test set int
            meeting.SetMeetingID(52);
            String actualmID3 = meeting.GetMeetingID();
            Assert.AreEqual("52", actualmID3, "Actual meeting ID not equal to 52");
        }

        [TestMethod]
        public void GetandSetStartTimeTest()
        {
            MeetingNode meeting0 = new MeetingNode(mTitle, mID, "0001/01/01 00:00:00", eTime, PID, Attendents);
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            String actualsTime0 = meeting0.GetStartTimeS();
            Assert.AreEqual("", actualsTime0, "Actual meeting start time does not equal to the minimized value");

            //test get
            String actualsTime1 = meeting.GetStartTimeS();
            Assert.AreEqual(sTime, actualsTime1, "Actual meeting start time does not equal to 2017/04/01 17:30:05");

            //test set
            meeting.SetStartTime("2017/04/02 12:30:00");
            String actualsTime2 = meeting.GetStartTimeS();
            Assert.AreEqual("2017/04/02 12:30:00", actualsTime2, "Actual meeting start time does not equal to 2017/04/02 12:30:00");
        }

        [TestMethod]
        public void GetandSetEndTimeTest()
        {
            MeetingNode meeting0 = new MeetingNode(mTitle, mID, sTime, "0001/01/01 00:00:00", PID, Attendents);
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            String actualsTime0 = meeting0.GetEndTimeS();
            Assert.AreEqual("", actualsTime0, "Actual meeting end time does not equal to empty, Actual:" + actualsTime0);

            //test get
            String actualsTime1 = meeting.GetEndTimeS();
            Assert.AreEqual(eTime, actualsTime1, "Expected: " + eTime + "Actual: " + actualsTime1);

            //test set
            meeting.SetEndTime("2017/04/02 18:00:30");
            String actualsTime2 = meeting.GetEndTimeS();
            Assert.AreEqual("2017/04/02 18:00:30", actualsTime2, "Actual meeting end time does not equal to 2017/04/02 18:00:30");
        }

        [TestMethod]
        public void GetDurationTest()
        {
            MeetingNode meeting0 = new MeetingNode(mTitle, mID, "0001/01/01 00:00:00", "0001/01/01 00:00:00", PID, Attendents);
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            TimeSpan expectedspan0 = new TimeSpan(0, 0, 0, 0);
            TimeSpan actualspan0 = meeting0.GetDuration();
            Assert.AreEqual(expectedspan0, actualspan0, "Actual duration does not equal to empty");

            //test get less than 1 day
            TimeSpan expectedspan1 = new TimeSpan(0, 1, 40, 0);
            TimeSpan actualspan1 = meeting.GetDuration();
            Assert.AreEqual(expectedspan1, actualspan1, "Actual duration does not equal to 0 day 1 hour 40 mins 0 second");

            //test get more than 1 day
            MeetingNode meeting2 = new MeetingNode(mTitle, mID, "2017/04/02 14:00:00", "2017/4/3 19:30:00", PID, Attendents);
            TimeSpan expectedspan2 = new TimeSpan(1, 5, 30, 0);
            TimeSpan actualspan2 = meeting2.GetDuration();
            Assert.AreEqual(expectedspan2, actualspan2, "Actual duration does not equal to 1 day 5 hour 30 mins 0 second");
        }

        [TestMethod]
        public void GetandSetParentIDTest()
        {
            MeetingNode meeting0 = new MeetingNode(mTitle, mID, sTime, eTime, "", Attendents);
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            String actualPID0 = meeting0.GetParentID();
            Assert.AreEqual("", actualPID0, "Actual Parent ID not equal to empty");

            //test get
            String actualPID1 = meeting.GetParentID();
            Assert.AreEqual(PID, actualPID1, "Actual meeting ID not equal to PID");

            //test set string
            meeting.SetParentID("01");
            String actualPID2 = meeting.GetParentID();
            Assert.AreEqual("01", actualPID2, "Actual meeting ID not equal to 01");

            //test set int
            meeting.SetParentID(1);
            String actualPID3 = meeting.GetParentID();
            Assert.AreEqual("1", actualPID3, "Actual meeting ID not equal to 1");
        }

        [TestMethod]
        public void GetandSetAttendentsTest()
        {
            MeetingNode meeting0 = new MeetingNode(mTitle, mID, sTime, eTime, PID, "");
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            String actualAttend0 = meeting0.GetAttendents();
            Assert.AreEqual("", actualAttend0, "Actual Attendents are not empty");

            //test get
            String actualAttend1 = meeting.GetAttendents();
            Assert.AreEqual(Attendents, actualAttend1, "Actual attendents are not Steven, Eddie, Xiaoying");

            //test set
            meeting.SetAttendents("Harris, John");
            String actualAttend2 = meeting.GetAttendents();
            Assert.AreEqual("Harris, John", actualAttend2, "Actual attendents are not Harris, John");

        }

        [TestMethod]
        public void SetFilesTest()
        {
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test empty
            meeting.SetFiles("");
            String actualFileList0 = meeting.GetFileListS();
            Assert.AreEqual("", actualFileList0, "The actual file list is not empty");

            //test set single element
            meeting.SetFiles("1");
            String actualFileList1 = meeting.GetFileListS();
            Assert.AreEqual("1", actualFileList1, "The actual file list does not contain 1");

            //test set multiple element
            meeting.SetFiles("2;3;4");
            String actualFileList2 = meeting.GetFileListS();
            Assert.AreEqual(3, meeting.FileList.Count, "The actual file list does not contain 3 elements");
            Assert.AreEqual("2;3;4", actualFileList2, "The actual file list does not contain 2;3;4");
        }

        [TestMethod]
        public void GetandAddFileListTest()
        {
            MeetingNode meeting0 = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);
            MeetingNode meeting = new MeetingNode(mTitle, mID, sTime, eTime, PID, Attendents);

            //test get 0 GetFileList
            meeting0.AddFiles(0);
            LinkedList<Int32> actualFileList0 = meeting0.GetFileList();
            LinkedList<Int32> expectedFileList0 = new LinkedList<Int32>();
            expectedFileList0.AddLast(0);
            Assert.AreEqual(expectedFileList0.Last.Value, actualFileList0.Last.Value, "Actual FileList does not contain 0, Expected: {0}, Actual: {0}.", expectedFileList0.Last, actualFileList0.Last);

            //test get 0 GetFileListS
            String acutualFileID01 = meeting0.GetFileListS();
            LinkedList<Int32> expectedFileList01 = new LinkedList<Int32>();
            expectedFileList01.AddFirst(0);
            String expectedFileID01 = expectedFileList01.First.Value.ToString();
            Assert.AreEqual(expectedFileID01, acutualFileID01, "Actual First FileID does not equal to 0, Expected: " + expectedFileID01 + " Actual: " + acutualFileID01);

            //test get GetFileList
            meeting.AddFiles(1);
            LinkedList<Int32> actualFileList2 = meeting.GetFileList();
            LinkedList<Int32> expectedFileList2 = new LinkedList<Int32>();
            expectedFileList2.AddFirst(1);
            Assert.AreEqual(expectedFileList2.First.Value, actualFileList2.First.Value, "Actual FileList does not contain 1");

            //test get GetFileListS
            String acutualFileID02 = meeting.GetFileListS();
            LinkedList<Int32> expectedFileList02 = new LinkedList<Int32>();
            expectedFileList02.AddFirst(1);
            String expectedFileID02 = expectedFileList02.First.Value.ToString();
            Assert.AreEqual(expectedFileID02, acutualFileID02, "Actual First FileID does not equal to 1");

            //test add Filelist
            meeting.AddFiles(2);
            meeting.AddFiles(3);
            LinkedList<Int32> actualFileList3 = meeting.GetFileList();
            LinkedList<Int32> expectedFileList3 = new LinkedList<Int32>();
            expectedFileList3.AddLast(1);
            expectedFileList3.AddLast(2);
            expectedFileList3.AddLast(3);
            for (int n = 0; n < actualFileList3.Count; n++)
            {
                if (n == 0)
                {
                    Assert.AreEqual(expectedFileList3.First.Value, actualFileList3.First.Value, "1st element in two lists are not equal");
                }
                if (n == 1)
                {
                    Assert.AreEqual(expectedFileList3.First.Next.Value, actualFileList3.First.Next.Value, "2nd element in two lists are not equal");
                }
                if (n == 2)
                {
                    Assert.AreEqual(expectedFileList3.First.Next.Next.Value, actualFileList3.First.Next.Next.Value, "3rd element in two lists are not equal");
                }
            }

            //test set Filelist
            meeting0.FileList.Clear();
            meeting0.SetFiles("1;2;3");
            LinkedList<Int32> actualFileList4 = meeting0.GetFileList();
            LinkedList<Int32> expectedFileList4 = new LinkedList<Int32>();
            expectedFileList4.AddLast(1);
            expectedFileList4.AddLast(2);
            expectedFileList4.AddLast(3);
            for (int n = 0; n < actualFileList4.Count; n++)
            {
                if (n == 0)
                {
                    Assert.AreEqual(expectedFileList4.First.Value, actualFileList4.First.Value, "1st element in two lists are not equal");
                }
                if (n == 1)
                {
                    Assert.AreEqual(expectedFileList4.First.Next.Value, actualFileList4.First.Next.Value, "2nd element in two lists are not equal");
                }
                if (n == 2)
                {
                    Assert.AreEqual(expectedFileList4.First.Next.Next.Value, actualFileList4.First.Next.Next.Value, "3rd element in two lists are not equal");
                }
            }

        }
    }
}