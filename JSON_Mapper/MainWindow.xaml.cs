﻿using Newtonsoft.Json;
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
            string text = System.IO.File.ReadAllText("TestJson.txt");
            JsonTextReader reader = new JsonTextReader(new StringReader(text));
            JsonColumn test = new JsonColumn { Property = "", Value = "" };

            while (reader.TokenType.ToString() != "String")
            {
                reader.Read();
                
                if (reader.TokenType.ToString() == "PropertyName")
                {
                    test.Property = reader.Value.ToString();
                } else if (reader.TokenType.ToString() == "String")
                {
                    test.Value = reader.Value.ToString();
                }


            }

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Property",
                DisplayMemberBinding = new Binding("Property")
            });

            this.listView.Items.Add(test);

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
