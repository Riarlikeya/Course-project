using System;
using System.Collections.Generic;
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
using Microsoft.Toolkit.Wpf.UI.Controls;
using Microsoft.Web.WebView2.Wpf;

namespace AdminProgram.Views.UserWindow.UserPages
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Page
    {
        public Map()
        {
            InitializeComponent();

            string apiKey = "4e2cc4e5-0147-4f41-ba8b-4f02fd20d092";
            
            double latitude = 56.8519;
            double longitude = 60.6122;

            string url = $"https://yandex.ru/maps/?apikey={apiKey}&ll={longitude},{latitude}&z=12";

            webView.Source = new Uri(url);
        }
    }

}
