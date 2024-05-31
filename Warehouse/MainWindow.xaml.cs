using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warehouse.Windows;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
        }
        //метод для перехода на окно клиента
        private void Client_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна
            ClientWindow clientWindow = new ClientWindow();
            //закрытие окна, которое открыто сейчас
            this.Close();
            //открытие ранее созданного объекта окна
            clientWindow.Show();
        }

        //метод для перехода на окно админа
        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            //создание обьекта окна
            AdminWindow adminWindow = new AdminWindow();
            //закрытие окна, которое открыто сейчас
            this.Close();
            //открытие ранее созданного объекта окна
            adminWindow.Show();
        }

    }
}