using Microsoft.EntityFrameworkCore;
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
using Warehouse.Models;

namespace Warehouse.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        //для работы с бд
        WarehouseContext db = new WarehouseContext();
        public OrdersWindow()
        {
            InitializeComponent();
            this.Loaded += OrdersWindow_Loaded;
        }

        //срабатывает при загрузке окна
        private void OrdersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка данных из бд
            db.Orders.Load();
            db.Products.Load();

            //выбор сегодняшней даты по умолчанию
            DatePick.SelectedDate = DateTime.Today;

            //передача данных в контекст
            DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.Date == DatePick.SelectedDate);
        }

        //возвращение обратно в меню
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }

        //при выборе дате срабатывает
        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //выводит только заказы с выбранной датой
            DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.Date == DatePick.SelectedDate);
            //сумма всех заказов в выбранную дату
            Sum.Header = db.Orders.Where(x => x.Date == DatePick.SelectedDate).Sum(x => x.Price).ToString();
        }
    }
}
