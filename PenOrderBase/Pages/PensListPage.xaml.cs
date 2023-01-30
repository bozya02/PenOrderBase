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
    /// Interaction logic for PensListPage.xaml
    /// </summary>
    public partial class PensListPage : Page
    {
        public List<Core.Pen> Pens { get; set; }
        public List<Core.Pen> FilteredPens { get; set; }

        public List<PenType> PenTypes{ get; set; }
        public Dictionary<string, Func<Core.Pen, object>> Sortings { get; set; }

        public PensListPage()
        {
            InitializeComponent();

            Pens = DataAccess.GetPens();
            Pens = Pens.ToList();
            
            PenTypes = DataAccess.GetPenTypes();
            PenTypes.Insert(0, new PenType
            {
                Name = "Все"
            });

            Sortings = new Dictionary<string, Func<Core.Pen, object>>
            {
                { "Номер по возрастанию", x => x.Id },
                { "Номер по убыванию", x => x.Id },     //reverse
                { "Цена по возрастанию", x => x.Price },
                { "Цена по убыванию", x => x.Price },     //reverse
            };

            cbSortings.ItemsSource = Sortings.Keys;
            this.DataContext = this;
        }

        private void ApplyFilters()
        {
            var search = tbSearch.Text;
            var sorting = Sortings[cbSortings.SelectedItem as string];
            var penType = cbPenType.SelectedItem as Core.PenType;

            FilteredPens = Pens.FindAll(x => x.Name.ToLower().Contains(search));

            if (penType.Id != 0)
                FilteredPens = FilteredPens.FindAll(x => x.PenType == penType);

            FilteredPens = (cbSortings.SelectedItem as string).Contains("убыванию") ?
                FilteredPens.OrderByDescending(sorting).ToList() :
                FilteredPens.OrderBy(sorting).ToList();

            lvPens.ItemsSource = FilteredPens;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbPenType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void btnNewPen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PenPage(new Core.Pen(), true));
        }

        private void lvPens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pen = lvPens.SelectedItem as Core.Pen;
            if (pen != null)
                NavigationService.Navigate(new PenPage(pen));

            lvPens.SelectedIndex = -1;
        }

        private void cbSortings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
    }
}
