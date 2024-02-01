using CarSharing.Models;
using CarSharing.ViewModels;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;



namespace CarSharing.Views.AdminWindow.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AddCarPage.xaml
    /// </summary>
    public partial class AddCarPage : Page
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        private readonly DataBaseControl _dbContext;
        public AddCarPage()
        {
            InitializeComponent();
            _dbContext = new DataBaseControl();

        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string brand = FormatInput(BrandTextBox.Text);
            string model = FormatInput(ModelTextBox.Text);
            string stateNumber = StateNumberTextBox.Text.ToUpper();

            if (IsValidStateNumber(stateNumber))
            {
                if (decimal.TryParse(PriceTextBox.Text, out decimal price) && price >= 0)
                {
                    if (double.TryParse(LatitudeTextBox.Text, out double latitude) && double.TryParse(LongitudeTextBox.Text, out double longitude))
                    {
                        int defaultStatusId = 1;

                        Car newCar = new Car
                        {
                            Brand = brand,
                            Model = model,
                            StateNumber = stateNumber,
                            Price = price,
                            Latitude = latitude,
                            Longitude = longitude,
                            Status = _dbContext.Statuses.FirstOrDefault(s => s.Id == defaultStatusId),
                        };
                        _dbContext.AddCar(newCar);

                        // Очистите TextBox после добавления
                        BrandTextBox.Clear();
                        ModelTextBox.Clear();
                        StateNumberTextBox.Clear();
                        PriceTextBox.Clear();
                        LatitudeTextBox.Clear();
                        LongitudeTextBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Введите корректные координаты!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректную стоимость!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Неверный формат госномера. Пожалуйста, введите верный госномер.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidStateNumber(string stateNumber)
        {
            // Регулярное выражение для проверки госномера, например, формата "А123БВ45"
            string pattern = @"^[А-Я]{1}\d{3}[А-Я]{2}\d{2}$";

            // Проверяем соответствие госномера шаблону
            return Regex.IsMatch(stateNumber, pattern);
        }

        public void AddCar(Car newCar)
        {
            if (newCar != null)
            {
                Cars.Add(newCar);
            }
            else
            {
                MessageBox.Show("Ошибка: передан null объект Car.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private string FormatInput(string input)
        {
            // Преобразуем первую букву в верхний регистр, а остальные - в нижний
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}
