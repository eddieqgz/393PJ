using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KIKIXmlProcessor;

namespace KIKIXMLProcessorUnitTest
{
    [TestClass]
    public class FileNodeUnitTest
    {
        String fileName = "test.txt";
        String fileID = "1";
        String modifiedTime = "2017/04/01 03:00:00";
        String createdTime = "2017/04/01 01:00:00";
        String executeTime = "2017/04/01 05:00:00";
        String extension = ".txt";
        String filePath = "D:/EECS395";

        [TestMethod]
        public void GetandSetFileNameTest()
        {
            FileNode file0 = new FileNode("", fileID, modifiedTime, createdTime, executeTime, extension, filePath);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test get empty string
            String actualfileName0 = file0.GetFileName();
            Assert.AreEqual("", actualfileName0, "Actual file name is not an empty string");

            //test get non-empty string
            String actualfileName1 = file.GetFileName();
            Assert.AreEqual("test.txt", actualfileName1, "Actual file name does not equal to text.txt");

            //test set empty string
            file.SetFileName("");
            String actualfileName2 = file.GetFileName();
            Assert.AreEqual("", actualfileName2, "Set empty string file name was not successful");

            //test set non-empty string
            file.SetFileName("exam.pdf");
            String actualfileName3 = file.GetFileName();
            Assert.AreEqual("exam.pdf", actualfileName3, "Set non-empty string file name was not successful");
        }

        [TestMethod]
        public void GetandSetFileIDTest()
        {
            FileNode file0 = new FileNode(fileName, "0", modifiedTime, createdTime, executeTime, extension, filePath);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test get 0
            Int32 actualfileID0 = file0.GetFileID();
            Assert.AreEqual(0, actualfileID0, "Actual file ID does not equal to 0");

            //test get non-0 integer
            Int32 actualfileID1 = file.GetFileID();
            Assert.AreEqual(1, actualfileID1, "Actual file ID does not equal to 1");

            //test set int input of 0
            file.SetFileID(0);
            Int32 actualfileID2 = file.GetFileID();
            Assert.AreEqual(0, actualfileID2, "Set method using 0 as input failed. Actual file ID does not equal to 0");

            //test set int input of non-0 integer
            file.SetFileID(2);
            Int32 actualfileID3 = file.GetFileID();
            Assert.AreEqual(2, actualfileID3, "Set method using non-0 int input failed. Actual file ID does not equal to 2");

            //test set string input of 0 value
            file.SetFileID("0");
            Int32 actualfileID4 = file.GetFileID();
            Assert.AreEqual(0, actualfileID4, "Set method using string input of 0 value failed. Actual file ID does not equal to 0");

            //test set string input of non-0 value
            file.SetFileID("3");
            Int32 actualfileID5 = file.GetFileID();
            Assert.AreEqual(3, actualfileID5, "Set method using string input of non-0 value failed. Actual file ID does not equal to 3");
        }

        [TestMethod]
        public void GetandSetFilePathTest()
        {
            FileNode file0 = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, "");
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test get empty string
            String actualfilePath0 = file0.GetFilePath();
            Assert.AreEqual("", actualfilePath0, "Actual file path is not an empty string");

            //test get non-empty string
            String actualfilePath1 = file.GetFilePath();
            Assert.AreEqual("D:/EECS395", actualfilePath1, "Actual file path does not equal to D:/EECS395");

            //test set empty string
            file.SetFilePath("");
            String actualfilePath2 = file.GetFilePath();
            Assert.AreEqual("", actualfilePath2, "Set empty string path was not successful");

            //test set non-empty string
            file.SetFilePath("C:/MKMR201");
            String actualfilePath3 = file.GetFilePath();
            Assert.AreEqual("C:/MKMR201", actualfilePath3, "Set non-empty string path was not successful");
        }

        [TestMethod]
        public void GetandSetExtensionTest()
        {
            FileNode file0 = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, "", filePath);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test get empty string
            String actualExtension0 = file0.GetExtension();
            Assert.AreEqual("", actualExtension0, "Actual file extension is not an empty string");

            //test get non-empty string
            String actualExtension1 = file.GetExtension();
            Assert.AreEqual(".txt", actualExtension1, "Actual file extension does not equal to .txt");

            //test set empty string
            file.SetExtension("");
            String actualExtension2 = file.GetExtension();
            Assert.AreEqual("", actualExtension2, "Set empty string extension was not successful");

            //test set non-empty string
            file.SetExtension(".pdf");
            String actualExtension3 = file.GetExtension();
            Assert.AreEqual(".pdf", actualExtension3, "Set non-empty string extension was not successful");
        }

        [TestMethod]
        public void GetandSetModifiedTimeTest()
        {
            DateTime min = DateTime.MinValue;
            FileNode file_na = new FileNode(fileName, fileID, "N / A", createdTime, executeTime, extension, filePath);
            FileNode file01 = new FileNode(fileName, fileID, "0001/01/01 00:00:00", createdTime, executeTime, extension, filePath);
            FileNode file02 = new FileNode(fileName, fileID, min, min, min, extension, filePath);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test when input is "N / A"
            String actualmodifiedTime_na = file_na.GetModifiedTimeS();
            Assert.AreEqual("", actualmodifiedTime_na, "Actual modified time is not empty when input is N / A");

            //test get method when input is string-type min value
            String actualmodifiedTime01 = file01.GetModifiedTimeS();
            Assert.AreEqual("", actualmodifiedTime01, "Actual modified time is not empty when input is 0001/01/01 00:00:00");

            //test get method when input is DateTime-type min value
            String actualmodifiedTime02 = file02.GetModifiedTimeS();
            Assert.AreEqual("", actualmodifiedTime02, "Actual modified time is not empty when input is 0001/01/01 00:00:00");

            //test get method when input is not min value or "N / A"
            String actualmodifiedTime1 = file.GetModifiedTimeS();
            Assert.AreEqual("2017/04/01 03:00:00", actualmodifiedTime1, "Actual modified time is not 2017/04/01 03:00:00");

            //test set method when input is empty string
            file.SetModifiedTime("");
            String actualmodifiedTime2 = file.GetModifiedTimeS();
            Assert.AreEqual("", actualmodifiedTime2, "Actual modified time is not empty when the modified time is set to be empty string");

            //test set method when input is "N / A"
            file.SetModifiedTime("N / A");
            String actualmodifiedTime3 = file.GetModifiedTimeS();
            Assert.AreEqual("", actualmodifiedTime3, "Actual modified time is not empty when the modified time is set to be N / A");

            //test set method when input is MinValue
            file.SetModifiedTime("0001/01/01 00:00:00");
            String actualmodifiedTime4 = file.GetModifiedTimeS();
            Assert.AreEqual("", actualmodifiedTime4, "Actual modified time is not empty when the modified time is set to be MinValue");

            //test set method when input is not MinValue
            file.SetModifiedTime("2016/12/01 10:00:00");
            String actualmodifiedTime5 = file.GetModifiedTimeS();
            Assert.AreEqual("2016/12/01 10:00:00", actualmodifiedTime5, "Actual modified time is not 2016/12/01 10:00:00");
        }

        [TestMethod]
        public void GetandSetCreatedTimeTest()
        {
            DateTime min = DateTime.MinValue;
            FileNode file_na = new FileNode(fileName, fileID, modifiedTime, "N / A", executeTime, extension, filePath);
            FileNode file01 = new FileNode(fileName, fileID, modifiedTime, "0001/01/01 00:00:00", executeTime, extension, filePath);
            FileNode file02 = new FileNode(fileName, fileID, min, min, min, extension, filePath);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test when input is "N / A"
            String actualcreatedTime_na = file_na.GetCreatedTimeS();
            Assert.AreEqual("", actualcreatedTime_na, "Actual created time is not empty when input is N / A");

            //test get method when input is string-type min value
            String actualcreatedTime01 = file01.GetCreatedTimeS();
            Assert.AreEqual("", actualcreatedTime01, "Actual created time is not empty when input is 0001/01/01 00:00:00");

            //test get method when input is DateTime-type min value
            String actualcreatedTime02 = file02.GetCreatedTimeS();
            Assert.AreEqual("", actualcreatedTime02, "Actual created time is not empty when input is 0001/01/01 00:00:00");

            //test get method when input is not min value or "N / A"
            String actualcreatedTime1 = file.GetCreatedTimeS();
            Assert.AreEqual("2017/04/01 01:00:00", actualcreatedTime1, "Actual created time is not 2017/04/01 01:00:00");

            //test set method when input is empty string
            file.SetCreatedTime("");
            String actualcreatedTime2 = file.GetCreatedTimeS();
            Assert.AreEqual("", actualcreatedTime2, "Actual created time is not empty when the created time is set to be empty string");

            //test set method when input is "N / A"
            file.SetCreatedTime("N / A");
            String actualcreatedTime3 = file.GetCreatedTimeS();
            Assert.AreEqual("", actualcreatedTime3, "Actual created time is not empty when the created time is set to be N / A");

            //test set method when input is MinValue
            file.SetCreatedTime("0001/01/01 00:00:00");
            String actualcreatedTime4 = file.GetCreatedTimeS();
            Assert.AreEqual("", actualcreatedTime4, "Actual created time is not empty when the created time is set to be MinValue");

            //test set method when input is not MinValue
            file.SetCreatedTime("2016/11/30 18:00:00");
            String actualcreatedTime5 = file.GetCreatedTimeS();
            Assert.AreEqual("2016/11/30 18:00:00", actualcreatedTime5, "Actual created time is not 2016/11/30 18:00:00");
        }

        [TestMethod]
        public void GetandSetExecuteTimeTest()
        {
            DateTime min = DateTime.MinValue;
            FileNode file_na = new FileNode(fileName, fileID, modifiedTime, createdTime, "N / A", extension, filePath);
            FileNode file01 = new FileNode(fileName, fileID, modifiedTime, createdTime, "0001/01/01 00:00:00", extension, filePath);
            FileNode file02 = new FileNode(fileName, fileID, min, min, min, extension, filePath);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test when input is "N / A"
            String actualexecuteTime_na = file_na.GetExecuteTimeS();
            Assert.AreEqual("", actualexecuteTime_na, "Actual execute time is not empty when input is N / A");

            //test get method when input is string-type min value
            String actualexecuteTime01 = file01.GetExecuteTimeS();
            Assert.AreEqual("", actualexecuteTime01, "Actual execute time is not empty when input is 0001/01/01 00:00:00");

            //test get method when input is DateTime-type min value
            String actualexecuteTime02 = file02.GetExecuteTimeS();
            Assert.AreEqual("", actualexecuteTime02, "Actual execute time is not empty when input is 0001/01/01 00:00:00");

            //test get method when input is not min value or "N / A"
            String actualexecuteTime1 = file.GetExecuteTimeS();
            Assert.AreEqual("2017/04/01 05:00:00", actualexecuteTime1, "Actual execute time is not 2017/04/01 05:00:00");

            //test set method when input is empty string
            file.SetExecuteTime("");
            String actualexecuteTime2 = file.GetExecuteTimeS();
            Assert.AreEqual("", actualexecuteTime2, "Actual execute time is not empty when the execute time is set to be empty string");

            //test set method when input is "N / A"
            file.SetExecuteTime("N / A");
            String actualexecuteTime3 = file.GetExecuteTimeS();
            Assert.AreEqual("", actualexecuteTime3, "Actual execute time is not empty when the execute time is set to be N / A");

            //test set method when input is MinValue
            file.SetExecuteTime("0001/01/01 00:00:00");
            String actualexecuteTime4 = file.GetExecuteTimeS();
            Assert.AreEqual("", actualexecuteTime4, "Actual execute time is not empty when the execute time is set to be MinValue");

            //test set method when input is not MinValue
            file.SetExecuteTime("2016/12/10 12:00:00");
            String actualexecuteTime5 = file.GetExecuteTimeS();
            Assert.AreEqual("2016/12/10 12:00:00", actualexecuteTime5, "Actual execute time is not 2016/12/10 12:00:00");

        }

        [TestMethod]
        public void GetandSetMissingTest()
        {
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test get initialized true
            Boolean actualMissing1 = file.GetMissing();
            Assert.IsTrue(actualMissing1, "Actual missing is not true");

            //test set No
            file.SetMissing("No");
            Boolean actualMissing2 = file.GetMissing();
            Assert.IsFalse(actualMissing2, "Actual missing is not false");

            //test set Yes
            file.SetMissing("Yes");
            Boolean actualMissing3 = file.GetMissing();
            Assert.IsTrue(actualMissing3, "Actual missing is not true");

            //test set other string
            file.SetMissing("Maybe");
            Boolean actualMissing4 = file.GetMissing();
            Assert.IsFalse(actualMissing4, "Actual missing is not false");
        }

        [TestMethod]
        public void GetandAddMeetingListTest()
        {
            FileNode file0 = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test get empty list
            String actualmeetingList0 = file0.GetMeetingListS();
            Assert.AreEqual("", actualmeetingList0, "The actual meeting list is not empty");

            //test add string value
            file0.AddMeetings("01");
            String actualmeetingList2 = file0.GetMeetingListS();
            Assert.AreEqual("01", actualmeetingList2, "The string value 01 was not added into the list successfully");

            //test add int value
            file.AddMeetings(2);
            String actualmeetingList3 = file.GetMeetingListS();
            Assert.AreEqual("2", actualmeetingList3, "The int value 2 was not added into the list successfully");

            //test add multiple values
            file.AddMeetings("3");
            file.AddMeetings("4");
            LinkedList<String> actualmeetingList4 = file.GetMeetingList();
            LinkedList<String> expectedmeetingList4 = new LinkedList<String>();
            expectedmeetingList4.AddLast("2");
            expectedmeetingList4.AddLast("3");
            expectedmeetingList4.AddLast("4");
            for (int n = 0; n < actualmeetingList4.Count; n++)
            {
                if (n == 0)
                {
                    Assert.AreEqual(expectedmeetingList4.First.Value, actualmeetingList4.First.Value, "1st element in two lists are not equal");
                }
                if (n == 1)
                {
                    Assert.AreEqual(expectedmeetingList4.First.Next.Value, actualmeetingList4.First.Next.Value, "2nd element in two lists are not equal");
                }
                if (n == 2)
                {
                    Assert.AreEqual(expectedmeetingList4.First.Next.Next.Value, actualmeetingList4.First.Next.Next.Value, "3rd element in two lists are not equal");
                }
            }
        }

        [TestMethod]
        public void SetMeetingsTest()
        {
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //test set single element
            file.SetMeetings("2");
            LinkedList<String> actualmeetingList0 = file.GetMeetingList();
            String actualmeetingString0 = file.GetMeetingListS();
            LinkedList<String> expectedmeetingList0 = new LinkedList<String>();
            expectedmeetingList0.AddLast("2");
            Assert.AreEqual("2", actualmeetingString0, "Actual meeting list is not 2");
            Assert.AreEqual(expectedmeetingList0.Count, actualmeetingList0.Count, "the length of two lists are not equal to 1");
            Assert.AreEqual(expectedmeetingList0.First.Value, actualmeetingList0.First.Value, "the only element in two lists are not equal");

            //test set multiple elements
            file.SetMeetings("3;4;5");
            LinkedList<String> actualmeetingList1 = file.GetMeetingList();
            String actualmeetingString1 = file.GetMeetingListS();
            LinkedList<String> expectedmeetingList1 = new LinkedList<String>();
            expectedmeetingList1.AddLast("3");
            expectedmeetingList1.AddLast("4");
            expectedmeetingList1.AddLast("5");
            Assert.AreEqual("3;4;5", actualmeetingString1, "Actual meeting list is not 3;4;5");
            Assert.AreEqual(expectedmeetingList1.Count, actualmeetingList1.Count, "the length of two lists are not equal to 3");
            for (int n = 0; n < actualmeetingList1.Count; n++)
            {
                if (n == 0)
                {
                    Assert.AreEqual(expectedmeetingList1.First.Value, actualmeetingList1.First.Value, "1st element in two lists are not equal");
                }
                if (n == 1)
                {
                    Assert.AreEqual(expectedmeetingList1.First.Next.Value, actualmeetingList1.First.Next.Value, "2nd element in two lists are not equal");
                }
                if (n == 2)
                {
                    Assert.AreEqual(expectedmeetingList1.First.Next.Next.Value, actualmeetingList1.First.Next.Next.Value, "3rd element in two lists are not equal");
                }
            }
        }

        [TestMethod]
        public void StringToTimeTest()
        {
            String str1 = "N / A";
            String str2 = "";
            String str3 = "2016/05/01 10:00:00";
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //Test "N / A"
            DateTime actualDT1 = Tools.StringToTime(str1);
            DateTime expectedDT1 = DateTime.MinValue;
            Assert.AreEqual(expectedDT1, actualDT1, "Actual DateTime does not equal to MinValue when input is N / A");

            //Test ""
            DateTime actualDT2 = Tools.StringToTime(str2);
            DateTime expectedDT2 = DateTime.MinValue;
            Assert.AreEqual(expectedDT2, actualDT2, "Actual DateTime does not equal to MinValue when input is empty string");

            //Test ""
            DateTime actualDT3 = Tools.StringToTime(str3);
            DateTime expectedDT3 = new DateTime(2016, 05, 01, 10, 00, 00);
            Assert.AreEqual(expectedDT3, actualDT3, "Actual DateTime does not equal to 2016/05/01 10:00:00");
        }

        [TestMethod]
        public void TimeToStringTest()
        {
            DateTime DT0 = DateTime.MinValue;
            DateTime DT1 = new DateTime(2016, 12, 12, 12, 12, 12);
            FileNode file = new FileNode(fileName, fileID, modifiedTime, createdTime, executeTime, extension, filePath);

            //Test MinValue
            String actualStringDT0 = Tools.TimeToString(DT0);
            String expectedStringDT0 = "";
            Assert.AreEqual(expectedStringDT0, actualStringDT0, "Actual String-type DateTime does not equal to empty string when input is MinValue");

            //Test not MinValue
            String actualStringDT1 = Tools.TimeToString(DT1);
            String expectedStringDT1 = "2016/12/12 12:12:12";
            Assert.AreEqual(expectedStringDT1, actualStringDT1, "Actual String-type DateTime does not equal to 2016/12/12 12:12:12");
        }
    }
}