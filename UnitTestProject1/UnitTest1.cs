using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        string output_path = "../../../../test_output.txt";

        [TestMethod]
        public void Test_Height_List()
        {
            string test_name = "Test_Height_List";
            int expected = 13;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.WriteAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Height_Dictionary()
        {
            string test_name = "Test_Height_Dictionary";
            int expected = 10;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_List_Of_Lists()
        {
            string test_name = "Test_Depth_List_Of_Lists";
            int expected = 10;
            int actual = 13;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_List_Of_Dicts()
        {
            string test_name = "Test_Depth_List_Of_Dicts";
            int expected = 10;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_List_Of_Mix()
        {
            string test_name = "Test_Depth_List_Of_Mix";
            int expected = 10;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_Dict_Of_Lists()
        {
            string test_name = "Test_Depth_Dict_Of_Lists";
            int expected = 10;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_Dict_Of_Dicts()
        {
            string test_name = "Test_Depth_Dict_Of_Dicts";
            int expected = 10;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_Dict_Of_Mix()
        {
            string test_name = "Test_Depth_Dict_Of_Mix";
            int expected = 10;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

    }
}

