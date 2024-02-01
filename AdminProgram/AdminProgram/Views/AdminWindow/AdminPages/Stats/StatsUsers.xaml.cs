using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using AdminProgram.Models;
using AdminProgram.ViewModels;
using AdminProgram.Views;
using Microsoft.EntityFrameworkCore;

namespace AdminProgram.Views.AdminWindow.AdminPages.Stats
{
    /// <summary>
    /// Логика взаимодействия для StatsUsers.xaml
    /// </summary>
    public partial class StatsUsers : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private AppDbContext _dbContext;
        public StatsUsers()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            LoadAdminData();
            LoadClientData();
            pageTitle.Text = "Общее количество пользователей - " + _dbContext.Users.Count();
        }
        private void LoadAdminData()
        {
            var admins = _dbContext.Users.Include(u => u.Role).Where(u => u.RoleId == 1).ToList();
            adminsDataGrid.Items.Clear();
            adminsDataGrid.ItemsSource = admins;
        }

        private void LoadClientData()
        {
            var users = _dbContext.Users.Include(u => u.Role).Include(u => u.Sub).Where(u => u.RoleId == 2).ToList();
            clientsDataGrid.Items.Clear();
            clientsDataGrid.ItemsSource = users;
        }
    }
}
