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
                        count = child.Count;
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
}
