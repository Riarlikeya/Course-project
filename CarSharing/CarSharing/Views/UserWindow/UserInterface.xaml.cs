using CarSharing.Models;
using CarSharing.Views.UserWindow.UserPages;
using System.Windows;
using System.Windows.Media.Animation;

namespace CarSharing.Views.UserWindow
{
    /// <summary>
    /// Логика взаимодействия для UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        private readonly List<Car> cars;
        public UserInterface()
        {
            InitializeComponent();
            FrameContext.MainWindowFrame = mainFrame;
            FrameContext.MainWindowFrame.Navigate(new Map());
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем или закрываем меню в зависимости от текущего состояния
            if (MenuPanel.Width == 0)
            {
                // Открываем меню
                DoubleAnimation openMenuAnimation = new DoubleAnimation(300, TimeSpan.FromSeconds(0.3));
                MenuPanel.BeginAnimation(FrameworkElement.WidthProperty, openMenuAnimation);
            }
            else
            {
                // Закрываем меню
                DoubleAnimation closeMenuAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
                MenuPanel.BeginAnimation(FrameworkElement.WidthProperty, closeMenuAnimation);
            }
        }

        private void Sub_Click(object sender, RoutedEventArgs e)
        {

            FrameContext.MainWindowFrame.Navigate(new Subs());
            DoubleAnimation closeMenuAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(FrameworkElement.WidthProperty, closeMenuAnimation);
        }
        private void OpenMap_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new Map());
            DoubleAnimation closeMenuAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(FrameworkElement.WidthProperty, closeMenuAnimation);
        }
        private void PersonalCabinet_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new PersonalCabinet());
            DoubleAnimation closeMenuAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(FrameworkElement.WidthProperty, closeMenuAnimation);
        }
    }
}
