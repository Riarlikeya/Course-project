using CarSharing.Models;
using CarSharing.ViewModels;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace CarSharing.Views.AdminWindow.AdminPages.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EditUsers.xaml
    /// </summary>
    public partial class EditUsers : Window
    {
        public ObservableCollection<Role> Roles { get; set; }
        public ObservableCollection<Subscription> Subscriptions { get; set; }
        private User _editedUser;
        public EditUsers(User editedUser)
        {
            InitializeComponent();
            DataContext = this;
            _editedUser = editedUser;
            LoadUserData(_editedUser);
            LoadRoles();
            LoadSubs();
        }
        private void LoadUserData(User user)
        {
            userEditing.Text = "Редактирование пользователя " + user.Login;
            firstName.Text = user.FirstName;
            lastName.Text = user.LastName;
            password.Text = user.Password;
        }
        private void LoadRoles()
        {
            using (var _dbContext = new AppDbContext())
            {
                Roles = new ObservableCollection<Role>(_dbContext.Roles.ToList());
            }
        }
        private void LoadSubs()
        {
            using (var _dbContext = new AppDbContext())
            {
                Subscriptions = new ObservableCollection<Subscription>(_dbContext.Subscriptions.ToList());
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveUserData();
            FrameContext.MainWindowFrame.Navigate(new ViewUsers());
            this.Close();
        }
        private void SaveUserData()
        {
            using (var _dbContext = new AppDbContext())
            {
                // Получите редактируемого пользователя из базы данных
                var editedUser = _dbContext.Users.FirstOrDefault(u => u.Id == _editedUser.Id);

                if (editedUser != null)
                {
                    if (!string.IsNullOrEmpty(password.Text))
                    {
                        if (IsStrongPassword(password.Text))
                        {
                            // Обновите данные пользователя
                            editedUser.FirstName = firstName.Text;
                            editedUser.LastName = lastName.Text;
                            editedUser.Password = password.Text;
                            editedUser.Role = (Role)RoleComboBox.SelectedItem;
                            editedUser.Sub = (Subscription)SubComboBox.SelectedItem;

                            // Сохраните изменения в базе данных
                            _dbContext.SaveChanges();

                            MessageBox.Show("Данные сохранены успешно.");
                        }
                        else
                        {
                            MessageBox.Show("Пароль должен содержать хотя-бы один спецсимвол, символ верхнего или нижнего регистра, длинной от 8 до 32 символов.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Поле пароль не должно быть пустым.");
                    }    
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении данных пользователя.");
                }
            }
        }
        private bool IsStrongPassword(string password)
        {
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
