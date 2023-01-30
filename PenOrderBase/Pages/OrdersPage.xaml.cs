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
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public List<Order> Orders { get; set; }
        public List<Order> FilteredOrders { get; set; }
        public Dictionary<string, Func<Order, object>> Sortings { get; set; }

        public OrdersPage()
        {
            InitializeComponent();

            Orders = DataAccess.GetOrders(App.Customer);
            FilteredOrders = Orders.ToList();

            Sortings = new Dictionary<string, Func<Order, object>>
            {
                { "Номер по возрастанию", x => x.Id },
                { "Номер по убыванию", x => x.Id },     //reverse
                { "Цена по возрастанию", x => x.Pen.Price },
                { "Цена по убыванию", x => x.Pen.Price },     //reverse
            };

            cbSortings.ItemsSource = Sortings.Keys;
            this.DataContext = this;

            DataAccess.AddNewItemEvent += DataAccess_AddNewItemEvent;
        }

        private void DataAccess_AddNewItemEvent()
        {
            Orders = DataAccess.GetOrders();
            ApplyFilters();

            lvOrders.Items.Refresh();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbSortings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var date = dpDate.SelectedDate;
            var sorting = Sortings[cbSortings.SelectedItem as string];

            if (date == null)
                date = DateTime.MinValue;

            FilteredOrders = Orders.FindAll(x => date <= x.Date);

            FilteredOrders = (cbSortings.SelectedItem as string).Contains("убыванию") ?
                FilteredOrders.OrderByDescending(sorting).ToList() :
                FilteredOrders.OrderBy(sorting).ToList();

            lvOrders.ItemsSource = FilteredOrders;
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderPage(new Order(), true));
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var order = lvOrders.SelectedItem as Order;
            if (order != null)
                NavigationService.Navigate(new OrderPage(order));

            lvOrders.SelectedIndex = -1;
        }
    }
}
