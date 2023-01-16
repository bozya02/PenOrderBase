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
    /// Interaction logic for RegistartionPage.xaml
    /// </summary>
    public partial class RegistartionPage : Page
    {
        public RegistartionPage()
        {
            InitializeComponent();
        }

        private void btnRegistartion_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer
            {
                Name = tbName.Text,
                User = new User
                {
                    Login = tbLogin.Text,
                    Password = pbPassword.Password
                }
            };

            var secondPassword = pbSecondPassword.Password;

            StringBuilder stringBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(customer.Name))
                stringBuilder.AppendLine("Имя не введено!");
            
            if (string.IsNullOrWhiteSpace(customer.User.Login))
                stringBuilder.AppendLine("Логин не введен!");
            if (DataAccess.IsLoginBusy(customer.User.Login))
                stringBuilder.AppendLine("Логин не уникален!");
            if (string.IsNullOrWhiteSpace(customer.User.Password))
                stringBuilder.AppendLine("Пароль не введен!");
            if (customer.User.Password != secondPassword)
                stringBuilder.AppendLine("Пароли не совпадают!");

            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            DataAccess.SaveCustomer(customer);

            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
