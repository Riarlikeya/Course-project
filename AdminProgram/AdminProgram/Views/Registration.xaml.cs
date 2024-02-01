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
using System.Windows.Shapes;
using Npgsql;
using AdminProgram.Models;
using AdminProgram.ViewModels;
using AdminProgram.Views;
using System.Text.RegularExpressions;

namespace AdminProgram.Views
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
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string phone = PhoneTextBox.Text;
            int sub = 1;
            int role = 2;

            if (!IsStrongPassword(password))
            {
                MessageBox.Show("Пароль должен содержать хотя-бы один спецсимвол, символ верхнего или нижнего регистра, длинной от 8 до 32 символов.");
                return;
            } else
            if (!IsValidatePhone(phone))
            {
                MessageBox.Show("Неправильный формат номера телефона.\nПример номера +1 (123) 456-78-90");
                return;
            } else
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Заполнены не все поля.");
                return;
            }

            if (_context.Users.Any(u => u.Login == username))
            {
                MessageBox.Show("Пользователь с таким логином существует.");
            }else
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

        private bool IsNull(string data)
        {
            if (data == null) {
                return false; 
            } else return true;
        }
        private bool IsStrongPassword(string password)
        {
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
