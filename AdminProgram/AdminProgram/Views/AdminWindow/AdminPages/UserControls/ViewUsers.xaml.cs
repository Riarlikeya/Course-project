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
using AdminProgram.ViewModels;
using AdminProgram.Models;
using AdminProgram.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace AdminProgram.Views.AdminWindow.AdminPages.UserControls
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
