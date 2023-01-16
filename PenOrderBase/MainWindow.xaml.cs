using PenOrderBase.Pages;
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

namespace PenOrderBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.NavigationService.Navigate(new AuthorizationPage());
            frame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var content = frame.Content as Page;

            if (content is AuthorizationPage)
            {
                spButtons.Visibility = Visibility.Hidden;
            }
            else
            {
                spButtons.Visibility = Visibility.Visible;
                btnForward.Visibility = Visibility.Hidden;
            }

            tbtitle.Text = content.Title;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoBack)
                frame.GoBack();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoForward)
                frame.GoForward();
        }
    }
}
