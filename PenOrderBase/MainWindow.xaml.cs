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
using System.Windows.Threading;

namespace PenOrderBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;
        public bool MenuVisible { get; set; } = true;
        public MainWindow()
        {
            InitializeComponent();
            frame.NavigationService.Navigate(new AuthorizationPage());
            frame.Navigated += Frame_Navigated;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timer.Tick += Timer_Tick;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var content = frame.Content as Page;

            if (content is AuthorizationPage)
            {
                spButtons.Visibility = Visibility.Collapsed;
            }
            else if (content is RegistartionPage)
            {
                spButtons.Visibility = Visibility.Visible;
                btnForward.Visibility = Visibility.Collapsed;
            }
            else
            {
                spButtons.Visibility = Visibility.Visible;
                btnForward.Visibility = Visibility.Visible;
            }

            tbtitle.Text = content.Title;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (MenuVisible)
            {
                gridMenu.Width += 2;
                if (gridMenu.Width >= 200)
                {
                    _timer.Stop();
                    MenuVisible = false;
                }
            }
            else
            {
                gridMenu.Width -= 2;
                if (gridMenu.Width <= 0)
                {
                    _timer.Stop();
                    MenuVisible = true;
                }
            }
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

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            var content = frame.Content as Page;

            if (content is AuthorizationPage || content is RegistartionPage)
            {
                MessageBox.Show("Необходимо авторизоваться, урод!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            frame.Navigate(new OrdersPage());
        }

        private void btnPens_Click(object sender, RoutedEventArgs e)
        {
            var content = frame.Content as Page;

            if (content is AuthorizationPage || content is RegistartionPage)
            {
                MessageBox.Show("Необходимо авторизоваться, урод!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            frame.Navigate(new PensListPage());
        }
    }
}
