using CarSharing.Models;
using CarSharing.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace CarSharing.Views.AdminWindow.AdminPages.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ViewAdmins.xaml
    /// </summary>
    public partial class ViewAdmins : Page
    {
        private AppDbContext _dbContext;
        public ViewAdmins()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            LoadAdmins();
        }
        private void LoadAdmins()
        {
            var admins = _dbContext.Users.Include(u => u.Role).Where(u => u.RoleId == 1).ToList();
            dataGrid.Items.Clear();
            dataGrid.ItemsSource = admins;
        }

        private void EditAdmin_Click(object sender, RoutedEventArgs e)
        {
            User u = dataGrid.SelectedItem as User;
            if (u != null)
            {
                EditAdmins edit = new EditAdmins(u);
                edit.Show();
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования");
            }
        }
    }
}
