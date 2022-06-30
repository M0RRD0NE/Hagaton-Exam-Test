using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            var r = App.PBD.Person.Where(x => x.Login == NowClass.NOW).FirstOrDefault();
            AdminWindow1.Title = r.Surname + "(Администратор) - Постирушки";
            double podchet = 0;
            var t = App.PBD.ActiveWash.Where(c => c.status == "Готов").ToList();
            AmountDoneText.Text = "Количество\nготовых стирок\n" + t.Count.ToString();
            NowClass.AMountDoneText = t.Count.ToString();
            //подсчет
            var q = App.PBD.ActiveWash.Where(c => c.drying == "Да").ToList();
            if (q != null)
            {
                podchet += (q.Count * 1150);
            }
            var q1 = App.PBD.ActiveWash.ToList();
            if (q1 != null)
            {
                podchet += (q1.Count * 1250);
            }
            var q2 = App.PBD.ActiveWash.Where(c => c.idpowder == 3).ToList();
            if (q2 != null)
            {
                podchet += (q2.Count * 1.35);
            }
            var q3 = App.PBD.ActiveWash.Where(c => c.idpowder == 4).ToList();
            if (q3 != null)
            {
                podchet += (q3.Count * 1.25);
            }
            var q4 = App.PBD.ActiveWash.Where(c => c.idpowder == 5).ToList();
            if (q4 != null)
            {
                podchet += (q4.Count * 1.30);
            }
            FullPriceText.Text = podchet.ToString();
            NowClass.FullPrice = podchet.ToString();
        }

        private void otchetbutton_Click_1(object sender, RoutedEventArgs e)
        {
            string path = @"..\..\отчетАдмин.pdf";
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Отчет <Постирушки>" + "\nКоличество завершенных стирок - " + NowClass.AMountDoneText.ToString() + "\nОбщая полученная сумма - " + NowClass.FullPrice.ToString());
            sw.Close();
            fs.Close();

        }

        private void AddStaffButton_click(object sender, RoutedEventArgs e)
        {
            if (AddStaffGrid.Visibility == Visibility.Hidden)
            {
                AddStaffGrid.Visibility = Visibility.Visible;
            }
            else {
                AddStaffGrid.Visibility = Visibility.Hidden;
            }
        }

        private void AddStaffBG_Click(object sender, RoutedEventArgs e)
        {
            int idro;
            if (IsAdminBox.IsChecked == true) {
                idro = 1;
            }
            else {
                idro = 0;
            }
            Person person = new Person() {
                Surname = SurnameText.Text,
                Name = NameText.Text,
                Patr = PatrText.Text,
                Login = LoginText.Text,
                Password = PassText.Text,
                RoleID = idro
            };
            App.PBD.Person.Add(person);
            App.PBD.SaveChanges();
            MessageBox.Show("Сотрудник успешно добавлен!");
        }
    }
}
