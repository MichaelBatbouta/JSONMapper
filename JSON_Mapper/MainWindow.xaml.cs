using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace JSON_Mapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int depthTracker = 2;
        JsonTreeWrapper masterTree = new JsonTreeWrapper();
        Dictionary<String, String> headerDictionary = new Dictionary<string, string>();
        String masterPath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LoadJson();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
         
        }

        //easy
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
                    if( child.Count > count )
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            this.listView.View = gridView;
            int i = 0;
            string text = System.IO.File.ReadAllText("test.json");
            JsonTextReader reader = new JsonTextReader(new StringReader(text));
            JsonColumn test = new JsonColumn { Property = "", Value = "" };

            
            String jProp = "", jVal = "";
            List<String> jValList = new List<String>();
            bool arrayFlag = false;
            bool ignoreflag = false;

            //Parse JSON
            while (reader.Read())
            {
                if(i != 0 && reader.TokenType.ToString() == "StartObject")
                {
                    ignoreflag = true;
                }
                if (reader.TokenType.ToString() == "EndObject")
                {
                    ignoreflag = false;
                }
                if (ignoreflag == false)
                {
                    //If a property type save value 
                    if (reader.TokenType.ToString() == "PropertyName")
                    {
                        //test.Property = reader.Value.ToString();
                        jProp = reader.Value.ToString();
                    }
                    //if a string value, set it and add to master tree
                    else if (reader.TokenType.ToString() == "String" || reader.TokenType.ToString() == "Integer")
                    {
                        if (!arrayFlag)
                        {
                            //test.Value = reader.Value.ToString();
                            jVal = reader.Value.ToString();
                            masterTree.addObject(new JsonTreeObject(jProp, jVal));
                            jProp = "";
                            jVal = "";
                        }
                        else
                        {
                            jValList.Add(reader.Value.ToString());
                        }
                    }
                    else if (reader.TokenType.ToString() == "StartArray")
                    {
                        arrayFlag = true;
                    }
                    else if (reader.TokenType.ToString() == "EndArray")
                    {
                        masterTree.addObject(new JsonTreeObject(jProp, jValList));
                        arrayFlag = false;
                        jValList.Clear();
                    }
                   

                }

                i++;


            }

            masterTree.printTree();

            
            

            
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Property",
                DisplayMemberBinding = new Binding("Property")
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Value",
                DisplayMemberBinding = new Binding("Value")
            });

   
            foreach (JsonTreeObject j in masterTree.data)
            {
                if(j.isArray)
                {
                    this.listView.Items.Add((new JsonColumn { Property = j.property, Value = "Array" }));
                }
                else
                {
                    this.listView.Items.Add((new JsonColumn { Property = j.property, Value = j.values[0] }));
                }
                

            }
            this.listView.Items.Add((new JsonColumn { Property = "other", Value = "Object" }));

        }



        public class JsonTreeWrapper
        {
            //root list of tree
            public List<JsonTreeObject> data = new List<JsonTreeObject>();


            public void addObject(JsonTreeObject newObject)
            {
                data.Add(newObject);
            }


            public void printTree()
            {
                foreach(JsonTreeObject obj in data)
                {
                    obj.printTreeObject();
                }
            }

            public JsonTreeObject findJsonTreeObject(String key)
            {
                foreach(JsonTreeObject obj in data)
                {
                    if(obj.property == key)
                    {
                        return obj;
                    }

                }

                return null;
            }
            


        }

        public class JsonTreeObject
        {
            public String property;
            public List<String> values = new List<string>();
            public bool isArray;
            public bool hasChild;

            public JsonTreeObject(String p, String v)
            {
                property = p;
                values.Add(v);
                isArray = false;
            }

            public JsonTreeObject(String p, List<String> v)
            {
                property = p;
                values = new List<String>(v);
                isArray = true;
            }

            public void printTreeObject()
            {
                if(!isArray)
                {
                    Console.WriteLine("Token {0}, Value {1}", property, values[0]);
                }
                else
                {
                    Console.WriteLine("Token {0}", property);
                    foreach (String obj in values)
                    {
                        Console.WriteLine("Array Value: {0}", obj);
                    }
                }
                
            }

        }

        public class MyItem
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class JsonColumn
        {
            public string Property { get; set; }

            public string Value { get; set; }
        }

        public class JsonColumn2
        {
            public string Property2 { get; set; }

            public string Value2 { get; set; }
        }

        public class JsonColumn3
        {
            public string Property3 { get; set; }

            public string Value3 { get; set; }
        }

        private void AddColumn(ListView lv, string title, int width)
        {
            GridViewColumnHeader column = new GridViewColumnHeader();
          //  column.TextInput = title;
            column.Width = width;
            //lv.Columns.Add(column);
        }


        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JsonColumn test = (JsonColumn)this.listView.SelectedItem;

            Console.WriteLine(test.Property);
            GridView gridView = new GridView();
  
            this.listView1.View = gridView;
            //gridView = (GridView) this.listView1.View;


            masterPath = masterPath + test.Property;
           

            if(test.Property == "other")
            {
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Property",
                    DisplayMemberBinding = new Binding("Property")
                });

                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Value",
                    DisplayMemberBinding = new Binding("Value")
                });
                this.listView1.Items.Add((new JsonColumn { Property = "weather" , Value = "bad" }));
                this.listView1.Items.Add((new JsonColumn { Property = "otherlist", Value = "Array"}));
            }

            else
            {
                JsonTreeObject j = masterTree.findJsonTreeObject(test.Property);
                if (j.isArray)
                {
                    foreach (String v in j.values)
                    {
                        this.listView1.Items.Add((new JsonColumn { Property = j.property, Value = v }));
                    }

                }
                else
                {
                    this.listView1.Items.Add((new JsonColumn { Property = j.property, Value = j.values[0] }));
                }
            }



            
            depthTracker++;



        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JsonColumn test = (JsonColumn)this.listView1.SelectedItem;
            GridView gridView2 = new GridView();

            this.listView2.View = gridView2;

            masterPath = masterPath + test.Property;

            if (test.Property == "otherlist")
            {
                gridView2.Columns.Add(new GridViewColumn
                {
                    Header = "Property",
                    DisplayMemberBinding = new Binding("Property")
                });

                gridView2.Columns.Add(new GridViewColumn
                {
                    Header = "Value",
                    DisplayMemberBinding = new Binding("Value")
                });
                this.listView2.Items.Add((new JsonColumn { Property = "[0]", Value = "red" }));
                this.listView2.Items.Add((new JsonColumn { Property = "[1]", Value = "yellow" }));
            }
        }

        private void listView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JsonColumn test = (JsonColumn)this.listView2.SelectedItem;

            masterPath = masterPath + test.Value;
        }
    }
}
