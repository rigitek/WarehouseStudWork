using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        WarehouseContext db = new WarehouseContext();
        public ProductsWindow()
        {
            InitializeComponent();
            this.Loaded += ProductsWindow_Loaded;
        }

        private void ProductsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка данных из бд
            db.Products.Load();

            //привязка контекста данных
            DataContext = db.Products.Local.ToObservableCollection();
        }

        private void AddShopCart_Click(object sender, RoutedEventArgs e)
        {
            //передача выбранного объекта
            Product? product = productsGrid.SelectedItem as Product;
            if (product is null) return;

            //создание нового объекта и передача параметров
            ShopCart shopCart = new ShopCart();
            shopCart.Product = product;
            shopCart.ProductPrice = product.Price;
            shopCart.ProductAmount = 1;

            //добавление данных в бд
            db.ShopCarts.Add(shopCart);
            //сохранение изменений
            db.SaveChanges();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            this.Close();
            clientWindow.Show();
        }
    }
}
