using Core;
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

namespace PenOrderBase.Pages
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var passwrod = pbPassword.Password;

            StringBuilder stringBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(login))
                stringBuilder.AppendLine("Логин не введен!");
            if (string.IsNullOrWhiteSpace(passwrod))
                stringBuilder.AppendLine("Пароль не введен!");

            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            if ((App.Customer = DataAccess.LoginCustomer(login, passwrod)) == null)
            {
                MessageBox.Show("Неверный логин и/или пароль!", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new OrdersPage());
        }

        private void btnRegistartion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistartionPage());
        }
    }
}
