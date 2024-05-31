using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Логика взаимодействия для ShopCartWindow.xaml
    /// </summary>
    public partial class ShopCartWindow : Window
    {
        WarehouseContext db = new WarehouseContext();

        List<ShopCart> shopCart;
        public ShopCartWindow()
        {
            InitializeComponent();
            //загрузка при октрытии окна
            this.Loaded += ShopCartWindow_Loaded;
        }

        private void ShopCartWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка данных из бд
            db.Products.Load();
            db.ShopCarts.Load();

            //установка данных из бд в контекст
            DataContext = db.ShopCarts.Local.ToObservableCollection();

            //передача данных в лист
            shopCart=db.ShopCarts.ToList();

            //инициализация даты
            DatePick.SelectedDate=DateTime.Today;
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            //цикл для создания заказа, все что добавлено в корзину, записывается в базу заказы
            foreach (ShopCart shopCarts in shopCart)
            {
                //создание нового обьекта заказа
                Order order = new Order
                {
                    Date = DatePick.SelectedDate.Value,
                    Price = shopCarts.ProductPrice,
                    Amount = shopCarts.ProductAmount,
                    Product = shopCarts.Product
                };

                //добавление объекта в бд
                db.Orders.Add(order);
                //удаление добавленного объекта из корзины
                db.ShopCarts.Remove(shopCarts);
                //сохранение изменений бд
                db.SaveChanges();
            }
        }

        //метод добавления количества товаров
        private void PlusAmount_Click(object sender, RoutedEventArgs e)
        {
            //передача выбранного объекта
            ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;
            //если объект не выбран, то ничего не происходит
            if (shopCart is null) return;

            //выбранный объект находим в базе данных
            shopCart = db.ShopCarts.Find(shopCart.Id);
            //если объект существует и количество выбранного меньше, чем есть в наличии
            if (shopCart != null && shopCart.ProductAmount < shopCart.Product.Amount)
            {
                //то добавляется одна штука
                shopCart.ProductAmount += 1;
                //расчет стоимости в зависимости от количества
                shopCart.ProductPrice=shopCart.Product.Price*shopCart.ProductAmount;

                //сохранение изменений в бд
                db.SaveChanges();
                //обновление данных в окне
                shopCartGrid.Items.Refresh();
            }
        }

        //метод убавления количества товаров
        private void MinusAmount_Click(object sender, RoutedEventArgs e)
        {
            //передача выбранного объекта
            ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;
            //если объект не выбран, то ничего не происходит
            if (shopCart is null) return;

            //выбранный объект находим в базе данных
            shopCart = db.ShopCarts.Find(shopCart.Id);
            //если объект существует и количество не равно 1
            if (shopCart != null && shopCart.ProductAmount!=1 )
            {
                //то убирается одна штука
                shopCart.ProductAmount -= 1;
                //расчет стоимости в зависимости от количества
                shopCart.ProductPrice = shopCart.Product.Price * shopCart.ProductAmount;

                //сохранение изменений в бд
                db.SaveChanges();
                //обновление данных в окне
                shopCartGrid.Items.Refresh();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;
            // если ни одного объекта не выделено, выходим
            if (shopCart is null) return;
            //удаляем выбранный объект из бд
            db.ShopCarts.Remove(shopCart);
            //сохраняем изменения
            db.SaveChanges();
        }

        //метод для перехода в меню клиента
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна
            ClientWindow clientWindow = new ClientWindow();
            //закрытие открытого окна
            this.Close();
            //открытие нового окна
            clientWindow.Show();
        }
    }
}
