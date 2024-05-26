using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void AddShopCart_Click(object sender, RoutedEventArgs e)
        {
            //ShopCart? shopCart = shopCartGrid.SelectedItem as ShopCart;
            //if (shopCart is null) return;

            //Order shopCart = new ShopCart();
            //shopCart.Product = product;

            //db.ShopCarts.Add(shopCart);
            //db.SaveChanges();

        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            this.Close();
            clientWindow.Show();
        }
    }
}
