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
    /// Логика взаимодействия для NewStatusWindow1.xaml
    /// </summary>
    public partial class NewStatusWindow1 : Window
    {
        public NewStatusWindow1()
        {
            InitializeComponent();
            StatusBox.Items.Add("Принят");
            StatusBox.Items.Add("Выполняется");
            StatusBox.Items.Add("Готов");
        }

        private void ChoosestatusButton_Click(object sender, RoutedEventArgs e)
        {
            var r = App.PBD.ActiveWash.Where(c => c.id == NowClass.NOWStatus).FirstOrDefault();
            if (StatusBox.SelectedIndex == 0) {
                r.status = "Принят";
                App.PBD.SaveChanges();
            }
            else if (StatusBox.SelectedIndex == 1)
            {
                r.status = "Выполняется";
                App.PBD.SaveChanges();
            }
            else if (StatusBox.SelectedIndex == 2)
            {
                r.status = "Готов";
                App.PBD.SaveChanges();
            }
        }
    }
}
