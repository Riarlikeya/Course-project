using CarSharing.Models;
using CarSharing.ViewModels;
using CarSharing.Views.AdminWindow;
using CarSharing.Views.UserWindow;
using Npgsql;
using System.Windows;
using System.Windows.Controls;

namespace CarSharing.Views
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                string Login = LoginTextBox.Text;
                string Password = PasswordBox.Password;

                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var dbContext = new AppDbContext())
                {
                    var user = dbContext.Users.Where(u => u.Login == Login && u.Password == Password).FirstOrDefault();

                    if (user != null)
                    {
                        int userId = GetUserIdByUsername(Login);
                        UserManager.SetCurrentUser(userId);
                        // Пользователь найден
                        if (user.RoleId == 1)
                        {
                            //MessageBox.Show("Авторизация успешна!");
                            if (MessageBox.Show("Запустить окно администрирования?", "Выбор окна", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                // открытие окна админа
                                AdminMainWindow adminWindow = new AdminMainWindow();
                                adminWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                UserInterface ui = new UserInterface();
                                ui.Show();
                                this.Close();
                            }
                        }
                        else if (user.RoleId == 2)
                        {
                            UserInterface ui = new UserInterface();
                            ui.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                // Обработка ошибок подключения к базе данных
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public int GetUserIdByUsername(string Login)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == Login);
                return user != null ? user.Id : -1;
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            registrationWindow.Show();
            this.Close();
        }
    }
}
