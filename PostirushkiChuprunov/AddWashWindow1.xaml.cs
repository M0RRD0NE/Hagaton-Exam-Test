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

namespace PostirushkiChuprunov
{
    /// <summary>
    /// Логика взаимодействия для AddWashWindow1.xaml
    /// </summary>
    public partial class AddWashWindow1 : Window
    {
        int idm = 0;
        int idp = 0;
        public AddWashWindow1()
        {
            InitializeComponent();
            var r = App.PBD.Person.Where(x => x.Login == NowClass.NOW).FirstOrDefault();
            AddWashWin.Title = r.Surname + " - Постирушки";
            updatemachineList();
        }
        public void updatemachineList() {
            var r = App.PBD.Services.ToList();
            MachineListSourse.ItemsSource = r;
            PowListSourse.ItemsSource= r;
        }
        private void ChooseMachine_Click(object sender, RoutedEventArgs e)
        {
            var buttonprof = (Button)sender;
            var user = (Services)buttonprof.DataContext;
            MachineLabel.Content = user.Name;
            idm = user.id;
            MachineGrid.Visibility = Visibility.Hidden;
            PowGrid.Visibility = Visibility.Hidden;
        }

        private void MachineButton_Click(object sender, RoutedEventArgs e)
        {
            if (MachineGrid.Visibility == Visibility.Hidden)
            {
                MachineGrid.Visibility = Visibility.Visible;
                PowGrid.Visibility = Visibility.Hidden;
            }
            else {
                MachineGrid.Visibility = Visibility.Hidden;

            }
        }

        private void PowderButton_Click(object sender, RoutedEventArgs e)
        {
            if (PowGrid.Visibility == Visibility.Hidden)
            {
                MachineGrid.Visibility = Visibility.Hidden;
                PowGrid.Visibility = Visibility.Visible;
            }
            else
            {
                PowGrid.Visibility = Visibility.Hidden;

            }
        }

        private void ChoosePow_Click(object sender, RoutedEventArgs e)
        {
            var buttonprof = (Button)sender;
            var user = (Services)buttonprof.DataContext;
            PowderLabel.Content = user.Name;
            idp = user.id;
            MachineGrid.Visibility = Visibility.Hidden;
            PowGrid.Visibility = Visibility.Hidden;
        }

        private void addWash_Click(object sender, RoutedEventArgs e)
        {
            var r = App.PBD.Person.Where(x => x.Login == NowClass.NOW).FirstOrDefault();
            if (SushkaCheck.IsChecked == true)
            {
                ActiveWash activeWash = new ActiveWash()
                {
                    idmachine = idm,
                    idpowder = idp,
                    drying = "Да",
                    IDStaff = r.id,
                    status = "Принят",
                    Branch = NowClass.NOWF,
                };
                App.PBD.ActiveWash.Add(activeWash);
                App.PBD.SaveChanges();
                if (MessageBox.Show("Напечатать чек?", "Postirushki", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Успешно!", "Postirushki");
                    StaffWindow s = new StaffWindow();
                    s.Show();
                    this.Close();
                }
                else
                {
                    StaffWindow s = new StaffWindow();
                    s.Show();
                    this.Close();
                }

            }
            else
            {
                ActiveWash activeWash = new ActiveWash()
                {
                    idmachine = idm,
                    idpowder = idp,
                    drying = "Нет",
                    IDStaff = r.id,
                    status = "Принят",
                    Branch = NowClass.NOWF,
                };
                App.PBD.ActiveWash.Add(activeWash);
                App.PBD.SaveChanges();
                if (MessageBox.Show("Напечатать чек?", "Postirushki", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Успешно!", "Postirushki");
                    StaffWindow s = new StaffWindow();
                    s.Show();
                    this.Close();
                }
                else
                {
                    StaffWindow s = new StaffWindow();
                    s.Show();
                    this.Close();
                }
            }
        }
    }
}
