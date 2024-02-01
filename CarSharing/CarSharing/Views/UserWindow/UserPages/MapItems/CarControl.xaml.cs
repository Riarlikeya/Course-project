using CarSharing.Models;
using CarSharing.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CarSharing.Views.UserWindow.UserPages.MapItems
{
    /// <summary>
    /// Логика взаимодействия для CarControl.xaml
    /// </summary>
    public partial class CarControl : UserControl
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        private AppDbContext _dbContext;
        public CarControl(Car car)
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            DataContext = this;
            LoadCarData(car);
        }
        private void MarkerButton_Click(object sender, RoutedEventArgs e)
        {
            var cars = _dbContext.Cars;
            markerButton.Visibility = Visibility.Hidden;

            DoubleAnimation openCarDataHeightAnimation = new DoubleAnimation(150, TimeSpan.FromSeconds(0.3));
            carData.BeginAnimation(FrameworkElement.HeightProperty, openCarDataHeightAnimation);
            DoubleAnimation openCarDataWidthAnimation = new DoubleAnimation(250, TimeSpan.FromSeconds(0.3));
            carData.BeginAnimation(FrameworkElement.WidthProperty, openCarDataWidthAnimation);
        }
        private void LoadCarData(Car car)
        {
            carText.Text = car.Brand + " " + car.Model + " " + car.StateNumber;
            carPrice.Text = $"{car.Price} ₽";
        }
        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation closeCarDataHeightAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            carData.BeginAnimation(FrameworkElement.HeightProperty, closeCarDataHeightAnimation);
            DoubleAnimation closeCarDataWidthAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            carData.BeginAnimation(FrameworkElement.WidthProperty, closeCarDataWidthAnimation);

            markerButton.Visibility = Visibility.Visible;
        }
    }
}
