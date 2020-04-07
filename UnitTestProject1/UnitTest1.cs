using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace UnitTestProject1
{
    public partial class MainWindow
    {
        public int get_height(string jsonFILE)
        {
            using (StreamReader r = new StreamReader(jsonFILE))
            {
                string json = r.ReadToEnd();
                JObject items = (JObject)JsonConvert.DeserializeObject(json);
                return items.Count;
            }
        }

        public int get_depth(string jsonFILE)
        {
            int count = 0;
            using (StreamReader r = new StreamReader(jsonFILE))
            {
                string json = r.ReadToEnd();
                JObject items = (JObject)JsonConvert.DeserializeObject(json);

                foreach (JObject child in items.Children())
                {
                    if (child.Count > count)
                    {
                        count = child.Count + 1;
                    }
                }
            }

            return count;
        }

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("test.json"))
            {
                string json = r.ReadToEnd();
                JObject items = (JObject)JsonConvert.DeserializeObject(json);

            }
        }



    }

    [TestClass]
    public class UnitTest1
    {
        string output_path = "../../../../test_output.txt";
        string test_json_dir = "../../../../test_json/";

        [TestMethod]
        public void Test_Height_List()
        {
            string test_name = "Test_Height_List";
            int expected = 13;
            int actual = 1;

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Height_Dictionary()
        {
            string test_name = "Test_Height_Dictionary";
            int expected = 3;
            var functions = new MainWindow();
            int actual = functions.get_height(test_json_dir + "height_3.json");

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_List_Of_Lists()
        {
            string test_name = "Test_Depth_List_Of_Lists";
            int expected = 13;
            int actual = 1;

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_List_Of_Dicts()
        {
            string test_name = "Test_Depth_List_Of_Dicts";
            int expected = 10;
            int actual = 1;

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_List_Of_Mix()
        {
            string test_name = "Test_Depth_List_Of_Mix";
            int expected = 10;
            int actual = 1;

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_Dict_Of_Lists()
        {
            string test_name = "Test_Depth_Dict_Of_Lists";
            int expected = 10;
            int actual = 1;

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_Dict_Of_Dicts()
        {
            string test_name = "Test_Depth_Dict_Of_Dicts";
            int expected = 3;
            int actual = 10;

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

        [TestMethod]
        public void Test_Depth_Dict_Of_Mix()
        {
            string test_name = "Test_Depth_Dict_Of_Mix";
            int expected = 10;
            int actual = 1;

            string test_output = string.Format("{0} {1} {2}\n", test_name, expected, actual);
            System.IO.File.AppendAllText(output_path, test_output);
            Assert.IsTrue(actual == expected, test_name + " failed");
        }

    }
}

