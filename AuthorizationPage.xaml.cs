using Morgue.Models;
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

namespace Morgue
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MorgueContext morgueContext = new MorgueContext();
                User? user = morgueContext.Users.FirstOrDefault(u => u.Login == LoginTextBox.Text);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                }
                else if (user.Password != PasswordTextBox.Text)
                {
                    MessageBox.Show("Пароль не подходит!");
                }
                else
                {
                    MessageBox.Show("Успешно!");
                    User.ActiveUser = user;
                    MainWindow.Frame.Content = new ViewPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
