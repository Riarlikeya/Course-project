using CarSharing.Models;
using CarSharing.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CarSharing.Views.UserWindow.UserPages
{
    /// <summary>
    /// Логика взаимодействия для PersonalCabinet.xaml
    /// </summary>
    public partial class PersonalCabinet : Page
    {
        private readonly AppDbContext _dbContext;
        public PersonalCabinet()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            LoadUserData();
        }
        private void LoadUserData()
        {
            string currentUserName = GetCurrentUserName();

            UserName.Text = $"Добро пожаловать, {currentUserName}!";


        }
        private string GetCurrentUserName()
        {
            int currentUserId = UserManager.CurrentUserId;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);
            DataContext = currentUser;

            if (currentUser != null)
            {
                firstName.Text = currentUser.FirstName;
                lastName.Text = currentUser.LastName;
                email.Text = currentUser.Email;
                driverPass.Text = currentUser.DriverPass;
            }

            if (string.IsNullOrEmpty(currentUser.FirstName))
            {
                return $"{currentUser.Login}";
            }
            else if (string.IsNullOrEmpty(currentUser.LastName))
            {
                return $"{currentUser.FirstName}";
            }
            else if (!string.IsNullOrEmpty(currentUser.FirstName) && !string.IsNullOrEmpty(currentUser.LastName))
            {
                return $"{currentUser?.FirstName} {currentUser?.LastName}";
            }
            else return string.Empty;
        }
        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            // Открываем или закрываем меню в зависимости от текущего состояния
            if (secondGrid.Height == 0)
            {
                // Открываем меню
                DoubleAnimation openGridAnimation = new DoubleAnimation(400, TimeSpan.FromSeconds(0.3));
                secondGrid.BeginAnimation(FrameworkElement.HeightProperty, openGridAnimation);
            }
            else
            {
                // Закрываем меню
                DoubleAnimation closeGridAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
                secondGrid.BeginAnimation(FrameworkElement.HeightProperty, closeGridAnimation);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string firstNameValue = firstName.Text;
            string lastNameValue = lastName.Text;
            string emailValue = email.Text;
            string driverPassValue = driverPass.Text;

            int currentUserId = UserManager.CurrentUserId;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);

            if (currentUser != null)
            {
                currentUser.FirstName = firstNameValue;
                currentUser.LastName = lastNameValue;
                currentUser.Email = emailValue;
                if (!string.IsNullOrEmpty(driverPassValue))
                {
                    if (!IsValidDriverLicenseFormat(driverPassValue))
                    {
                        MessageBox.Show("Проверьте правильность ввода водительского удостоверения. Формат должен быть: 0000 000000.");
                        return;
                    }
                    else
                    {
                        currentUser.DriverPass = driverPassValue;
                    }
                }
                else
                {
                    MessageBox.Show("Введите номер водительского удоствоерения");
                }
                _dbContext.SaveChanges();
            }
            FrameContext.MainWindowFrame.Navigate(new PersonalCabinet());
        }
        private bool IsValidDriverLicenseFormat(string driverLicense)
        {
            // Проверяем формат водительского удостоверения с использованием регулярного выражения
            Regex regex = new Regex(@"^\d{4}\s\d{6}$");
            return regex.IsMatch(driverLicense);
        }
    }
}
