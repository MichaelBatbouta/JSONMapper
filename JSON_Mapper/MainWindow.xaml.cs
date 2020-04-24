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

            JsonTreeWrapper masterTree = new JsonTreeWrapper();
            String jProp = "", jVal = "";
            List<String> jValList = new List<String>();
            bool arrayFlag = false;

            //Parse JSON
            while (reader.Read())
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

            masterTree.printTree();

            
            

            
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Property",
                DisplayMemberBinding = new Binding("Property")
            });

            this.listView.Items.Add(new JsonColumn {Property = masterTree.data[0].property, Value = masterTree.data[0].values[0] } );
            

        }

            /*while (reader.TokenType.ToString() != "Int")
            {
                reader.Read();

                if (reader.TokenType.ToString() == "PropertyName")
                {
                    test2.Property = reader.Value.ToString();
                }
                else if (reader.TokenType.ToString() == "Int")
                {
                    test2.Value = reader.Value.ToString();
                }

                i++;
            }*/


        /* private void MenuItem_Click(object sender, RoutedEventArgs e)
         {
            // LoadJson();
             var gridView = new GridView();
             this.listView.View = gridView;
             gridView.Columns.Add(new GridViewColumn
             {
                 Header = "Id",
                 DisplayMemberBinding = new Binding("Id")
             });
             gridView.Columns.Add(new GridViewColumn
             {
                 Header = "Name",
                 DisplayMemberBinding = new Binding("Name")
             });

             // Populate list
             this.listView.Items.Add(new MyItem { Id = 1, Name = "David" });
         }*/


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
            


        }

        public class JsonTreeObject
        {
            public String property;
            public List<String> values = new List<string>();
            public bool isArray;

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

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JsonColumn test = (JsonColumn) this.listView.SelectedItem;

            GridView gridView = (GridView) this.listView.View;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Value",
                DisplayMemberBinding = new Binding("Value")
            });

           
        }
    }
}
