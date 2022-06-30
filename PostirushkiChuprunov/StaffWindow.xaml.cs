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
    /// Логика взаимодействия для StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            InitializeComponent();
            var r = App.PBD.Person.Where(x => x.Login == NowClass.NOW).FirstOrDefault();
            StaffWin.Title = r.Surname + " - Постирушки";
            
            FelialBox.Items.Add("город Тюмень");
            FelialBox.Items.Add("город Москва");
            FelialBox.Items.Add("город Екатеринбург");
        }
        public void updateActiveList()
        {
            try
            {
                if (FelialBox.SelectedItem.ToString() == "город Тюмень")
                {
                    var r = App.PBD.ActiveWash.Where(c => c.Branch == "город Тюмень" && c.status == "Принят").ToList();
                    ActiveWashView.ItemsSource = r;
                }
                else if (FelialBox.SelectedItem.ToString() == "город Москва")
                {
                    var r = App.PBD.ActiveWash.Where(c => c.Branch == "город Москва" && c.status == "Принят").ToList();
                    ActiveWashView.ItemsSource = r;
                }
                else if (FelialBox.SelectedItem.ToString() == "город Екатеринбург")
                {
                    var r = App.PBD.ActiveWash.Where(c => c.Branch == "город Екатеринбург" && c.status == "Принят").ToList();
                    ActiveWashView.ItemsSource = r;
                }
                else
                {
                    MessageBox.Show("Выберите свой фелиал!", "Postirushki");
                }
            }
            catch {
                MessageBox.Show("Выберите свой фелиал!", "Postirushki");
            }
        }
        public void updateEndList() {
            try
            {
                if (FelialBox.SelectedItem.ToString() == "город Тюмень")
                {
                    var r = App.PBD.ActiveWash.Where(c => c.Branch == "город Тюмень" && c.status == "Готов").ToList();
                    DoneWashView.ItemsSource = r;
                }
                else if (FelialBox.SelectedItem.ToString() == "город Москва")
                {
                    var r = App.PBD.ActiveWash.Where(c => c.Branch == "город Москва" && c.status == "Готов").ToList();
                    DoneWashView.ItemsSource = r;
                }
                else if (FelialBox.SelectedItem.ToString() == "город Екатеринбург")
                {
                    var r = App.PBD.ActiveWash.Where(c => c.Branch == "город Екатеринбург" && c.status == "Готов").ToList();
                    DoneWashView.ItemsSource = r;
                }
                else
                {
                    MessageBox.Show("Выберите свой фелиал!", "Postirushki");
                }
            }
            catch {
                MessageBox.Show("Выберите свой фелиал!", "Postirushki");
            }
        }
        private void ActiveWashButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveWashView.Visibility == Visibility.Hidden)
            {
                ActiveWashButton.FontWeight = FontWeights.Bold;
                DoneWashButton.FontWeight= FontWeights.Normal;
                OtchetButton.FontWeight= FontWeights.Normal;
                ActiveWashView.Visibility = Visibility.Visible;
                DoneWashView.Visibility = Visibility.Hidden;
                OtchetGrid.Visibility = Visibility.Hidden;
                updateActiveList();
            }
            else {
                ActiveWashButton.FontWeight = FontWeights.Bold;
                DoneWashButton.FontWeight = FontWeights.Normal;
                OtchetButton.FontWeight = FontWeights.Normal;
                updateActiveList();
            }
        }

        private void DoneWashButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoneWashView.Visibility == Visibility.Hidden)
            {
                ActiveWashButton.FontWeight = FontWeights.Normal;
                DoneWashButton.FontWeight = FontWeights.Bold;
                OtchetButton.FontWeight = FontWeights.Normal;
                ActiveWashView.Visibility = Visibility.Hidden;
                DoneWashView.Visibility = Visibility.Visible;
                OtchetGrid.Visibility = Visibility.Hidden;
                updateEndList();
            }
            else
            {
                ActiveWashButton.FontWeight = FontWeights.Normal;
                DoneWashButton.FontWeight = FontWeights.Bold;
                OtchetButton.FontWeight = FontWeights.Normal;
                updateEndList();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NowClass.NOWF = FelialBox.SelectedItem.ToString();
                AddWashWindow1 addWashWindow1 = new AddWashWindow1();
                addWashWindow1.Show();
                this.Close();
            }
            catch {
                MessageBox.Show("Прежде чем добавить стирку выберите свой фелиал!", "Postirushki");
            }
        }

        private void FelialBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateActiveList();
        }
        public void clickac() {
            MessageBox.Show(ActiveWashView.SelectedItems.ToString());
        }

        private void ChooseMachine_Click(object sender, RoutedEventArgs e)
        {
            var buttonprof = (Button)sender;
            var user = (ActiveWash)buttonprof.DataContext;
            NowClass.NOWStatus = user.id;
            NewStatusWindow1 newStatusWindow1 = new NewStatusWindow1();
            newStatusWindow1.Show();

        }

        private void OtchetButton_Click(object sender, RoutedEventArgs e)
        {
            if (OtchetGrid.Visibility == Visibility.Hidden)
            {
                ActiveWashButton.FontWeight = FontWeights.Normal;
                DoneWashButton.FontWeight = FontWeights.Normal;
                OtchetButton.FontWeight = FontWeights.Bold;
                ActiveWashView.Visibility = Visibility.Hidden;
                DoneWashView.Visibility = Visibility.Hidden;
                OtchetGrid.Visibility = Visibility.Visible;

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
            else {
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
        }

        private void otchetbutton_Click_1(object sender, RoutedEventArgs e)
        {
            string path = @"..\..\отчет.pdf";
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Отчет <Постирушки>\nФилиал - "+ FelialBox.SelectedItem.ToString()+ "\nКоличество завершенных стирок - "+NowClass.AMountDoneText.ToString()+"\nОбщая полученная сумма - "+NowClass.FullPrice.ToString());
            sw.Close();
            fs.Close();
            
        }
    }
}
