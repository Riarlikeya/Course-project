using CarSharing.Models;
using CarSharing.ViewModels;
using Npgsql;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CarSharing.Views
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private AppDbContext _context;

        public Registration()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text;
                string password = PasswordBox.Password;
                string phone = PhoneTextBox.Text;
                int sub = 1;
                int role = 2;

                if (!IsStrongPassword(password))
                {
                    MessageBox.Show("Пароль должен содержать хотя-бы один спецсимвол, символ верхнего или нижнего регистра, длинной от 8 до 32 символов.");
                    return;
                }
                else
                {
                    if (!IsValidatePhone(phone))
                    {
                        MessageBox.Show("Неправильный формат номера телефона.\nПример номера +1 (123) 456-78-90");
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
                        {
                            MessageBox.Show("Заполнены не все поля.");
                            return;
                        }
                        else
                        {

                            if (_context.Users.Any(u => u.Login == username))
                            {
                                MessageBox.Show("Пользователь с таким логином существует.");
                            }
                            else
                            {
                                if (_context.Users.Any(u => u.Phone == phone))
                                {
                                    MessageBox.Show("Данный номер телефона занят.");
                                }
                                else
                                {

                                    _context.Users.Add(new User { Login = username, Password = password, Phone = phone, RoleId = role, SubId = sub });
                                    _context.SaveChanges();
                                    MessageBox.Show("Пользователь успешно зарегистрирован!");
                                    Auth auth = new Auth();
                                    auth.Show();
                                    this.Close();
                                }
                            }
                        }
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

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        private bool IsValidatePhone(string phone)
        {
            string pattern = @"^\+?\d{1,3}[\s-]?\(?\d{2,3}\)?[\s-]?\d{2,3}[\s-]?\d{2,3}$";
            return Regex.IsMatch(phone, pattern);

        }
        private bool IsStrongPassword(string password)
        {
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
