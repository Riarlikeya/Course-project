using CarSharing.Models;
using CarSharing.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CarSharing.Views.AdminWindow.AdminPages
{

    /// <summary>
    /// Логика взаимодействия для DeleteCarPage.xaml
    /// </summary>
    public partial class DeleteCarPage : Page
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        private AppDbContext _dbContext;
        private readonly DataBaseControl _dbControl;
        public DeleteCarPage()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            _dbControl = new DataBaseControl();
            LoadCars();
            DataContext = this;
        }
        private void LoadCars()
        {
            var cars = _dbContext.Cars.Include(c => c.Status).Distinct().ToList();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            Car selectedCar = (Car)dataGrid.SelectedItem;
            if (selectedCar != null)
            {
                // Вызываем метод удаления
                _dbControl.RemoveCar(selectedCar);
                Cars.Remove(selectedCar);
            }
        }
    }
}
