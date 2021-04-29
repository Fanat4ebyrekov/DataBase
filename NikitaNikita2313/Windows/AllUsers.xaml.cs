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
using NikitaNikita2313.EF;
using static NikitaNikita2313.ClassEntities;

namespace NikitaNikita2313.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AllUsers : Window
    {
        public AllUsers()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (All.SelectedItem is Person person)
            {
                this.Hide();
                AddNewUser addUsersWindow = new AddNewUser(person);
                PersonData.IdPerson = person.IdPerson;
                addUsersWindow.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                AddNewUser addUsersWindow = new AddNewUser();
                addUsersWindow.ShowDialog();
                this.Close();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (All.SelectedItem is Person person)
            {
                context.Person.Remove(context.Person.Where(i => i.IdPerson == person.IdPerson).FirstOrDefault());
                context.SaveChanges();
                All.ItemsSource = context.Person.ToList();
            }
        }

        private void btn_Click1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
