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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AdminProgram.Models;
using AdminProgram.Views.UserWindow.UserPages;

namespace AdminProgram.Views.UserWindow
{
    /// <summary>
    /// Логика взаимодействия для UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
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
    }
}
