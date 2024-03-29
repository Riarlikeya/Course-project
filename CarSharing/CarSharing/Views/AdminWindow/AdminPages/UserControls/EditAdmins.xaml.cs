﻿using CarSharing.Models;
using CarSharing.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace CarSharing.Views.AdminWindow.AdminPages.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EditAdmins.xaml
    /// </summary>
    public partial class EditAdmins : Window
    {
        public ObservableCollection<Role> Roles { get; set; }
        private User _editedUser;
        public EditAdmins(User editedUser)
        {
            InitializeComponent();
            DataContext = this;
            _editedUser = editedUser;
            LoadUserData(_editedUser);
            LoadRoles();
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
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveUserData();
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
                    // Обновите данные пользователя
                    editedUser.FirstName = firstName.Text;
                    editedUser.LastName = lastName.Text;
                    editedUser.Password = password.Text;
                    editedUser.Role = (Role)RoleComboBox.SelectedItem;

                    // Сохраните изменения в базе данных
                    _dbContext.SaveChanges();

                    MessageBox.Show("Данные сохранены успешно.");
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении данных пользователя.");
                }
            }
        }

    }
}
