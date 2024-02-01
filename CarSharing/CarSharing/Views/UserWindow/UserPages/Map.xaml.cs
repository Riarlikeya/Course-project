using CarSharing.Models;
using CarSharing.ViewModels;
using CarSharing.Views.UserWindow.UserPages.MapItems;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarSharing.Views.UserWindow.UserPages
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Page
    {
        public List<Car> Cars { get; set; } = new List<Car>();
        private AppDbContext _dbContext;

        public Map()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            DataContext = this;
            InitializeMap();
        }

        private void InitializeMap()
        {
            // Инициализация карты
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl.MapProvider = GoogleMapProvider.Instance;
            gMapControl.MinZoom = 10;
            gMapControl.MaxZoom = 20;
            gMapControl.Zoom = 10;
            gMapControl.Position = new PointLatLng(56.8389, 60.6057); // Координаты Екатеринбурга
            gMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            gMapControl.CanDragMap = true;
            gMapControl.DragButton = MouseButton.Left;
            gMapControl.ShowCenter = false;

            AddMarkersFromDatabase();
        }
        private void AddMarkersFromDatabase()
        {
            foreach (var car in _dbContext.Cars)
            {
                if (car.StatusId == 1)
                {
                    // Создание маркера для каждой машины и установка его позиции
                    var marker = new GMapMarker(new PointLatLng(car.Latitude, car.Longitude))
                    {
                        Shape = new CarControl(car), // Используйте ваш UserControl вместо CarMarkerControl
                        Tag = car // Привязка объекта Car к маркеру
                    };

                    // Добавление маркера на карту
                    gMapControl.Markers.Add(marker);
                }
            }
        }


        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (gMapControl.Zoom < 21)
            {
                gMapControl.Zoom += 1;
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (gMapControl.Zoom > 0)
            {
                gMapControl.Zoom -= 1;
            }
        }
    }
}
