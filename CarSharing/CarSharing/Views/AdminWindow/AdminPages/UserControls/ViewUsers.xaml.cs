using CarSharing.Models;
using CarSharing.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CarSharing.Views.AdminWindow.AdminPages.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : Page
    {
        private AppDbContext _dbContext;
        private DataBaseControl _dbControl;
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public ViewUsers()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            _dbControl = new DataBaseControl();
            LoadUsers();
        }
        private void LoadUsers()
        {
            var users = _dbContext.Users.Include(u => u.Role).Include(u => u.Sub).Where(u => u.RoleId == 2).ToList();
            dataGrid.Items.Clear();
            dataGrid.ItemsSource = users;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            User u = dataGrid.SelectedItem as User;
            if (u != null)
            {
                EditUsers edit = new EditUsers(u);
                edit.Show();
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования");
            }
        }
        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            User u = dataGrid.SelectedItem as User;
            if (u != null)
            {
                _dbControl.RemoveUser(u);
                Users.Remove(u);
                FrameContext.MainWindowFrame.Navigate(new ViewUsers());
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления");
            }
        }
    }
}
