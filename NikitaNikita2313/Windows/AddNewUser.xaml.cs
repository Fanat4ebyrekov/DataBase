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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class AddNewUser : Window
    {
        public AddNewUser()
        {
            InitializeComponent();
            tbGender.ItemsSource = context.Gender.ToList();
            tbGender.DisplayMemberPath = "GenderName";
            tbGender.SelectedIndex = 0;

            tbRole.ItemsSource = context.Role.ToList();
            tbRole.DisplayMemberPath = "RoleName";
            tbRole.SelectedIndex = 0;

            EditUser.Visibility = Visibility.Hidden;
            EditUser.IsEnabled = false;
        }

        public AddNewUser(Person person)
        {
            InitializeComponent();
            tbGender.ItemsSource = context.Gender.ToList();
            tbGender.DisplayMemberPath = "GenderName";
            tbGender.SelectedIndex = person.IdGender - 1;

            AddUser.Visibility = Visibility.Hidden;
            AddUser.IsEnabled = false;

            tbRole.ItemsSource = context.Role.ToList();
            tbRole.DisplayMemberPath = "RoleName";
            tbRole.SelectedIndex = person.IdRole - 1;

            tbLog.Text = person.Name;
            tbPass.Text = person.Password;
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person();
            if (!string.IsNullOrWhiteSpace(tbLog.Text))
            {
                person.Name = tbLog.Text;
            }
            else
            {
                MessageBox.Show("Вы не ввели логин");
            }
            if (!string.IsNullOrWhiteSpace(tbPass.Text))
            {
                person.Password = tbPass.Text;
            }
            else
            {
                MessageBox.Show("Вы не  ввели пароль");
            }

            person.IdRole = tbRole.SelectedIndex + 1;
            person.IdGender = tbGender.SelectedIndex + 1;
            context.Person.Add(person);
            context.SaveChanges();

          

            MessageBox.Show("Пользователь добавлен");

            this.Hide();
            AllUsers allUsersWindow = new AllUsers();
            allUsersWindow.ShowDialog();
            this.Close();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {

            var user = context.Person.Where(i => i.IdPerson == PersonData.IdPerson).FirstOrDefault();
            user.Name = tbLog.Text.Trim();
            user.Password = tbPass.Text.Trim();
            user.IdRole = tbRole.SelectedIndex + 1;
            user.IdGender = tbGender.SelectedIndex + 1;


            var chek = MessageBox.Show($"Вы хотите изменить данные ", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (string.IsNullOrEmpty(tbLog.Text))
            {
                MessageBox.Show("Пустое поле логина");
                return;
            }
            if (string.IsNullOrEmpty(tbPass.Text))
            {
                MessageBox.Show("Пустое поле пароля");
                return;
            }


            if (chek == MessageBoxResult.Yes)
            {
                context.SaveChanges();

                MessageBox.Show("Данные изменены");
                this.Hide();
                AllUsers allUsers = new AllUsers();
                allUsers.ShowDialog();
                this.Close();
            }


            else
            {
                MessageBox.Show("Вы не ввели значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



  
        
    }
}
