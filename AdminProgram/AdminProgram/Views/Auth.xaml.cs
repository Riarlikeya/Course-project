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
using Npgsql;
using AdminProgram.Models;
using AdminProgram.ViewModels;
using AdminProgram.Views;
using AdminProgram.Views.AdminWindow;
using AdminProgram.Views.UserWindow;

namespace AdminProgram.Views
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
