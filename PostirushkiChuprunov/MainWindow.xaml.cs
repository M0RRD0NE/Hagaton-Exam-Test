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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostirushkiChuprunov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void SingInButton_Click(object sender, RoutedEventArgs e)
        {
            var r = App.PBD.Person.Where(x => x.Login == LoginText.Text).FirstOrDefault();
            if (r != null)
            {
                if (r.Password == PasswordText.Text)
                {
                    if (r.RoleID == 0)
                    {
                        NowClass.NOW = r.Login;
                        StaffWindow staffWindow = new StaffWindow();
                        staffWindow.Show();
                        this.Close();
                    }
                    else if (r.RoleID == 1) { 
                        NowClass.NOW = r.Login;
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                }
                else
                {

                }
            }
            else { 
            
            }
        }
    }
}
