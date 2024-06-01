using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

namespace Warehouse.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        //для работы с бд
        WarehouseContext db = new WarehouseContext();
        public ProductsWindow()
        {
            InitializeComponent();
            this.Loaded += ProductsWindow_Loaded;
        }

        //срабаытает при загрузке окна
        private void ProductsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка данных из бд
            db.Products.Load();
            //передача данных в контекст
            DataContext = db.Products.Local.ToObservableCollection();
        }

        //метод привязанный к кнопке добавления
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow AddProductWindow = new AddProductWindow(new Product());

            //если окно закрыто с тру
            if (AddProductWindow.ShowDialog() == true)
            {
                //передача объекта из окна 
                Product Product = AddProductWindow.Product;
                //добавление нового объекта в бд
                db.Products.Add(Product);
                //сохранение изменений
                db.SaveChanges();
            }
        }

        //метод привязанный к кнопке изменения
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //передача выбранного элемента
            Product? product = productsGrid.SelectedItem as Product;
            // если элемент не выбран, то ничего не происходит
            if (product is null) return;

            //создание объекта окна и отправка выбранных данных в конструкторе
            AddProductWindow AddProductWindow = new AddProductWindow(new Product
            {
                Id = product.Id,
                Title = product.Title,
                Amount = product.Amount,
                Price = product.Price
            });


            //если при закрытии нажато добавить, то тру и
            if (AddProductWindow.ShowDialog() == true)
            {
                // получаем измененный объект и ищем его в бд
                product = db.Products.Find(AddProductWindow.Product.Id);
                //если объект найдет, то сохраняем изменения
                if (product != null)
                {
                    product.Title = AddProductWindow.Product.Title;
                    product.Amount = AddProductWindow.Product.Amount;
                    product.Price = AddProductWindow.Product.Price;
                    //сохранение данных в бд
                    db.SaveChanges();
                    //обновление списка
                    productsGrid.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Product? product = productsGrid.SelectedItem as Product;
            // если ни одного объекта не выделено, выходим
            if (product is null) return;
            //удаляем объект
            db.Products.Remove(product);
            //сохранение данных в бд
            db.SaveChanges();
        }

        //выход обратно в меню
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }

    }
}
