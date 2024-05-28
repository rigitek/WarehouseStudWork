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
        public ShopCartWindow()
        {
            InitializeComponent();
            this.Loaded += ShopCartWindow_Loaded;
        }

        private void ShopCartWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Products.Load();
            db.ShopCarts.Load();
            DataContext = db.ShopCarts.Local.ToObservableCollection();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;

            
                Order order = new Order{
                    Products = shopCart.Product,
                };
            
            

            //Order shopCart = new ShopCart();
            //shopCart.Product = product;

            //db.Order.AddRange( );
            //db.SaveChanges();

        }

        private void PlusAmount_Click(object sender, RoutedEventArgs e)
        {
            ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;
            if (shopCart is null) return;

            shopCart = db.ShopCarts.Find(shopCart.Id);
            if (shopCart != null && shopCart.ProductAmount < shopCart.Product.Amount)
            {
                shopCart.ProductAmount += 1;
                shopCart.ProductPrice=shopCart.Product.Price*shopCart.ProductAmount;

                db.SaveChanges();
                shopCartGrid.Items.Refresh();
            }
        }

        private void MinusAmount_Click(object sender, RoutedEventArgs e)
        {
            ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;
            if (shopCart is null) return;

            shopCart = db.ShopCarts.Find(shopCart.Id);
            if (shopCart != null && shopCart.ProductAmount!=1 )
            {
                shopCart.ProductAmount -= 1;
                shopCart.ProductPrice = shopCart.Product.Price * shopCart.ProductAmount;

                db.SaveChanges();
                shopCartGrid.Items.Refresh();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;
            // если ни одного объекта не выделено, выходим
            if (shopCart is null) return;
            db.ShopCarts.Remove(shopCart);
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
