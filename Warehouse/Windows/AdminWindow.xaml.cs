﻿using System;
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
using Warehouse.Windows.Admin;

namespace Warehouse.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        //метод для перехода на окно продуктов
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна
            ProductsWindow productsWindow = new ProductsWindow();
            //закрытие окна, которое открыто сейчас
            this.Close();
            //открытие ранее созданного объекта окна
            productsWindow.Show();
        }

        //метод для перехода на окно заказов
        private void Delivery_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна
            OrdersWindow ordersWindow = new OrdersWindow();
            //закрытие окна, которое открыто сейчас
            this.Close();
            //открытие ранее созданного объекта окна
            ordersWindow.Show();
        }

        //метод для перехода в главное меню
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна
            MainWindow mainWindow = new MainWindow();
            //закрытие окна, которое открыто сейчас
            this.Close();
            //открытие ранее созданного объекта окна
            mainWindow.Show();
        }
    }
}
