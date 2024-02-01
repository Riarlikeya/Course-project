using CarSharing.Models;
using CarSharing.ViewModels;
using System.Windows.Controls;

namespace CarSharing.Views.AdminWindow.AdminPages
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
            TotalCarsAmount.Text = $"Количество автомобилей: {numberOfCars}";
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
