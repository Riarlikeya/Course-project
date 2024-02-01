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
using AdminProgram.Models;
using AdminProgram.ViewModels;
using AdminProgram.Views;

namespace AdminProgram.Views.AdminWindow.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private readonly AppDbContext _dbContext;
        public StartPage()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            LoadUserData();
        }

        private void LoadUserData()
        {
            string currentUserName = GetCurrentUserName();
            int numberOfCars = GetNumberOfCars();

            UserName.Text = $"Добро пожаловать, {currentUserName}!";
            TotalCarsAmount.Text = $"Количество машин: {numberOfCars}";
            CurrentDateTime.Text = $"Дата и время последней авторизации: {DateTime.Now}";

        }

        private string GetCurrentUserName()
        {
            int currentUserId = UserManager.CurrentUserId;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);
            DataContext = currentUser;

            return $"{currentUser?.FirstName} {currentUser?.LastName}";
        }

        private int GetNumberOfCars()
        {
            return _dbContext.Cars.Count();
        }
    }
}
