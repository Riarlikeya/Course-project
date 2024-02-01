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

namespace AdminProgram.Views.AdminWindow.AdminPages.UserControls
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

        private void EditAdmin_Click(object sender,RoutedEventArgs e)
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
