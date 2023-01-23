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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public Order Order { get; set; }
        public List<Core.Pen> Pens { get; set; }

        public OrderPage(Order order, bool isNew = false)
        {
            InitializeComponent();

            Pens = DataAccess.GetPens();

            if (isNew)
            {
                Title = $"Новый {Title}";
            }
            else
            {
                Title += $" {Order.Id}";
                IsEnabled = false;
            }

            this.DataContext = this;
        }
    }
}
