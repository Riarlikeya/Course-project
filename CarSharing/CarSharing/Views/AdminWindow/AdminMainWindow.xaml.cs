using CarSharing.Models;
using CarSharing.Views.AdminWindow.AdminPages;
using CarSharing.Views.AdminWindow.AdminPages.Stats;
using CarSharing.Views.AdminWindow.AdminPages.UserControls;
using System.Windows;

namespace CarSharing.Views.AdminWindow
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
