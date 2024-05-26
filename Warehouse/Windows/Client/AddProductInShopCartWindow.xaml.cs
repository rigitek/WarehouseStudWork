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
    /// Логика взаимодействия для AddProductInShopCartWindow.xaml
    /// </summary>
    public partial class AddProductInShopCartWindow : Window
    {
        public Product Product { get; set; }
        public AddProductInShopCartWindow(Product product)
        {
            InitializeComponent();

            Product = product;
            DataContext = Product;
        }

        private void PriceFull_Changed(object sender, RoutedEventArgs e)
        {
            double Sum = Product.Price * double.Parse(ProductAmount.Text);
            ProductPrice.Text =Sum.ToString();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }
    }
}
