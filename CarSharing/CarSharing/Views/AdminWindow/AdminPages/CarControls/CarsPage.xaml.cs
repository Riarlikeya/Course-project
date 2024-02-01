using CarSharing.Models;
using CarSharing.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CarSharing.Views.AdminWindow.AdminPages
{
    public partial class CarsPage : Page
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        private AppDbContext _dbContext;

        public CarsPage()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
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

    }
}
