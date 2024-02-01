using CarSharing.Models;
using CarSharing.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace CarSharing.Views.AdminWindow.AdminPages.Stats
{
    /// <summary>
    /// Логика взаимодействия для StatsCars.xaml
    /// </summary>

    public partial class StatsCars : Page
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        private AppDbContext _dbContext;
        public StatsCars()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            LoadCars();
            DataContext = this;
            carsTitle.Text = "Текущее количество автомобилей - " + _dbContext.Cars.Count();
        }

        private void LoadCars()
        {
            var cars = _dbContext.Cars.Include(c => c.Status).Distinct().ToList();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
    }
}
