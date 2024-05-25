﻿using Microsoft.EntityFrameworkCore;
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow AddProductWindow = new AddProductWindow(new Product());

            //AddHumanWindow.Show();
            if (AddProductWindow.ShowDialog() == true)
            {
                Product Product = AddProductWindow.Product;
                db.Products.Add(Product);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Product? product = productsGrid.SelectedItem as Product;
            if (product is null) return;

            AddProductWindow AddProductWindow = new AddProductWindow(new Product
            {
                Id = product.Id,
                Title = product.Title,
                Amount = product.Amount,
                Price = product.Price
            });


            if (AddProductWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                product = db.Products.Find(AddProductWindow.Product.Id);
                if (product != null)
                {
                    product.Title = AddProductWindow.Product.Title;
                    product.Amount = AddProductWindow.Product.Amount;
                    product.Price = AddProductWindow.Product.Price;
                    db.SaveChanges();
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
            db.Products.Remove(product);
            db.SaveChanges();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }

    }
}
