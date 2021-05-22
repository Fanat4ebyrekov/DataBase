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
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;

namespace NikitaNikita2313.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AllUsers : Window
    {


        List<string> qtyList = new List<string>() { "Все", "10", "50", "100" };
        List<Person> people = new List<Person>();
        List<Role> roleList = new List<Role>();
        private int selectNotes = 0;
        private int numPage = 0;

        public AllUsers()
        {
            InitializeComponent();
            All.ItemsSource = context.Person.ToList();
            roleList = ClassEntities.context.Role.ToList();
            roleList.Insert(0, new Role {RoleName = "Все роли"});
            cmbSortRole.ItemsSource = roleList;
            cmbSortRole.DisplayMemberPath = "RoleName";
            cmbSortRole.SelectedIndex = 0;

            cmbVolume.ItemsSource = qtyList;
            cmbVolume.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            people = ClassEntities.context.Person.ToList();

            

            if (cmbSortRole.SelectedIndex != 0)
            {
                people = people.Where(i => i.IdRole == cmbSortRole.SelectedIndex).ToList();
            }

            people = people.Where(i => i.FIO.Contains(txtSearch.Text)).ToList();

            if (cmbVolume.SelectedIndex == 0)
            {
                numPage = 0;
            }
            if (cmbVolume.SelectedIndex != 0)
            {
                selectNotes = Convert.ToInt32(cmbVolume.SelectedItem);
            }
            else
            {
                selectNotes = people.ToList().Count();
            }

            people = people.Skip(numPage * selectNotes).Take(selectNotes).ToList();


            All.ItemsSource = people;
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


        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (All.SelectedItem is Person person)
            {
                var resMAss = MessageBox.Show($"Вы хотите изменить пользователя {person.Name}", "Предупреждение", MessageBoxButton.YesNo);
                if (resMAss == MessageBoxResult.Yes)
                {
                    this.Hide();
                    AddNewUser addUsersWindow = new AddNewUser(person);
                    PersonData.IdPerson = person.IdPerson;
                    addUsersWindow.ShowDialog();
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show($"Вы не выбрали пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
           Filter();
        }

        private void cmbSortRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Filter();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (people.ToList().Count > 0 && people.ToList().Count() >= selectNotes)
            {
                numPage++;
                Filter();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (people.ToList().Count > 0 && people.ToList().Count() >= selectNotes)
            {
                numPage++;
                Filter();
            }
        }

        private void cmbVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
