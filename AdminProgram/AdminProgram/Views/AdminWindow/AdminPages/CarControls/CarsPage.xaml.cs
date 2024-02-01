using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AdminProgram.Models;
using AdminProgram.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AdminProgram.Views.AdminWindow.AdminPages
{
    public partial class CarsPage : Page
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        //public ObservableCollection<Status> Statuses { get; set; } = new ObservableCollection<Status>();
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
