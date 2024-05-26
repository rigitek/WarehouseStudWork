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

            db.Products.Load();
            DataContext = db.Products.Local.ToObservableCollection();

        }

        private void AddShopCart_Click(object sender, RoutedEventArgs e)
        {
            Product? product = productsGrid.SelectedItem as Product;
            if (product is null) return;

            AddProductInShopCartWindow AddProductInShopCartWindow = new AddProductInShopCartWindow(new Product{

                Id = product.Id,
                Title = product.Title,
                Amount = product.Amount,
                Price = product.Price
            });

            if (AddProductInShopCartWindow.ShowDialog() == true)
            {

                ShopCart shopCart = new ShopCart();
                shopCart.Product = product;
               

                db.ShopCarts.Add(shopCart);
                db.SaveChanges();


            }

            
        }
    

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            this.Close();
            clientWindow.Show();
        }
    }
}
