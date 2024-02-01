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
using System.Windows.Shapes;
using AdminProgram.ViewModels;
using AdminProgram.Models;
using AdminProgram.Views;
using AdminProgram.Views.AdminWindow.AdminPages;
using AdminProgram.Views.AdminWindow.AdminPages.UserControls;
using AdminProgram.Views.AdminWindow.AdminPages.Stats;

namespace AdminProgram.Views.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();

            FrameContext.MainWindowFrame = mainFrame;
            FrameContext.MainWindowFrame.Navigate(new StartPage());

            
        }

        private void ViewCars_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new CarsPage());
        }

        private void AddNewCar_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new AddCarPage());
        }

        private void RemoveCar_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new DeleteCarPage());
        }
        private void ViewAdmins_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new ViewAdmins());
        }
        private void ViewUsers_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new ViewUsers());
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new StartPage());
        }
        private void StatsUser_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new StatsUsers());
        }
        private void StatsCar_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new StatsCars());
        }
    }
}
