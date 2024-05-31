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

namespace Warehouse.Windows.Client
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        WarehouseContext db = new WarehouseContext();
        public OrdersWindow()
        {
            InitializeComponent();
            this.Loaded += OrdersWindow_Loaded;
        }

        private void OrdersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Orders.Load();
            db.Products.Load();

            DatePick.SelectedDate = DateTime.Today;

            DataContext = db.Orders.Local.ToObservableCollection().Where(x=>x.Date==DatePick.SelectedDate);

            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            this.Close();
            clientWindow.Show();
        }

        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.Date == DatePick.SelectedDate);
            Sum.Header = db.Orders.Where(x => x.Date == DatePick.SelectedDate).Sum(x => x.Price).ToString();
        }
    }
}

