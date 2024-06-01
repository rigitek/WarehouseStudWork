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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public Product Product { get; set; }

        //передача объекта в конструктор
        public AddProductWindow(Product product)
        {
            InitializeComponent();

            Product = product;
            //передача объекта в контекст
            DataContext = Product;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            //результат нажатия на кнопку добавить
            DialogResult = true;
        }
    }
}
