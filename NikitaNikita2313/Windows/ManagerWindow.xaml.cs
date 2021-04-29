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

namespace NikitaNikita2313.Windows
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void See_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CheckWindow chekWindow = new CheckWindow();
            chekWindow.ShowDialog();
            this.Show();
            this.Close();
        }

        public ManagerWindow(string login)
        {
            InitializeComponent();
            txtMg.Text = "Менеджер " + login;
        }

        private void btn_Click1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
